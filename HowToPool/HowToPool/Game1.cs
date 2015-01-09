using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HowToPool
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        
        mainUpdate updateGame = new mainUpdate();

        Menu menu = new Menu();

        List<Entity> Entities = new List<Entity>();

        Application application = new Application();
      

        MouseCursor mouseHandler = new MouseCursor();

        PoolTable table = new PoolTable();

        private List<DrawString> currentMenu;
        string tickState = Config.State;
        int tickSelected = Config.Selected;

        
        //Entity player = new Entity("Defenceship",new Vector2(0,0));

        public static Random rnd = new Random();

        private bool cWasUp = true;

        private Texture2D redBall;
        private Texture2D blueBall;
        private Texture2D whiteBall;

        private bool rWasUp = true;
        private static bool pgWasUp = true;
        
        static int maxv = 10;
        static int minv = maxv * -1;

        //Random numebrs(temporary but fun)
       /* static float a = (float)rnd.Next(minv, maxv);
        static float b = (float)rnd.Next(minv, maxv);

        Ball redBall = new Ball("redBall", new Vector3(100,100,0),13,100,new Vector2(a, b),new Vector2(0,0));

        static float a1 = (float)rnd.Next(minv, maxv);
        static float b1 = (float)rnd.Next(minv, maxv);

        Ball blueBall = new Ball("blueBall", new Vector3(200, 100, 0), 13, 100,new Vector2(a1, b1), new Vector2(1000, 0));

        static float a2 = (float)rnd.Next(minv, maxv);
        static float b2 = (float)rnd.Next(minv, maxv);

        Ball redBall2 = new Ball("redBall", new Vector3(100, 50, 0), 13, 100, new Vector2(a2, b2), new Vector2(300, 300));

        static float a3 = (float)rnd.Next(minv, maxv);
        static float b3 = (float)rnd.Next(minv, maxv);
        
        Ball blueBall2 = new Ball("blueBall", new Vector3(500, 150, 0), 13, 100, new Vector2(a3, b3), new Vector2(600, 150));

        static float a4 = (float)rnd.Next(minv, maxv);
        static float b4 = (float)rnd.Next(minv, maxv);

        Ball redBall3 = new Ball("redBall", new Vector3(100, 50, 0), 13, 100, new Vector2(a4, a5), new Vector2(0, 500));

        static float a5 = (float)rnd.Next(minv, maxv);
        static float b5 = (float)rnd.Next(minv, maxv);

        Ball blueBall3 = new Ball("blueBall", new Vector3(500, 150, 0), 13, 100, new Vector2(a5, -b5), new Vector2(1200, 150));*/



        Renderer renderer = new Renderer();

        SpriteFont Font1;
        SpriteFont TitleFont;

        Vector2 FontPos;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
         
            
            graphics.PreferredBackBufferWidth = Config.width;
            graphics.PreferredBackBufferHeight = Config.height;

            Content.RootDirectory = "Content";

            Config.State = "mainMenu";
            Config.Selected = 0;

            
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Entities.Add(redBall);
            //Entities.Add(blueBall);
            Config.shouldCollide = true;
            Config.shouldResist = true;
            Config.soundEnabled = true;
            Config.fov = 20;

            
            

            base.Initialize();
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Entities[0].texture = Content.Load<Texture2D>("Defenceship");
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
            TitleFont = Content.Load<SpriteFont>("TitleFont");

            //Note : All drawstrings will be in a list corrosponding to what part of the menu their in
            DrawString test = new DrawString("play", Font1, "Play", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));

            //List of strings for menu
            List<DrawString> _MenuStrings = new List<DrawString>();


            //Sets up title that is displayed to user
            DrawString TitleString = new DrawString("title", Font1, "How to pool", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) - 40));

            //Adds menu strings that the user can interact with
            _MenuStrings.Add(new DrawString("play", Font1, "Play", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            _MenuStrings.Add(new DrawString("settings", Font1, "Settings", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 40)));
            _MenuStrings.Add(new DrawString("quit", Font1, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 80)));



            List<DrawString> _SettingsMenuStrings = new List<DrawString>();

            //Adds menu strings to list for settings menu
            _SettingsMenuStrings.Add(new DrawString("sound", Font1, "Sound: " + Config.soundEnabled, new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2))));
            _SettingsMenuStrings.Add(new DrawString("fps", Font1, "FPS limit: " + Config.maxFPS, new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 20)));
            _SettingsMenuStrings.Add(new DrawString("fov", Font1, "FOV " + (Config.fov + 50).ToString(), new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 40)));
            _SettingsMenuStrings.Add(new DrawString("sale",Font1, "Sale? " + Config.SALE,new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+60)));

            //Create menu
            menu = new Menu(TitleString,_MenuStrings,_SettingsMenuStrings);



            application.ContentLoad(renderer,Entities,table,Content);

            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);


            Viewport viewport = graphics.GraphicsDevice.Viewport;

            /*Menu.mainMenu.Add(new DrawString("play",Font1, "Play", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            Menu.mainMenu.Add(new DrawString("settings",Font1, "Settings", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 40)));
            Menu.mainMenu.Add(new DrawString("quit",Font1, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 80)));

            Menu.settingsMenu.Add(new DrawString("sound",Font1, "Sound: " + Config.soundEnabled,new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2))));
            Menu.settingsMenu.Add(new DrawString("sale",Font1, "Sale? " + Config.SALE,new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+60)));
            Menu.settingsMenu.Add(new DrawString("fps",Font1, "FPS limit: " + Config.maxFPS, new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+20)));
            Menu.settingsMenu.Add(new DrawString("fov",Font1, "FOV " + (Config.fov + 50).ToString(), new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+40)));*/
        }


        

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

       

       

       
        protected override void Update(GameTime gameTime)
        {
            tickState = Config.State;
            tickSelected = Config.Selected;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Config.State == "quit")
            {
                Exit();
            }
            
            /*if (tickState == "startSPGame")
            {
                StartGame();
                Config.State = "SPGame";
            }*/
            
            
           
            menu.UpdateMenus(application);
            
            /*if (Keyboard.GetState().IsKeyDown(Keys.Enter)) { 
                balls[0].pos.X = 100;
            }*/
            
            if (Keyboard.GetState().IsKeyDown(Keys.C) && cWasUp)
            {
                cWasUp = false;
                if (Config.shouldCollide){
                    Config.shouldCollide = false;
                }else{
                    Config.shouldCollide = true;
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.C))
            {
                cWasUp = true;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.PageUp) && pgWasUp)
            {
                pgWasUp = false;

                Config.resistance = Config.resistance + 0.05f;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.PageDown) && pgWasUp)
            {
                pgWasUp = false;

                Config.resistance = Config.resistance - 0.05f;

            }

            if ((Keyboard.GetState().IsKeyUp(Keys.PageUp) || Keyboard.GetState().IsKeyUp(Keys.PageDown)) && !Keyboard.GetState().IsKeyDown(Keys.PageDown) && !Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                pgWasUp = true;
            }



            if (Keyboard.GetState().IsKeyDown(Keys.R) && rWasUp)
            {
                rWasUp = false;
                if (Config.shouldResist)
                {
                    Config.shouldResist = false;
                }
                else
                {
                    Config.shouldResist = true;
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.R))
            {
                rWasUp = true;
            }

            updateGame.run(Entities,application.balls,application.cue,mouseHandler,gameTime);
           
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


 
            //spriteBatch.DrawString(Font1, balls[0].vel.ToString(), new Vector2(150, 150), Color.Black, 0, FontOrigin, 2.0f, SpriteEffects.None, 0.5f);
            //spriteBatch.DrawString(Font1, "FPS: " + (1 / (Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds))).ToString(), new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(Font1, "Collisions: " + Config.shouldCollide, new Vector2(0, 20), Color.Black);
            spriteBatch.DrawString(Font1, "Resistance (" + Config.resistance.ToString() + "): "  + Config.shouldResist, new Vector2(0,40), Color.Black);
            spriteBatch.DrawString(Font1, "Selected: " + menu.selected.ToString(), new Vector2(0, 60), Color.Black);
            spriteBatch.DrawString(Font1, "State: " + Config.State + " (" + Config.State + ")", new Vector2(0, 80), Color.Black);
            //spriteBatch.DrawString(Font1, "State: " + Config.State + " (" + Application.cue + ")", new Vector2(0, 100), Color.Black);


            renderer.run(Entities, application.balls, application.cue, table, gameTime, spriteBatch);

           
            menu.DrawMenus(spriteBatch);



            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
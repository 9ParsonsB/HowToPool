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

        Color playColor = Color.Black;
        Color quitColor = Color.Black;

        List<Entity> Entities = new List<Entity>();
        static List<Ball> balls = new List<Ball>();
        List<Ball> tempBalls = new List<Ball>();
        private List<DrawString> currentMenu;
        string tickState = Config.State;
        int tickSelected = Config.Selected;

        
        //Entity player = new Entity("Defenceship",new Vector2(0,0));

        public static Random rnd = new Random();

        private bool cWasUp = true;

        private Texture2D redBall;
        private Texture2D blueBall;

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
        Vector2 FontPos;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
         
            
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 700;

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

            List<Texture2D> textures = renderer.ContentLoad(Entities, balls, Content);

            redBall = textures[0];
            blueBall = textures[1];

            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            Viewport viewport = graphics.GraphicsDevice.Viewport;

            Menu.mainMenu.Add(new DrawString("play",Font1, "Play", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            Menu.mainMenu.Add(new DrawString("settings",Font1, "Settings", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 40)));
            Menu.mainMenu.Add(new DrawString("quit",Font1, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 80)));

            Menu.settingsMenu.Add(new DrawString("sound",Font1, "Sound: " + Config.soundEnabled,new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2))));
            Menu.settingsMenu.Add(new DrawString("sale",Font1, "Sale? " + Config.SALE,new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+60)));
            Menu.settingsMenu.Add(new DrawString("fps",Font1, "FPS limit: " + Config.maxFPS, new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+20)));
            Menu.settingsMenu.Add(new DrawString("fov",Font1, "FOV " + (Config.fov + 50).ToString(), new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+40)));
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

        private void StartGame()
        {
            for (int i = 0; i < 100; i++)
            {
                float a = (float)rnd.Next(minv, maxv);
                float b = (float)rnd.Next(minv, maxv);
                float x = (float)rnd.Next(0, graphics.PreferredBackBufferWidth);
                float y = (float)rnd.Next(0, graphics.PreferredBackBufferHeight);
                if (i % 2 == 0)
                {
                    tempBalls.Add(new Ball(redBall, new Vector3(0, 0, 0), 13, 100, new Vector2(a, b), new Vector2(x, y)));
                }
                else
                {
                    tempBalls.Add(new Ball(blueBall, new Vector3(0, 0, 0), 13, 100, new Vector2(a, b), new Vector2(x, y)));
                }

            }

            foreach (Ball i in tempBalls.ToArray())
            {
                balls.Add(i);
            }
            tempBalls = new List<Ball>();
        }

        public static void clearBalls()
        {
            foreach (Ball b in balls.ToArray())
            {
                balls.Remove(b);
            }
        }


        protected override void Update(GameTime gameTime)
        {
            tickState = Config.State;
            tickSelected = Config.Selected;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (tickState == "quit")
            {
                Exit();
            }
            if (tickState == "startSPGame")
            {
                StartGame();
                Config.State = "SPGame";
            }
            
            // TODO: Add your update logic here

            //Entities[0].update(Entities, 0);
            Menu.updateMenu(tickState,tickSelected);
            
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

                Config.resistnace = Config.resistnace + 0.05f;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.PageDown) && pgWasUp)
            {
                pgWasUp = false;

                Config.resistnace = Config.resistnace - 0.05f;

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

            updateGame.run(Entities,balls,gameTime);
           
         
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
                    


            if (tickState == "mainMenu") { currentMenu = Menu.mainMenu; }
            if (tickState == "settingMenu") { currentMenu = Menu.settingsMenu; }
            List<DrawString> mainMenu = Menu.mainMenu;
            List<DrawString> settingsMenu = Menu.settingsMenu;

            if (tickState == "mainMenu")
            {
                if (Config.Selected == 0) { playColor = Color.Red; } else { playColor = Color.Black; }
                if (Config.Selected == 1) { quitColor = Color.Red; } else { quitColor = Color.Black; }
                foreach (DrawString menuItem in Menu.mainMenu.ToArray())
                {
                    if (menuItem.id == "play")
                    {
                        if (Config.Selected == 0 ) 
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "settings")
                    {
                        if (Config.Selected == 1)
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "quit")
                    {
                        if (Config.Selected == 2)
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }

                    DrawString item = mainMenu[mainMenu.IndexOf(menuItem)];
                    spriteBatch.DrawString(item.Font, item.Text, item.Position, item.TextColor);
                }
            }
            else if (tickState == "settingsMenu")
            {
                foreach (DrawString menuItem in settingsMenu.ToArray())
                {
                    if (menuItem.id == "sound")
                    {
                        settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "Sound: " + Config.soundEnabled;
                        if (Config.Selected == 0)
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "fps")
                    {
                        if (Config.maxFPS == 0) { settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "FPS limit: " + "None"; }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "FPS limit: " + Config.maxFPS;
                        }

                        
                        if (Config.Selected == 1)
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "fov")
                    {
                        if (Config.Selected == 2)
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "sale")
                    {
                        settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "Sale? " + Config.SALE;
                        if (Config.Selected == 3)
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }

                    DrawString item = settingsMenu[settingsMenu.IndexOf(menuItem)];
                    spriteBatch.DrawString(item.Font,item.Text,item.Position,item.TextColor);

                }
            }
            //spriteBatch.DrawString(Font1, balls[0].vel.ToString(), new Vector2(150, 150), Color.Black, 0, FontOrigin, 2.0f, SpriteEffects.None, 0.5f);
            //spriteBatch.DrawString(Font1, "FPS: " + (1 / (Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds))).ToString(), new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(Font1, "Collisions: " + Config.shouldCollide, new Vector2(0, 20), Color.Black);
            spriteBatch.DrawString(Font1, "Resistance (" + Config.resistnace.ToString() + "): "  + Config.shouldResist, new Vector2(0,40), Color.Black);
            spriteBatch.DrawString(Font1, "Selected: " + Config.Selected.ToString(), new Vector2(0, 60), Color.Black);
            spriteBatch.DrawString(Font1, "State: " + tickState + " (" + Config.State + ")", new Vector2(0, 80), Color.Black);
            //tickState

            renderer.run(Entities,balls,gameTime,spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

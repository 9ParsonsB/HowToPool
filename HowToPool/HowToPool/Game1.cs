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

        List<Entity> Entities = new List<Entity>();
        static List<Ball> balls = new List<Ball>();
        List<Ball> tempBalls = new List<Ball>();
        
        //Entity player = new Entity("Defenceship",new Vector2(0,0));

        public static Random rnd = new Random();

        private bool cWasUp = true;

        private bool rWasUp = true;

        private bool pgWasUp = true;

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

            

            for (int i = 1; i < 100; i++)
            {
                float a = (float)rnd.Next(minv, maxv);
                float b = (float)rnd.Next(minv, maxv);
                float x = (float)rnd.Next(0, graphics.PreferredBackBufferWidth);
                float y = (float)rnd.Next(0, graphics.PreferredBackBufferHeight);
                if (i % 2 == 0)
                {
                    tempBalls.Add(new Ball("redBall", new Vector3(0, 0, 0), 13, 100, new Vector2(a, b), new Vector2(x, y)));
                }
                else
                {
                    tempBalls.Add(new Ball("blueBall", new Vector3(0, 0, 0), 13, 100, new Vector2(a, b), new Vector2(x, y)));
                }

            }


            
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

            
            foreach (Ball i in tempBalls.ToArray())
            {
                balls.Add(i);
            }


            base.Initialize();
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

            renderer.ContentLoad(Entities,balls,Content);

            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            Viewport viewport = graphics.GraphicsDevice.Viewport;

           
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //Entities[0].update(Entities, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) { 
                balls[0].pos.X = 100;
            }
            
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

            Vector2 FontOrigin = new Vector2(0, 0);

            //spriteBatch.DrawString(Font1, balls[0].vel.ToString(), new Vector2(150, 150), Color.Black, 0, FontOrigin, 2.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Font1, "Collisions: " + Config.shouldCollide, FontPos, Color.Black, 0, new Vector2(200,150), 2.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Font1, "Resistance (" + Config.resistnace.ToString() + "): "  + Config.shouldResist, FontPos, Color.Black, 0, new Vector2(200, 130), 2.0f, SpriteEffects.None, 0.5f);



            renderer.run(Entities,balls,gameTime,spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

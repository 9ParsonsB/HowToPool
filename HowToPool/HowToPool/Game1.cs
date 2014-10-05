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
        List<Ball> balls = new List<Ball>();
        
        //Entity player = new Entity("Defenceship",new Vector2(0,0));

        public static Random rnd = new Random();

        static int minv = -50;
        static int maxv = 50;


        //Random numebrs(temporary but fun)
        static float a = (float)rnd.Next(minv, maxv);
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

        Ball redBall3 = new Ball("redBall", new Vector3(100, 50, 0), 10, 100, new Vector2(a4, a5), new Vector2(0, 500));

        static float a5 = (float)rnd.Next(minv, maxv);
        static float b5 = (float)rnd.Next(minv, maxv);

        Ball blueBall3 = new Ball("blueBall", new Vector3(500, 150, 0), 10, 100, new Vector2(a5, -b5), new Vector2(1200, 150));


        Renderer renderer = new Renderer();

        SpriteFont Font1;
        Vector2 FontPos;
        bool isDebug = true;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 700;

            Content.RootDirectory = "Content";
            
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
            balls.Add(redBall);
            balls.Add(blueBall);
            balls.Add(redBall2);
            balls.Add(blueBall2);
            balls.Add(redBall3);
            balls.Add(blueBall3);

            



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
                isDebug = false;
                balls[0].pos.X = 100;
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

            spriteBatch.DrawString(Font1, balls[0].vel.ToString(), FontPos, Color.Black,0, FontOrigin, 2.0f, SpriteEffects.None, 0.5f);




            renderer.run(Entities,balls,gameTime,spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

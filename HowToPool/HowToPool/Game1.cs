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
        List<Entity.Ball> balls = new List<Entity.Ball>();
        
        //Entity player = new Entity("Defenceship",new Vector2(0,0));

        Entity.Ball redBall = new Entity.Ball("redBall", new Vector3(100,100,0),40,100,new Vector2(3, 2),new Vector2(100,240));
        Entity.Ball blueBall = new Entity.Ball("blueBall", new Vector3(200, 100, 0), 40, 100,new Vector2(-5, -2), new Vector2(700, 300));

        Entity.Ball redBall2 = new Entity.Ball("redBall", new Vector3(100, 50, 0), 40, 1, new Vector2(5, 0), new Vector2(600, 1000));
        Entity.Ball blueBall2 = new Entity.Ball("blueBall", new Vector3(500, 150, 0), 40, 1, new Vector2(-5, 0), new Vector2(500, 150));


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
            //balls.Add(redBall2);
            //balls.Add(blueBall2);

            



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

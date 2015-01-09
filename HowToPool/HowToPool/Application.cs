using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HowToPool
{
    class Application
    {

        private Texture2D redBall;
        private Texture2D blueBall;
        private Texture2D whiteBall;

        public Cue cue = new Cue();
        public List<Ball> balls = new List<Ball>();

        public Application() { }

        public void StartGame()
        {


            /* for (int i = 0; i < 100; i++)
             {
                 float a = (float)rnd.Next(minv, maxv);
                 float b = (float)rnd.Next(minv, maxv);
                 float x = (float)rnd.Next(0, graphics.PreferredBackBufferWidth);
                 float y = (float)rnd.Next(0, graphics.PreferredBackBufferHeight);
                 if (i % 2 == 0)
                 {
                     balls.Add(new Ball(redBall, new Vector3(0, 0, 0), 13, 100, new Vector2(a, b), new Vector2(x, y)));
                 }
                 else
                 {
                     balls.Add(new Ball(blueBall, new Vector3(0, 0, 0), 13, 100, new Vector2(a, b), new Vector2(x, y)));
                 }

             }*/

            

            float mass = 50;

            //Creates all balls for game

            //Need to be first(White ball)
            balls.Add(new Ball(whiteBall, 12.5f, mass, new Vector2(500, Config.height / 2)));

            balls.Add(new Ball(redBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3), Config.height / 2)));

            balls.Add(new Ball(redBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 29, (Config.height / 2) - 14)));
            balls.Add(new Ball(blueBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 29, (Config.height / 2) + 14)));

            balls.Add(new Ball(blueBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 57, (Config.height / 2))));
            balls.Add(new Ball(blueBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 57, (Config.height / 2) - 27)));
            balls.Add(new Ball(redBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 57, (Config.height / 2) + 27)));

            balls.Add(new Ball(blueBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 85, (Config.height / 2) - 14)));
            balls.Add(new Ball(redBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 85, (Config.height / 2) - 41)));
            balls.Add(new Ball(redBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 85, (Config.height / 2) + 14)));
            balls.Add(new Ball(blueBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 85, (Config.height / 2) + 41)));

            balls.Add(new Ball(redBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2))));
            balls.Add(new Ball(blueBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2) - 27)));
            balls.Add(new Ball(blueBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2) - 55)));
            balls.Add(new Ball(blueBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2) + 27)));
            balls.Add(new Ball(redBall, 12.5f, mass, new Vector2(Config.width - (Config.width / 3) + 113, (Config.height / 2) + 55)));

            cue = new Cue(cue.texture, new Vector2(0, 0), new Vector2(500,500), 0);

            cue.drawCue = true;

        }

        public void clearBalls()
        {
            foreach (Ball b in balls.ToArray())
            {
                balls.Remove(b);
            }

            cue.drawCue = false;

        }

        public void ContentLoad(Renderer renderer,List<Entity> Entities,PoolTable table,ContentManager Content) 
        {
            List<Texture2D> textures = renderer.ContentLoad(Entities, balls, cue,table, Content);

            blueBall = textures[0];
            redBall = textures[1];
            whiteBall = textures[2];

            table.pos = new Vector2(0, 0);
            table.drawTable = true;


        }


    }
}

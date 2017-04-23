using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HowToPool
{
    class mainUpdate
    {
        Config Config = new Config();


        public void run(List<Entity> Entities, List<Ball> balls,Cue cue,MouseCursor mouseHandler,GameTime gameTime) 
        {

            //Console.WriteLine(Entities.ToArray().Length);

            for (int i = 0; i < Entities.ToArray().Length; i++)
            {
                Entities[i].update(gameTime);
            }


            //Console.WriteLine(balls.ToArray().Length);

            for (int i = 0; i < balls.ToArray().Length; i++) 
            {
                balls[i].ballUpdate(balls,i,gameTime);

            }

            if (balls.Count > 0)
            {
                cue.update(gameTime, mouseHandler, balls);
            }

            mouseHandler.update(gameTime);

        }

    }

}

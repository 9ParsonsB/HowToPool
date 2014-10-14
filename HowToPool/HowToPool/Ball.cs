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
    class Ball :  Entity
    {
        public float mass;

        public BoundingSphere sphere;


        public Ball(string path, Vector3 center, float radius, float _mass, Vector2 _vel, Vector2 _pos) : base(path, _vel, _pos)
        {
            sphere = new BoundingSphere(center, radius);

            mass = _mass;



        }

        public void ballUpdate(List<Ball> balls, int i, GameTime gameTime)
        {

            //Console.WriteLine(this.pos);
            //Console.WriteLine(this.vel);

            this.pos = this.pos + this.vel;

            if (this.vel.X < 0) { this.vel.X += Config.resistnace; }
            if (this.vel.X > 0) { this.vel.X -= Config.resistnace; }

            if (this.vel.Y < 0) { this.vel.Y += Config.resistnace; }
            if (this.vel.Y > 0) { this.vel.Y -= Config.resistnace; }

            if (this.vel.X > -0.1 && this.vel.X < 0.1) { this.vel.X = 0; }
            if (this.vel.Y > -0.1 && this.vel.Y < 0.1) { this.vel.Y = 0; }

            Math.Round(vel.X, 1);
            Math.Round(vel.Y, 1);

            if (this.pos.X < 0 || this.pos.X > 1200 - this.texture.Width)
            {
                this.vel.X *= -1;
                if (this.pos.X < 0)
                {
                    this.pos.X = 1;
                }
                else
                {
                    this.pos.X = 1199 - this.texture.Width;
                }

            }

            if (this.pos.Y < 0 || this.pos.Y > 700 - this.texture.Height)
            {
                this.vel.Y *= -1;

                if (this.pos.Y < 0)
                {
                    this.pos.Y = 1;
                }
                else
                {
                    this.pos.Y = 699 - this.texture.Height;
                }

            }

            this.sphere.Center = new Vector3(this.pos.X, this.pos.Y, 0);


            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;


           


        }
        



        public bool colliding(Ball ball)
        {
            float xd = this.pos.X - ball.pos.X;
            float yd = this.pos.Y - ball.pos.Y;

            float sumRadius = this.sphere.Radius + ball.sphere.Radius;
            float sqrRadius = sumRadius * sumRadius;

            float distSqr = (xd * xd) + (yd * yd);

            if (distSqr <= sqrRadius) // causing errors as should not return true
            {
                return true;
            }

            return false;
        }







        

    }
}

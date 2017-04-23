using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HowToPool
{
    class Player
    {
        public string name; //TODO: give use chance to input name
        //public Entity Entity;
        public int BallsRemaining;
        public string colour;
        public bool isTurn;

      

    }

    class State 
    {
        public string name;
    }


    class DrawString
    {
        public SpriteFont Font;
        public string Text;
        public Vector2 Position;
        public Color TextColor;
        public string id;

        public DrawString(string _id,SpriteFont _Font, string _Text, Vector2 _Position)
        {
            id = _id;
            Font = _Font;
            Text = _Text;
            Position = _Position;
            TextColor = Color.Black;
        }
    }
    
    class Entity
    {

        public Vector2 vel;

        public Vector2 pos;

        public Vector2 origin;

        public float angle;

        public String fileName;

        public Texture2D texture;

        public float mass;


        public Entity(Texture2D _texture,Vector2 _vel,Vector2 _pos,float _angle = 0,float _mass = 30) 
        {
            mass = _mass;

            texture = _texture;

            vel.X = _vel.X;
            vel.Y = _vel.Y;

            pos = _pos;
            
        
        }

        public Entity() { }

        public virtual void update(GameTime gameTime) 
        {
            //Console.WriteLine(string.Format("INDEX:"+index+". POS: X:"+this.pos.X+" Y:"+this.pos.Y+". VEL: X:"+this.vel.X+" Y:"+ this.vel.Y ));
            this.pos = this.pos + this.vel;
            //Console.WriteLine(string.Format("INDEX:" + index + ". POS: X:" + this.pos.X + " Y:" + this.pos.Y + ". VEL: X:" + this.vel.X + " Y:" + this.vel.Y));

        }

        public void draw(SpriteBatch spriteBatch)
        {
          
            spriteBatch.Draw(this.texture, this.pos, null, Color.White, this.angle, this.origin, 1f, SpriteEffects.None, 0);

        }

        public void resistance() 
        {
            float tempX = this.vel.X;
            float tempY = this.vel.Y;

            if (Config.shouldResist)
            {
                
                if (this.vel.X != 0)
                {
                    if (this.vel.X < 0) { this.vel.X += this.mass * Config.resistance; }

                    if (this.vel.X > 0 && tempX == this.vel.X) 
                    {
                        this.vel.X -= this.mass * Config.resistance;
                    }
                }

                if (this.vel.Y != 0)
                {
                    if (this.vel.Y < 0) { this.vel.Y += this.mass / Config.resistance; }
                    if (this.vel.Y > 0 && tempY == this.vel.Y) { this.vel.Y -= this.mass * Config.resistance; }
                }
            }

            if (tempX > 0 && this.vel.X < 0 || tempX < 0 && this.vel.X > 0)
            {
                this.vel.X = 0;
            }

            if (tempY > 0 && this.vel.Y < 0 || tempY < 0 && this.vel.Y > 0)
            {
                this.vel.Y = 0;
            }

            this.vel.X = (float)Math.Round(this.vel.X, 4);
            this.vel.Y = (float)Math.Round(this.vel.Y, 4);

            this.pos.X = (float)Math.Round(this.pos.X, 4);
            this.pos.Y = (float)Math.Round(this.pos.Y, 4);

        }

        public void resolveCollision(Entity other,BoundingSphere sphere,BoundingSphere otherSphere)
        {
            // get the mtd
            Vector2 delta = (pos - other.pos);
            float d = delta.Length();
            //minimum translation distance to push balls apart after intersecting
            Vector2 mtd = delta * (((sphere.Radius + otherSphere.Radius) - d) / d);

            // resolve intersection --
            // inverse mass quantities
            float im1 = 1 / this.mass;
            float im2 = 1 / other.mass;

            // push-pull them apart based off their mass
            this.vel = this.vel + (mtd * (im1 / (im1 + im2)));
            other.vel = other.vel - (mtd * (im2 / (im1 + im2)));

            this.vel.X = (float)Math.Round(this.vel.X, 4);
            this.vel.Y = (float)Math.Round(this.vel.Y, 4);

            if (float.IsNaN(this.pos.X) || float.IsNaN(this.pos.Y))
            {
                Console.WriteLine("Error occured. Position is NaN");
            }

            other.vel.X = (float)Math.Round(other.vel.X, 4);
            other.vel.Y = (float)Math.Round(other.vel.Y, 4);

            // impact speed
            Vector2 v = (this.vel - (other.vel));
            v.Normalize();//Normalizes vector then converts to a single value.

            float vn = Vector2.Dot(v, v);

            //Sphere intersecting but moving away from each other already
            if (vn > 0.0f) return;

            //Collision impulse
            float i = (-(1.0f + Config.resistance) * vn) / (im1 + im2);
            Vector2 impulse = mtd * i;

            // change in momentum
            this.vel = this.vel + (impulse * im1);
            other.vel = other.vel - (impulse * im2);

        }

       

      


       



    }
}

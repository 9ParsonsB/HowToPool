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
    class MouseCursor : Entity
    {
        //Bounding sphere for box
        public BoundingSphere box;

        public MouseCursor() : base()
        {

        }

        public override void update(GameTime gameTime)
        {
            //Mouse updating goes here
            box.Center = new Vector3(Mouse.GetState().X,Mouse.GetState().Y,0);    


        }


    }
}

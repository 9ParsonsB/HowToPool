﻿using System;
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
    class Update
    {
        public List<Entity> Entities;

        public void Run(GameTime gameTime) 
        {
            for (int i = 0; i < Entities.Count; i++) 
            {
                Entities[i].update(Entities,i);
            }


        }
        


    }
}

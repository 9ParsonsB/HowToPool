﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HowToPool
{
    class Config
    {
        private State currentState = new State();
        public List<Entity> Entities { get; set; }

        public List<Entity.Ball> balls { get; set; }

        private static float _resistance = 0.1f;
        public static float resistnace
        {
            get { return _resistance; }
        }
        
    }
}

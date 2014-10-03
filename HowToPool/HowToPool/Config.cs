using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HowToPool
{
    class Config
    {
        private static State currentState = new State();
        public static List<Entity> Entities { get; set; }

        public static List<Entity.Ball> balls { get; set; }

        private static float _resistance = 10.0f;
        public static float resistnace
        {
            get { return _resistance; }
        }
        
    }
}

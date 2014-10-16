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
        public static string State { get; set; }
        public static int Selected { get; set; }
        public static bool shouldCollide { get; set; }
        public static bool shouldResist { get; set; }
        public static List<Ball> balls { get; set; }

        private static float _resistance = 0.05f; //usually 0.05f

        private static float _maxVel = 50f;


        public static float resistnace
        {
            get { return _resistance; }

            set { _resistance = value; }
        }

        public static float maxVel
        {
            get { return _maxVel; }
        }
        
    }
}

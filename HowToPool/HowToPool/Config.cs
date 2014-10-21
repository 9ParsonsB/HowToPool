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
        public static int maxFPS { get; set; }
        public static int Selected { get; set; }
        public static int fov { get; set; }
        public static bool shouldCollide { get; set; }
        public static bool shouldResist { get; set; }
        public static bool SALE { get; set; }
        public static bool soundEnabled { get; set; }

        public static List<Ball> balls { get; set; }

        private static float _resistance = 0.05f; //usually 0.05f

        private static float _maxVel = 50f;

        private static string _State;

        public static string State
        {
            get { return _State; }
            set { Selected = 0; _State = value; }
        }

        public static float resistance
        {
            get { return _resistance; }

            set { _resistance = (float)Math.Round(value,2); }
        }

        public static float maxVel
        {
            get { return _maxVel; }
        }
        
    }
}

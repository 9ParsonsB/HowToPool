using System;

namespace HowToPool
{
    class Config
    {
        public const string RootDirectory = "Content/";
        private static State currentState = new State();
        public static int Selected { get; set; }
        public static int fov { get; set; }
        public static bool shouldCollide { get; set; }
        public static bool shouldResist { get; set; }
        public static bool sale { get; set; }
        public static bool soundEnabled { get; set; }

        // public static float g = (float)6.673 * ((MathF.Pow(10, -11)));
        public static float maxVel { get; } = 50.0f;
        public static int width = 1200;
        public static int height = 700;

        private static float _resistance = 0.0020f; // usually 0.05f
        private static string _State;

        public static string State
        {
            get { return _State; }
            set { Selected = 0; _State = value; }
        }

        public static float resistance
        {
            get { return _resistance; }
            set { _resistance = MathF.Round(value, 2); }
        }
    }
}

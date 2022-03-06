using Raylib_cs;

namespace HowToPool
{
    static class Program
    {
        public static unsafe void Main()
        {
            // Initialization
            //----------------------------------------------------------------------------------
            Raylib.SetTraceLogCallback(&Logging.LogConsole);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_MSAA_4X_HINT);
            Raylib.InitWindow(Config.width, Config.height, "How To Pool");

            Game1 game = new Game1();
            game.LoadContent();
            game.Load();
            //----------------------------------------------------------------------------------

            while (!Raylib.WindowShouldClose())
            {
                // Update
                //----------------------------------------------------------------------------------
                game.Update(Raylib.GetFrameTime());
                //----------------------------------------------------------------------------------

                // Draw
                //----------------------------------------------------------------------------------
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                game.Draw();

                Raylib.EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            game.Unload();
            Raylib.CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}

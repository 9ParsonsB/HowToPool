using System.Numerics;
using Raylib_cs;

namespace HowToPool
{
    class State
    {
        public string name;
    }

    public class Game1
    {
        private World world = new World();
        private string tickState = Config.State;
        private int tickSelected = Config.Selected;
        private Texture2D cueTexture;
        private Texture2D tableTexture;
        private Texture2D ballTexture;

        public void Load()
        {
            Config.State = "mainMenu";
            Config.Selected = 0;
            Config.shouldCollide = true;
            Config.soundEnabled = true;
            Config.fov = 20;

            world.Load();
        }

        public void LoadContent()
        {
            cueTexture = Raylib.LoadTexture(Config.RootDirectory + "cue.png");
            tableTexture = Raylib.LoadTexture(Config.RootDirectory + "table.png");
            ballTexture = Raylib.LoadTexture(Config.RootDirectory + "ball.png");
        }

        public void Unload()
        {
            Raylib.UnloadTexture(cueTexture);
            Raylib.UnloadTexture(tableTexture);
            Raylib.UnloadTexture(ballTexture);
        }

        public void Update(float dt)
        {
            tickState = Config.State;
            tickSelected = Config.Selected;

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                world.entities[0].position.X = 100;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
            {
                if (Config.shouldCollide)
                {
                    Config.shouldCollide = false;
                }
                else
                {
                    Config.shouldCollide = true;
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_PAGE_UP))
            {
                Config.resistance = Config.resistance + 0.05f;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_PAGE_DOWN))
            {
                Config.resistance = Config.resistance - 0.05f;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                if (Config.shouldResist)
                {
                    Config.shouldResist = false;
                }
                else
                {
                    Config.shouldResist = true;
                }
            }

            world.Update(dt);
        }

        public void Draw()
        {
            // Draw table
            Raylib.DrawTexture(tableTexture, 0, 0, Color.WHITE);

            // Draw balls
            var src = new Rectangle(0, 0, ballTexture.width, ballTexture.height);
            var dest = new Rectangle(0, 0, 20, 20);
            var origin = new Vector2(dest.width / 2, dest.height / 2);

            foreach (var entity in world.entities)
            {
                dest.x = entity.position.X;
                dest.y = entity.position.Y;
                Raylib.DrawTexturePro(ballTexture, src, dest, origin, 0.0f, Color.WHITE);
            }

            // Draw cue
            if (world.cue.drawCue)
            {
                Raylib.DrawTexture(cueTexture, 100, 200, Color.WHITE);
            }

            // Draw mouse cursor
            Raylib.DrawCircleLines(Raylib.GetMouseX(), Raylib.GetMouseY(), 5, Color.GRAY);

            Raylib.DrawFPS(10, 10);

            Raylib.DrawText("Collisions: " + Config.shouldCollide, 10, 40, 14, Color.RAYWHITE);
            Raylib.DrawText("Resistance (" + Config.resistance.ToString() + "): " + Config.shouldResist, 10, 60, 14, Color.RAYWHITE);
            Raylib.DrawText("State: " + Config.State + " (" + Config.State + ")", 10, 100, 14, Color.RAYWHITE);
        }
    }
}

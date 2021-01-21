using Raylib_cs;

namespace HowToPool
{
    class State
    {
        public string name;
    }

    public class Game1
    {
        World world = new World();
        // Menu menu = new Menu();
        // private List<DrawString> currentMenu;
        string tickState = Config.State;
        int tickSelected = Config.Selected;

        Texture2D cueTexture;
        Texture2D tableTexture;

        /// <summary>
        /// Perform any loading it needs to before starting to run.
        /// </summary>
        public void Load()
        {
            Config.State = "mainMenu";
            Config.Selected = 0;
            Config.shouldCollide = true;
            // Config.shouldResist = true;
            Config.soundEnabled = true;
            Config.fov = 20;

            world.Load();
        }

        /// <summary>
        /// Called once per game for loading game-specific content.
        /// </summary>
        public void LoadContent()
        {
            cueTexture = Raylib.LoadTexture(Config.RootDirectory + "cue.png");
            tableTexture = Raylib.LoadTexture(Config.RootDirectory + "table.png");

            // Note : All drawstrings will be in a list corrosponding to what part of the menu their in
            /*DrawString test = new DrawString("play", Font1, "Play", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));

            // List of strings for menu
            List<DrawString> _MenuStrings = new List<DrawString>();

            // Sets up title that is displayed to user
            DrawString TitleString = new DrawString("title", Font1, "How to pool", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) - 40));

            // Adds menu strings that the user can interact with
            _MenuStrings.Add(new DrawString("play", Font1, "Play", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            _MenuStrings.Add(new DrawString("settings", Font1, "Settings", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 40)));
            _MenuStrings.Add(new DrawString("quit", Font1, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 80)));

            List<DrawString> _SettingsMenuStrings = new List<DrawString>();

            // Adds menu strings to list for settings menu
            _SettingsMenuStrings.Add(new DrawString("sound", Font1, "Sound: " + Config.soundEnabled, new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2))));
            _SettingsMenuStrings.Add(new DrawString("fps", Font1, "FPS limit: " + Config.maxFPS, new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 20)));
            _SettingsMenuStrings.Add(new DrawString("fov", Font1, "FOV " + (Config.fov + 50).ToString(), new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 40)));
            _SettingsMenuStrings.Add(new DrawString("sale", Font1, "Sale? " + Config.SALE, new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 60)));

            // Create menu
            menu = new Menu(TitleString, _MenuStrings, _SettingsMenuStrings);
            Viewport viewport = graphics.GraphicsDevice.Viewport;

            /*Menu.mainMenu.Add(new DrawString("play",Font1, "Play", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            Menu.mainMenu.Add(new DrawString("settings",Font1, "Settings", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 40)));
            Menu.mainMenu.Add(new DrawString("quit",Font1, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) + 80)));

            Menu.settingsMenu.Add(new DrawString("sound",Font1, "Sound: " + Config.soundEnabled,new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2))));
            Menu.settingsMenu.Add(new DrawString("sale",Font1, "Sale? " + Config.SALE,new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+60)));
            Menu.settingsMenu.Add(new DrawString("fps",Font1, "FPS limit: " + Config.maxFPS, new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+20)));
            Menu.settingsMenu.Add(new DrawString("fov",Font1, "FOV " + (Config.fov + 50).ToString(), new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2)+40)));*/
        }

        /// <summary>
        /// Called once per game and is the place to unload game-specific content.
        /// </summary>
        public void Unload()
        {
            Raylib.UnloadTexture(cueTexture);
            Raylib.UnloadTexture(tableTexture);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        public void Update(float dt)
        {
            tickState = Config.State;
            tickSelected = Config.Selected;

            /*if (tickState == "startSPGame")
            {
                StartGame();
                Config.State = "SPGame";
            }*/

            // menu.UpdateMenus(application);

            /*if (Keyboard.GetState().IsKeyDown(Keys.Enter)) {
                balls[0].pos.X = 100;
            }*/

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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        public void Draw()
        {
            // Draw table
            Raylib.DrawTexture(tableTexture, 0, 0, Color.WHITE);

            // Draw balls
            foreach (var entity in world.entities)
            {
                Raylib.DrawCircleV(entity.position, 10, Color.WHITE);
            }

            // Draw cue
            if (world.cue.drawCue)
            {
                Raylib.DrawTexture(cueTexture, 100, 200, Color.WHITE);
            }

            // Draw mouse cursor
            Raylib.DrawCircleLines(Raylib.GetMouseX(), Raylib.GetMouseY(), 5, Color.GRAY);

            Raylib.DrawFPS(10, 10);

            // spriteBatch.DrawString(Font1, balls[0].vel.ToString(), new Vector2(150, 150), Color.Black, 0, FontOrigin, 2.0f, SpriteEffects.None, 0.5f);
            // spriteBatch.DrawString(Font1, "FPS: " + (1 / (Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds))).ToString(), new Vector2(0, 0), Color.Black);
            /*spriteBatch.DrawString(Font1, "Collisions: " + Config.shouldCollide, new Vector2(0, 20), Color.Black);
            spriteBatch.DrawString(Font1, "Resistance (" + Config.resistance.ToString() + "): " + Config.shouldResist, new Vector2(0, 40), Color.Black);
            spriteBatch.DrawString(Font1, "Selected: " + menu.selected.ToString(), new Vector2(0, 60), Color.Black);
            spriteBatch.DrawString(Font1, "State: " + Config.State + " (" + Config.State + ")", new Vector2(0, 80), Color.Black);
            // spriteBatch.DrawString(Font1, "State: " + Config.State + " (" + Application.cue + ")", new Vector2(0, 100), Color.Black);

            menu.DrawMenus(spriteBatch);*/
        }
    }
}

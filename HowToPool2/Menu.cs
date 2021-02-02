using System;
using Raylib_cs;

namespace HowToPool
{
    class Menu : SettingsMenu
    {
        public int selected;

        // Constructor for menu. Currently takes array of DrawStrings for each menu set
        public Menu()
        {
            // Sets main menu draw strings
            // MenuStrings = _MenuStrings;
            selected = 0;
            Config.State = "main";
            // MenuStrings[selected].TextColor = Color.Red;
            // TitleString = _TitleString;
        }

        // Draws all menus that need drawing
        public void DrawMenus()
        {
            switch (Config.State)
            {
                // Draws main menu if needed
                case "main":
                    DrawMainMenu();
                    break;

                // Draws settings menu if needed
                case "settings":
                    DrawSettingsMenu();
                    break;
            }
        }

        // Updates all menus that need updating
        public void UpdateMenus(Game1 game)
        {
            switch (Config.State)
            {
                // Draws main menu if needed
                case "main":
                    UpdateMainMenu(game);
                    break;

                // Draws settings menu if needed
                case "settings":
                    UpdateSettingsMenu();
                    break;
            }

            // Allows game to exit and go to menu
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE) && Config.State == "startSPGame")
            {
                Config.State = "main";
                // game.ClearBalls();
            }
        }

        // Draws main menu
        public void DrawMainMenu()
        {

        }

        // Draws settings menu
        public void DrawSettingsMenu()
        {

        }

        // Updates main menu
        public void UpdateMainMenu(Game1 game)
        {
            // Makes sure numbers are valid
            if (selected < 0)
            {
                selected = 0;
            }

            // if (selected > MenuStrings.Count - 1)
            {
                // selected = MenuStrings.Count - 1;
            }

            // If user isn't at top of menu
            if (selected != 0)
            {
                // Check if they attempt to move up menu
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP) && changeSelectUp == true)
                {
                    selected -= 1;
                    changeSelectUp = false;

                    // Sets current selection to red
                    // MenuStrings[selected].TextColor = Color.Red;

                    // Sets old selection back to default
                    // MenuStrings[selected + 1].TextColor = Color.Black;
                }

                // Selection can change once keys are up
                if (Raylib.IsKeyUp(KeyboardKey.KEY_W) && Raylib.IsKeyUp(KeyboardKey.KEY_UP))
                {
                    changeSelectUp = true;
                }
            }

            // if (selected < (SettingsMenuStrings.Count - 1))
            {
                // Check if user attempts to move down menu
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN) && changeSelectDown == true)
                {
                    selected += 1;

                    changeSelectDown = false;
                    // if (selected >= MenuStrings.Count)
                    // selected = MenuStrings.Count - 1;
                    // Sets current selection to red

                    // MenuStrings[selected].TextColor = Color.Red;

                    // Sets old selection back to default
                    // MenuStrings[selected - 1].TextColor = Color.Black;
                }

                // Selection can change once keys are up
                if (Raylib.IsKeyReleased(KeyboardKey.KEY_S) && Raylib.IsKeyReleased(KeyboardKey.KEY_DOWN))
                {
                    changeSelectDown = true;
                }
            }

            // Handles menu option events
            MenuEvents(game);
        }

        // Handles events for main menu
        public void MenuEvents(Game1 game)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) && Config.State != "startSPGame")
            {
                switch (selected)
                {
                    case 0:
                        // game.StartGame();
                        Config.State = "startSPGame";
                        break;
                    case 1:
                        Config.State = "settings";
                        break;
                    case 2:
                        Config.State = "quit";
                        break;
                }
            }
        }
    }
}

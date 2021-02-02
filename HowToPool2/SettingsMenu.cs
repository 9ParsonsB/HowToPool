using Raylib_cs;

namespace HowToPool
{
    class SettingsMenu
    {
        public int settingSelected = 0;
        public bool changeSelectUp = true;
        public bool changeSelectDown = true;

        public void UpdateSettingsMenu()
        {
            // Makes sure numbers are valid
            if (settingSelected < 0)
            {
                settingSelected = 0;
            }

            // if (settingSelected > SettingsMenuStrings.Count - 1)
            {
                // settingSelected = SettingsMenuStrings.Count - 1;
            }

            // If user isn't at top of menu
            if (settingSelected != 0)
            {
                // Check if they attempt to move up menu
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP) && changeSelectUp == true)
                {
                    settingSelected -= 1;
                    changeSelectUp = false;

                    // Sets current selection to red
                    // SettingsMenuStrings[settingSelected].TextColor = Color.Red;

                    // Sets old selection back to default
                    // SettingsMenuStrings[settingSelected + 1].TextColor = Color.Black;
                }

                // Selection can change once keys are up
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
                {
                    changeSelectUp = true;
                }
            }

            /*if (settingSelected < SettingsMenuStrings.Count - 1)
            {
                // Check if user attempts to move down menu
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_S) || Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN) && changeSelectDown)
                {
                    settingSelected += 1;
                    changeSelectDown = false;

                    // Sets current selection to red
                    // SettingsMenuStrings[settingSelected].TextColor = Color.Red;

                    // Sets old selection back to default
                    // SettingsMenuStrings[settingSelected - 1].TextColor = Color.Black;
                }

                // Selection can change once keys are up
                if (Raylib.IsKeyReleased(KeyboardKey.KEY_DOWN))
                {
                    changeSelectDown = true;
                }
            }*/

            // Allows user to return to main menu from the settings menu
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                Config.State = "main";
            }
        }
    }
}

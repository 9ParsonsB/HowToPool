using System.Collections.Generic;

namespace HowToPool
{
    class SettingsMenu
    {
        public List<DrawString> SettingsMenuStrings = new List<DrawString>();
        public int settingSelected = 0;
        public bool changeSelectUp = true;
        public bool changeSelectDown = true;

        public SettingsMenu(List<DrawString> _SettingsMenuStrings) 
        {
            SettingsMenuStrings = _SettingsMenuStrings;
        }

        public SettingsMenu() { }

        public void DrawSettingsMenu(SpriteBatch spriteBatch) 
        {
            // Iterate through all strings that need to be drawn for settings
            foreach (DrawString setting in SettingsMenuStrings.ToArray()) 
            {
                spriteBatch.DrawString(setting.Font, setting.Text, setting.Position, setting.TextColor);
            }
        }

        public void UpdateSettingsMenu()
        {
            // Makes sure numbers are valid
            if (settingSelected < 0) { settingSelected = 0; }
            if (settingSelected > SettingsMenuStrings.Count - 1) { settingSelected = SettingsMenuStrings.Count - 1; }

            // If user isn't at top of menu
            if (settingSelected != 0)
            {
                // Check if they attempt to move up menu
                if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && changeSelectUp == true)
                {
                    settingSelected -= 1;
                    changeSelectUp = false;

                    // Sets current selection to red
                    SettingsMenuStrings[settingSelected].TextColor = Color.Red;

                    // Sets old selection back to default
                    SettingsMenuStrings[settingSelected + 1].TextColor = Color.Black;
                }

                // Selection can change once keys are up
                if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.Up))
                {
                    changeSelectUp = true;
                }
            }

            if (settingSelected < SettingsMenuStrings.Count - 1)
            {
                // Check if user attempts to move down menu
                if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && changeSelectDown == true)
                {
                    settingSelected += 1;
                    changeSelectDown = false;

                    // Sets current selection to red
                    SettingsMenuStrings[settingSelected].TextColor = Color.Red;

                    // Sets old selection back to default
                    SettingsMenuStrings[settingSelected - 1].TextColor = Color.Black;
                }

                // Selection can change once keys are up
                if (Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.Down))
                {
                    changeSelectDown = true;
                }
            }

            // Allows user to return to main menu from the settings menu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Config.State = "main";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HowToPool
{

    class Menu : SettingsMenu
    {
       
        public int selected;

        List<DrawString> MenuStrings = new List<DrawString>();


        //Constructor for menu. Currently takes array of DrawStrings for each menu set
        public Menu(List<DrawString> _MenuStrings,List<DrawString> _SettingsMenuStrings) : base(_SettingsMenuStrings)
        {
            //Sets main menu draw strings
            MenuStrings = _MenuStrings;

            selected = 0;

            Config.State = "main";

            MenuStrings[selected].TextColor = Color.Red;

           
        }

        public Menu(){ }

        //Draws all menus that need drawing
        public void DrawMenus(SpriteBatch spriteBatch)
        {
            switch (Config.State)
            {
                //Draws main menu if needed
                case "main":
                    DrawMainMenu(spriteBatch);

                    break;

                //Draws settings menu if needed
                case "settings":

                    DrawSettingsMenu(spriteBatch);

                    break;

            }

        }

        //Updates all menus that need updating
        public void UpdateMenus(Application application) 
        {
       
            switch (Config.State)
            {
                //Draws main menu if needed
                case "main":
                    UpdateMainMenu(application);

                    break;

                //Draws settings menu if needed
                case "settings":

                    UpdateSettingsMenu();

                    break;

            }

            //Allows game to exit and go to menu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && Config.State == "startSPGame")
            {
                Config.State = "main";
                application.clearBalls();
            }

        }

        //Draws main menu
        public void DrawMainMenu(SpriteBatch spriteBatch) 
        {
            //Iterate through all strings that need to be drawn for play menu
            foreach (DrawString setting in MenuStrings.ToArray())
            {
                spriteBatch.DrawString(setting.Font, setting.Text, setting.Position, setting.TextColor);
            }
        }

        //Updates main menu
        public void UpdateMainMenu(Application application) 
        {

            //Makes sure numbers are valid
            if (selected < 0) { selected = 0; }
            if (selected > MenuStrings.Count - 1) { selected = MenuStrings.Count - 1; }

            //If user isn't at top of menu
            if (selected != 0)
            {
                //Check if they attempt to move up menu
                if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && changeSelectUp == true)
                {
                    selected -= 1;

                    changeSelectUp = false;

                    //Sets current selection to red
                    MenuStrings[selected].TextColor = Color.Red;

                    //Sets old selection back to default
                    MenuStrings[selected + 1].TextColor = Color.Black;

                }

                //Selection can change once keys are up
                if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.Up))
                {
                    changeSelectUp = true;
                }

            }



            if (selected < (SettingsMenuStrings.Count - 1))
            {

                //Check if user attempts to move down menu
                if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && changeSelectDown == true)
                {
                    selected += 1;

                    changeSelectDown = false;
                    if (selected >= MenuStrings.Count) 
                        selected = MenuStrings.Count - 1;
                    //Sets current selection to red
                    
                    MenuStrings[selected].TextColor = Color.Red;

                    //Sets old selection back to default
                    MenuStrings[selected - 1].TextColor = Color.Black;

                }

                //Selection can change once keys are up
                if (Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.Down))
                {
                    changeSelectDown = true;
                }
            }

            //Handles menu option events
            MenuEvents(application);

        }

        //Handles events for main menu
        public void MenuEvents(Application application) 
        {
           
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && Config.State != "startSPGame")
            {
                switch (selected)
                {
                    case 0:
                        application.StartGame();
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







    /*static class Menu
    {
        private static bool wWasUp = true;
        private static bool sWasUp = true;
        private static bool enterWasUp = true;

        static Color playColor = Color.Black;
        static Color quitColor = Color.Black;

        public static List<DrawString> mainMenu = new List<DrawString>();
        public static List<DrawString> settingsMenu = new List<DrawString>();
        private static List<DrawString> currentMenu;


        public static void drawMenu(string tickState, SpriteBatch spriteBatch)
        {
            if (tickState == "mainMenu") { currentMenu = Menu.mainMenu; } 
            //if the current state of the menu for this tick is mainMenu then set the current menu item to the Mainmenu
            if (tickState == "settingMenu") { currentMenu = Menu.settingsMenu; }
            // same with settings menu
            List<DrawString> mainMenu = Menu.mainMenu;
            List<DrawString> settingsMenu = Menu.settingsMenu;

            if (tickState == "mainMenu") // if the menu state is main menu then
            {
                if (Config.Selected == 0) { playColor = Color.Red; } else { playColor = Color.Black; } 
                if (Config.Selected == 1) { quitColor = Color.Red; } else { quitColor = Color.Black; }
                foreach (DrawString menuItem in Menu.mainMenu.ToArray())// for each of the menu itemes, if they are slected set their color to red, else black
                {
                    if (menuItem.id == "play")
                    {
                        if (Config.Selected == 0) // menu item play is s
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "settings")
                    {
                        if (Config.Selected == 1)
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "quit")
                    {
                        if (Config.Selected == 2)
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            mainMenu[mainMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }

                    DrawString item = mainMenu[mainMenu.IndexOf(menuItem)]; //get the menu item by first getting its index in the array
                    spriteBatch.DrawString(item.Font, item.Text, item.Position, item.TextColor); // draw the menu item after it has been eddited
                }
            }
            if (tickState == "settingsMenu") // as above, but for settings menu
            {
                foreach (DrawString menuItem in settingsMenu.ToArray())
                {
                    if (menuItem.id == "sound")
                    {
                        settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "Sound: " + Config.soundEnabled; // update the sound setting
                        if (Config.Selected == 0)
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "fps")
                    {
                        if (Config.maxFPS == 0) { settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "FPS limit: " + "None"; } // if maxfps is set to 0, say "none" rather than 0
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "FPS limit: " + Config.maxFPS; // else just update it
                        }


                        if (Config.Selected == 1)
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "fov")
                    {
                        if (Config.Selected == 2)
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }
                    if (menuItem.id == "sale")
                    {
                        settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "Sale? " + Config.SALE;
                        if (Config.Selected == 3)
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Red;
                        }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].TextColor = Color.Black;
                        }
                    }

                    DrawString item = settingsMenu[settingsMenu.IndexOf(menuItem)];
                    spriteBatch.DrawString(item.Font, item.Text, item.Position, item.TextColor);

                }
            }
        }



        public static void updateMenu(string tickState, int tickSelected)
        {

            if (tickState == "mainMenu") { currentMenu = mainMenu; }
            if (tickState == "settingsMenu") { currentMenu = settingsMenu; }

            if (tickState == "mainMenu")
            {
                if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && wWasUp)
                {
                    wWasUp = false;
                    Config.Selected--;
                    if (Config.Selected < 0)
                    {
                        Config.Selected = 0;
                    }
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && sWasUp)
                {
                    sWasUp = false;
                    Config.Selected++;
                    if (Config.Selected > (currentMenu.Count() - 1))
                    {
                        Config.Selected = currentMenu.Count() - 1;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (tickSelected == 2) { Config.State = "quit"; }
                    if (tickSelected == 1) { Config.State = "settingsMenu"; }
                    if (tickSelected == 0) { Config.State = "startSPGame"; }
                }
            }
            if (tickState == "settingsMenu")
            {
                if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && wWasUp)
                {
                    wWasUp = false;
                    Config.Selected--;
                    if (Config.Selected < 0)
                    {
                        Config.Selected = 0;
                    }
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && sWasUp)
                {
                    sWasUp = false;
                    Config.Selected++;
                    if (Config.Selected > (currentMenu.Count() - 1))
                    {
                        Config.Selected = currentMenu.Count() - 1;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && enterWasUp)
                {
                    enterWasUp = false;
                    if (tickSelected == 0) { if (Config.soundEnabled) { Config.soundEnabled = false; } else { Config.soundEnabled = true; } }
                    if (tickSelected == 1) { if (Config.maxFPS == 120) { Config.maxFPS = 0; } else { Config.maxFPS += 30; } }

                    if (tickSelected == 3) { if (Config.SALE) { Config.SALE = false; } else { Config.SALE = true; } }

                }
                if (Keyboard.GetState().IsKeyUp(Keys.Enter) && !enterWasUp)
                {
                    enterWasUp = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Config.State = "mainMenu";
                }
            }
            if (tickState == "SPGame")
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Config.State = "mainMenu";
                    Game1.clearBalls();
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    /*foreach (Ball ball in balls){
                        if (ball.sphere.intersects)
                    }
                }

            }

            if ((Keyboard.GetState().IsKeyUp(Keys.S) || Keyboard.GetState().IsKeyUp(Keys.Down)) && !sWasUp && !Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                sWasUp = true;
            }

            if ((Keyboard.GetState().IsKeyUp(Keys.W) || Keyboard.GetState().IsKeyUp(Keys.Up)) && !wWasUp && !Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                wWasUp = true;
            }

        }
    }
}*/


// Stopped commenting as i need to re-write the menu system
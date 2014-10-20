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
    static class Menu
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
            if (tickState == "settingMenu") { currentMenu = Menu.settingsMenu; }
            List<DrawString> mainMenu = Menu.mainMenu;
            List<DrawString> settingsMenu = Menu.settingsMenu;

            if (tickState == "mainMenu")
            {
                if (Config.Selected == 0) { playColor = Color.Red; } else { playColor = Color.Black; }
                if (Config.Selected == 1) { quitColor = Color.Red; } else { quitColor = Color.Black; }
                foreach (DrawString menuItem in Menu.mainMenu.ToArray())
                {
                    if (menuItem.id == "play")
                    {
                        if (Config.Selected == 0)
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

                    DrawString item = mainMenu[mainMenu.IndexOf(menuItem)];
                    spriteBatch.DrawString(item.Font, item.Text, item.Position, item.TextColor);
                }
            }
            if (tickState == "settingsMenu")
            {
                foreach (DrawString menuItem in settingsMenu.ToArray())
                {
                    if (menuItem.id == "sound")
                    {
                        settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "Sound: " + Config.soundEnabled;
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
                        if (Config.maxFPS == 0) { settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "FPS limit: " + "None"; }
                        else
                        {
                            settingsMenu[settingsMenu.IndexOf(menuItem)].Text = "FPS limit: " + Config.maxFPS;
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
                    }*/
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
}

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

        public static List<DrawString> mainMenu = new List<DrawString>();
        public static List<DrawString> settingsMenu = new List<DrawString>();
        private static List<DrawString> currentMenu;

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
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    /*if (tickSelected == 2) { Config.State = "quit"; }
                    if (tickSelected == 1) { Config.State = "settingsMenu"; }
                    if (tickSelected == 0) { Config.State = "startSPGame"; }*/
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

using Engineer.Engine;
using Engineer.Runner;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJam.FrogShift
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExternRunner Runner = new ExternRunner(1024, 768, new GraphicsMode(32, 24, 0, 8), "Frog Shift");
            GameLogic Logic = new GameLogic();
            Game GameObject = new Game();
            
            Menu _Menu = new Menu();
            Scene2D MenuScene = (Scene2D)_Menu.CreateMenuScene();
            MenuScene.Data["Runner"] = Runner;
            GameObject.Scenes.Add(MenuScene);

            Scene2D PlayScene = new Scene2D("Play Scene");
            PlayScene.BackColor = Color.FromArgb(41, 216, 238);
            GameObject.Scenes.Add(PlayScene);

            Logic.Init(Runner, GameObject, PlayScene);
            Runner.Init(GameObject, MenuScene);
            Runner.Run();
        }
    }
}

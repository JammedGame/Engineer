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
            ExternRunner Runner = new ExternRunner(1920, 1080, new GraphicsMode(32, 24, 0, 8), "Frog Shift");
            Runner.WindowState = OpenTK.WindowState.Fullscreen;
            GameLogic Logic = new GameLogic();
            Game GameObject = new Game();
            
            Menu _Menu = new Menu();
            Scene2D MenuScene = (Scene2D)_Menu.CreateMenuScene();
            MenuScene.Data["Runner"] = Runner;
            GameObject.Scenes.Add(MenuScene);

            Scene2D PlayScene = new Scene2D("Play Scene");
            PlayScene.BackColor = Color.FromArgb(0, 210, 127);
            GameObject.Scenes.Add(PlayScene);

            AudioPlayer.Init();
            AudioPlayer.PlaySound(AudioPlayer.Music, true, 30);

            Logic.Init(Runner, GameObject, PlayScene);
            Runner.Init(GameObject, MenuScene);
            Runner.Run();
        }
    }
}

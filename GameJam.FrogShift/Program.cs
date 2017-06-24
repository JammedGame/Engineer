using Engineer.Engine;
using Engineer.Runner;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
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
            ExternRunner Runner = new ExternRunner(1366, 768, new GraphicsMode(32, 24, 0, 8), "Frog Shift");
            GameLogic Logic = new GameLogic();
            Game GameObject = new Game();
            Scene2D PlayScene = new Scene2D("Play Scene");
            PlayScene.BackColor = System.Drawing.Color.AliceBlue;
            GameObject.Scenes.Add(PlayScene);
            Logic.Init(Runner, GameObject, PlayScene);
            Runner.Init(GameObject, PlayScene);
            Runner.Run();
        }
    }
}

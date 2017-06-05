using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engineer.Runner;
using OpenTK.Graphics;


namespace Engineer.PlatformerExample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExternRunner Runner = new ExternRunner(1366, 768, new GraphicsMode(32, 24, 0, 8), "Trihead Engineer Platformer Example");
            GameLogic Logic = new GameLogic();
            Logic.Runner = Runner;
            Logic.CurrentGame = new Engineer.Engine.Game();
            Logic.Init(new System.Drawing.Point(1366, 768));
            Runner.Init(Logic.CurrentGame, Logic.CurrentGame.Scenes[0]);
            Runner.Run();
        }
    }
}

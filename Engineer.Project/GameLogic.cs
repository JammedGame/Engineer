using Engineer.Engine;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Project
{
    public class GameLogic
    {
        private Game _Game;
        private ExternRunner _Runner;
        public GameLogic()
        {
            this._Game = new Game();
            this._Game.Name = "Engineer Project";
            Scene2D Menu = new Menu();
            Menu.Name = "Menu";
            this._Game.AddScene(Menu);
            this._Runner = new ExternRunner((int)LocalSettings.Scale.X, (int)LocalSettings.Scale.Y, "Engineer Project");
            this._Runner.SetWindowState(LocalSettings.State);
        }
        public void Run()
        {
            this._Runner.Init(this._Game, Menu);
            this._Runner.Run();
        }
    }
}

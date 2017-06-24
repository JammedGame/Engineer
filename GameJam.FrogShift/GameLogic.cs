using Engineer.Engine;
using Engineer.Mathematics;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    public class GameLogic
    {
        private Runner _Runner;
        private Game _CGame;
        private Scene _CScene;
        private ResourceManager _ResMan;
        public Runner Runner { get => _Runner; set => _Runner = value; }
        public Game CGame { get => _CGame; set => _CGame = value; }
        public Scene CScene { get => _CScene; set => _CScene = value; }
        public GameLogic()
        {
            _ResMan = new ResourceManager();
            _ResMan.Init();
        }
        public void Init(Runner NewRunner, Game NewGame, Scene CurrentScene)
        {
            this._Runner = NewRunner;
            this._CGame = NewGame;
            this._CScene = CurrentScene;
        }
        private void CreateStaticSprite(string Name, Bitmap Image, Vertex Positon, Vertex Size)
        {
            SpriteSet StaticSet = new SpriteSet("Static", Image);
            Sprite StaticSprite = new Sprite();
            StaticSprite.SpriteSets.Add(StaticSet);
            StaticSprite.Translation = Positon;
            StaticSprite.Scale = Size;
            DrawnSceneObject Static = new DrawnSceneObject(Name, StaticSprite);
        }
    }
}

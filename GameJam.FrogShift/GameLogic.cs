using Engineer.Engine;
using Engineer.Engine.Physics;
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
        private DrawnSceneObject _Player;
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
            CreateFloor();
            CreateCharacter();
            
        }
        private void CreateCharacter()
        {
            SpriteSet IdleSet = new SpriteSet("Idle");
            IdleSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk0);
            SpriteSet IdleBSet = new SpriteSet("Idle");
            IdleBSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk0b);
            SpriteSet WalkSet = new SpriteSet("Walk");
            WalkSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk0);
            WalkSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk1);
            WalkSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk2);
            WalkSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk3);
            SpriteSet WalkBSet = new SpriteSet("Walk");
            WalkBSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk0b);
            WalkBSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk1b);
            WalkBSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk2b);
            WalkBSet.Sprite.Add(global::GameJam.FrogShift.Properties.Resources.walk3b);
            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(IdleSet);
            CharSprite.SpriteSets.Add(IdleBSet);
            CharSprite.SpriteSets.Add(WalkSet);
            CharSprite.SpriteSets.Add(WalkBSet);
            CharSprite.Translation = new Vertex(CharSprite.Translation.X, Runner.Height - CharSprite.Scale.Y - 175, 0);
            DrawnSceneObject Char = new DrawnSceneObject("Char", CharSprite);

            _Player = Char;
            _Player.Data["Direction"] = 0;
            _Player.Data["Collision"] = true;
            
            //Char.Events.Extern.KeyPress += new GameEventHandler(KeyPressEvent);
            _CScene.AddSceneObject(Char);
        }

        private void CreateFloor()
        {
            int[] LilipadsX = new int[] { 0, 200, 450, 600, 800 };

            DrawnSceneObject Back = GameLogic.CreateStaticSprite("Back", global::GameJam.FrogShift.Properties.Resources.BG, new Vertex(0, 0, 0), new Vertex(Runner.Width, Runner.Height, 0));
            _CScene.AddSceneObject(Back);

            for (int i = 0; i < LilipadsX.Length; i++)
            {
                DrawnSceneObject Floor = GameLogic.CreateStaticSprite("Floor"+i, global::GameJam.FrogShift.Properties.Resources._2, new Vertex(LilipadsX[i], 425, 0), new Vertex(100, 50, 0));
                _CScene.AddSceneObject(Floor);
            }

            DrawnSceneObject Water = GameLogic.CreateStaticSprite("Water", global::GameJam.FrogShift.Properties.Resources.voda, new Vertex(0, 450, 0), new Vertex(1024, 1000, 0));
            _CScene.AddSceneObject(Water);
        }
        private void GameUpdateEvent(Game G, EventArguments E)
        {
        }
        public static DrawnSceneObject CreateStaticSprite(string Name, Bitmap Image, Vertex Positon, Vertex Size)
        {
            SpriteSet StaticSet = new SpriteSet("Static", Image);
            Sprite StaticSprite = new Sprite();
            StaticSprite.SpriteSets.Add(StaticSet);
            StaticSprite.Translation = Positon;
            StaticSprite.Scale = Size;
            DrawnSceneObject Static = new DrawnSceneObject(Name, StaticSprite);
            return Static;
        }
    }
}

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
        private GameTimer gtimer;
        private CameraMove Camera;
        private int counter = 0;
        private int counter1 = 0;
        public static float _GlobalScale;
        private DrawnSceneObject _Player;
        private Runner _Runner;
        private Game _CGame;
        private Scene _CScene;
        private ResourceManager _ResMan;
        private Movement _Movement;
        private List<SceneObject> _Colliders = new List<SceneObject>();
        public Runner Runner
        {
            get => _Runner; set => _Runner = value;
        }
        public Game CGame
        {
            get => _CGame; set => _CGame = value;
        }
        public Scene CScene
        {
            get => _CScene; set => _CScene = value;
        }
        public GameLogic()
        {
            _ResMan = new ResourceManager();
            _ResMan.Init();
        }
        public void Init(Runner NewRunner, Game NewGame, Scene CurrentScene)
        {
            GameLogic._GlobalScale = NewRunner.Height / 1080.0f;
            this._Runner = NewRunner;
            this._CGame = NewGame;
            this._CScene = CurrentScene;
            CreateFloor();
            CreateCharacter();
            this._Movement = new Movement(_Runner, _Player, _Colliders);
            CScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdateEvent);
            CScene.Events.Extern.KeyDown += new GameEventHandler(_Movement.KeyDownEvent);
            CScene.Events.Extern.KeyUp += new GameEventHandler(_Movement.KeyUpEvent);
            CScene.Events.Extern.KeyPress += new GameEventHandler(_Movement.KeyPressEvent);
            this.gtimer = new GameTimer(_CScene, Runner);
            Camera = new CameraMove(_CScene);
        }
        private void CreateCharacter()
        {
            DrawnSceneObject Char = Character.Create((Scene2D)CScene);
            _Player = Char;
            _Player.Data["Direction"] = 0;
            _Player.Data["Collision"] = true;
            _Player.Data["skokBrojac"] = 0;
            _Player.Data["padBrojac"] = 0;
            _Player.Data["colliding"] = true;
            _Player.Data["flying"] = false;
        }
        private void CreateFloor()
        {
            _CScene.Data["Colliders"] = this._Colliders;
            Level.Create((Scene2D)_CScene, (ExternRunner)Runner);
        }
        public void GameUpdateEvent(Game G, EventArguments E)
        {
            _Movement.CheckCollision();
            Character.UpdateLegs(_Player);
            if (counter++ >= 333) gtimer.DecTime();
            if (counter1++ >= 1) { Camera.MoveCamera(this._CScene); counter1 = 0; }
        }
        public static DrawnSceneObject CreateStaticSprite(string Name, Bitmap Image, Vertex Positon, Vertex Size, bool ApplyGlobalScale = true)
        {
            if (ApplyGlobalScale)
            {
                Positon = new Vertex(Positon.X * GameLogic._GlobalScale, Positon.Y * GameLogic._GlobalScale, 0);
                Size = new Vertex(Size.X * GameLogic._GlobalScale, Size.Y * GameLogic._GlobalScale, 0);
            }
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
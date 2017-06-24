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
        private int counter = 0;
        private static float _GlobalScale;
        private DrawnSceneObject _Player;
        private Runner _Runner;
        private Game _CGame;
        private Scene _CScene;
        private ResourceManager _ResMan;
        private List<SceneObject> _Colliders = new List<SceneObject>();
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
            GameLogic._GlobalScale = NewRunner.Height / 1080.0f;
            this._Runner = NewRunner;            
            this._CGame = NewGame;
            this._CScene = CurrentScene;
            CreateFloor();
            CreateCharacter();
            CScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdateEvent);
            CScene.Events.Extern.KeyDown += new GameEventHandler(KeyDownEvent);
            this.gtimer = new GameTimer(_CScene,Runner);
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
            CharSprite.Translation = new Vertex(200 * GameLogic._GlobalScale, 600 * GameLogic._GlobalScale, 0);
            CharSprite.Scale = new Vertex(200 * GameLogic._GlobalScale, 200 * GameLogic._GlobalScale, 0);
            DrawnSceneObject Char = new DrawnSceneObject("Char", CharSprite);

            _Player = Char;
            _Player.Data["Direction"] = 0;
            _Player.Data["Collision"] = true;
            _Player.Data["skokBrojac"] = 0;
            _Player.Data["padBrojac"] = 0;
            _Player.Data["colliding"] = true;
            _Player.Data["flying"] = false;
            //Char.Events.Extern.KeyPress += new GameEventHandler(KeyPressEvent);
            _CScene.AddSceneObject(Char);
        }

        private void CreateFloor()
        {
            int[] LilipadsX = new int[] { 200, 600, 1000, 1400, 1800 };

            DrawnSceneObject Back = GameLogic.CreateStaticSprite("Back", global::GameJam.FrogShift.Properties.Resources.BG, new Vertex(0, 0, 0), new Vertex(Runner.Width, Runner.Height, 0), false);
            _CScene.AddSceneObject(Back);

            for (int i = 0; i < LilipadsX.Length; i++)
            {
                DrawnSceneObject Floor = GameLogic.CreateStaticSprite("Floor"+i, global::GameJam.FrogShift.Properties.Resources._2, new Vertex(LilipadsX[i], 800, 0), new Vertex(200, 100, 0));
                _CScene.AddSceneObject(Floor);
                _Colliders.Add(Floor);
            }

            DrawnSceneObject Water = GameLogic.CreateStaticSprite("Water", global::GameJam.FrogShift.Properties.Resources.voda, new Vertex(0, 850, 0), new Vertex(1920, 1080, 0));
            _CScene.AddSceneObject(Water);
        }

        private void KeyDownEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.A)
            {
                if (Convert.ToBoolean(_Player.Data["flying"])  )
                {
                    _Player.Representation.Translation = new Vertex(_Player.Representation.Translation.X - 3, _Player.Representation.Translation.Y, 0);
                }
            }
            if (E.KeyDown == KeyType.D)
            {
                if (Convert.ToBoolean(_Player.Data["flying"]) )
                {
                    _Player.Representation.Translation = new Vertex(_Player.Representation.Translation.X + 3, _Player.Representation.Translation.Y, 0);
                }
            }
            if (E.KeyDown == KeyType.Space)
            {
                if (Convert.ToBoolean(_Player.Data["flying"])==false)
                {
                    _Player.Data["skokBrojac"] = 25;
                    _Player.Data["flying"] = true;
                }
            }
            if (E.KeyDown == KeyType.Escape)
            {
                Runner.Close();
            }
        }

        private void GameUpdateEvent(Game G, EventArguments E)
        {
            CheckCollision();
            
            if (counter++ == 333) gtimer.DecTime();
            if(counter1++==500)

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

        private void CheckCollision()
        {
            bool collided = Convert.ToBoolean(_Player.Data["colliding"]);
            bool flying = Convert.ToBoolean(_Player.Data["flying"]);
            int tmpSkokBrojac = Convert.ToInt32(_Player.Data["skokBrojac"]);
            if (tmpSkokBrojac > 0 && collided == true)
            {
                tmpSkokBrojac -= 1;
                _Player.Data["skokBrojac"] = tmpSkokBrojac;

                Vertex lastPos = _Player.Representation.Translation;

                lastPos.Y -= tmpSkokBrojac;

                _Player.Representation.Translation = lastPos;
            }
            else
            {
                Vertex lastPos = _Player.Representation.Translation;
                Vertex playerScale = _Player.Representation.Scale;
                //lastPos.Y += (float)12;
                collided = false;

                _Player.Representation.Translation = lastPos;

                for (int i = 0; i < _Colliders.Count; i++)
                {
                    DrawnSceneObject colliderDSO = (DrawnSceneObject)_Colliders[i];
                    Vertex colliderPos = colliderDSO.Representation.Translation;
                    Vertex coliderScale = colliderDSO.Representation.Scale;

                    Rectangle playerRect = new Rectangle(Convert.ToInt32(lastPos.X + playerScale.X / 4), Convert.ToInt32(lastPos.Y), (int)(playerScale.X / 2), (int)playerScale.Y);
                    Rectangle colliderRect = new Rectangle(Convert.ToInt32(colliderPos.X), Convert.ToInt32(colliderPos.Y), Convert.ToInt32(coliderScale.X), Convert.ToInt32(coliderScale.Y));

                    if (playerRect.IntersectsWith(colliderRect))
                    {
                        collided = true;
                        _Player.Representation.Translation = new Vertex(_Player.Representation.Translation.X, colliderPos.Y - _Player.Representation.Scale.Y + 1, _Player.Representation.Translation.Z);
                        flying = false;
                        break;
                    }
                }

                if (!collided)
                {

                    lastPos.Y += (int)_Player.Data["padBrojac"] + 1;
                    _Player.Data["padBrojac"] = (int)_Player.Data["padBrojac"] + 1;

                    _Player.Data["flying"] = true;
                    _Player.Representation.Translation = lastPos;

                }
                else
                {
                    _Player.Data["flying"] = false;
                    _Player.Data["padBrojac"] = 0;
                }
            }
            //_Physics.RunSimulation();
            _Player.Data["colliding"] = collided;
        }

        private void MoveCamera()
        {
            _CScene
        }

    }
}

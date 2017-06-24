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
            this._Runner = NewRunner;            
            this._CGame = NewGame;
            this._CScene = CurrentScene;
            CreateFloor();
            CreateCharacter();
            CScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdateEvent);
            CScene.Events.Extern.KeyDown += new GameEventHandler(KeyDownEvent);
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
            _Player.Data["skokBrojac"] = 0;
            _Player.Data["colliding"] = true;
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
                _Colliders.Add(Floor);
            }

            DrawnSceneObject Water = GameLogic.CreateStaticSprite("Water", global::GameJam.FrogShift.Properties.Resources.voda, new Vertex(0, 450, 0), new Vertex(1024, 1000, 0));
            _CScene.AddSceneObject(Water);
        }

        private void KeyDownEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.Space && Convert.ToInt32(_Player.Data["skokBrojac"]) < 1)
            {

                _Player.Data["skokBrojac"] = 25;
            }

        }

        private void GameUpdateEvent(Game G, EventArguments E)
        {

            CheckCollision();
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

        private void CheckCollision()
        {
            bool collided = Convert.ToBoolean(_Player.Data["colliding"]);
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
                    }
                }

                if (!collided)
                {
                    lastPos.Y += 12.0f;
                    _Player.Representation.Translation = lastPos;
                }
            }
            //_Physics.RunSimulation();
            _Player.Data["colliding"] = collided;
        }


    }
}

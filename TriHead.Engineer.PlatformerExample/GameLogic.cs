using Engineer.Engine;
using Engineer.Engine.IO;
using Engineer.Mathematics;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.PlatformerExample
{
    public class GameLogic
    {
        private int _CharDirection;
        private Point _WindowSize;
        private DrawnSceneObject _Player;
        private Game _CurrentGame;
        private Scene2D _CurrentScene;
        private ExternRunner _Runner;
        private ResourceManager _ResMan;
        private List<SceneObject> _Colliders;
        public Game CurrentGame
        {
            get
            {
                return _CurrentGame;
            }
            set
            {
                _CurrentGame = value;
            }
        }
        public ExternRunner Runner
        {
            get
            {
                return _Runner;
            }

            set
            {
                _Runner = value;
            }
        }
        public Point WindowSize
        {
            get
            {
                return _WindowSize;
            }

            set
            {
                _WindowSize = value;
            }
        }
        public GameLogic()
        {
            _ResMan = new ResourceManager();
            _ResMan.Init();
        }
        public void Init(Point WindowSize)
        {
            _WindowSize = WindowSize;
            _CurrentGame.Scenes.Add(new Scene2D("Default"));
            _CurrentScene = (Scene2D)_CurrentGame.Scenes[0];
            //EFXInterface Interface = new EFXInterface();
            //_CurrentGame = (Game)Interface.Load("Data/game.efx");
            //_CurrentScene = (Scene2D)_CurrentGame.Scenes[0];
            _CurrentScene.Events.Extern.KeyPress += new GameEventHandler(KeyPressEvent);
            _CurrentScene.Events.Extern.KeyDown += new GameEventHandler(KeyDownEvent);
            _CurrentScene.Events.Extern.KeyUp += new GameEventHandler(KeyUpEvent);
            _CurrentScene.Events.Extern.MouseClick += new GameEventHandler(MouseClickEvent);
            _CurrentScene.Events.Extern.MouseMove += new GameEventHandler(MouseMoveEvent);
            _CurrentScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdateEvent);
            _CurrentScene.BackColor = Color.FromArgb(41, 216, 238);
            CreateFloor();
            CreateCharacter();

            //Interface.Save(_CurrentGame, "Data/game.efx");
        }
        private void CreateFloor()
        {
            SpriteSet FloorSet = new SpriteSet("Idle");
            FloorSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.floor);
            Sprite FloorSprite = new Sprite();
            FloorSprite.SpriteSets.Add(FloorSet);
            FloorSprite.Translation = new Vertex(FloorSprite.Translation.X, _WindowSize.Y - FloorSprite.Scale.Y, FloorSprite.Translation.Z);
            DrawnSceneObject Floor = new DrawnSceneObject("Floor", FloorSprite);
            for (int i = 0; i < 14; i++)
            {
                DrawnSceneObject FloorN = new DrawnSceneObject(Floor, _CurrentScene);
                FloorN.Representation.Translation = new Vertex(FloorN.Representation.Translation.X + i * 98, FloorN.Representation.Translation.Y, FloorN.Representation.Translation.Z);
                _CurrentScene.AddSceneObject(FloorN);
            }
            DrawnSceneObject FloorU = new DrawnSceneObject(Floor, _CurrentScene);
            FloorU.Representation.Translation = new Vertex(FloorU.Representation.Translation.X + 8 * 98, FloorU.Representation.Translation.Y - 200, FloorU.Representation.Translation.Z);
            _CurrentScene.AddSceneObject(FloorU);
        }
        private void CreateCharacter()
        {
            SpriteSet IdleSet = new SpriteSet("Idle");
            IdleSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk0);
            SpriteSet IdleBSet = new SpriteSet("Idle");
            IdleBSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk0b);
            SpriteSet WalkSet = new SpriteSet("Walk");
            WalkSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk0);
            WalkSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk1);
            WalkSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk2);
            WalkSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk3);
            SpriteSet WalkBSet = new SpriteSet("Walk");
            WalkBSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk0b);
            WalkBSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk1b);
            WalkBSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk2b);
            WalkBSet.Sprite.Add(global::Engineer.PlatformerExample.Properties.Resources.walk3b);
            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(IdleSet);
            CharSprite.SpriteSets.Add(IdleBSet);
            CharSprite.SpriteSets.Add(WalkSet);
            CharSprite.SpriteSets.Add(WalkBSet);
            CharSprite.Translation = new Vertex(CharSprite.Translation.X, _WindowSize.Y - CharSprite.Scale.Y - 95, CharSprite.Translation.Z);
            DrawnSceneObject Char = new DrawnSceneObject("Char",CharSprite);
            Char.ParentScene = _CurrentScene;
            _Player = Char;
            Char.Events.Extern.KeyPress += new GameEventHandler(KeyPressEvent);
            _CurrentScene.AddSceneObject(Char);
        }
        private void KeyPressEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.D)
            {
                _CharDirection = 0;
                Sprite PlayerSprite = _Player.Representation as Sprite;
                PlayerSprite.UpdateSpriteSet(2);
                PlayerSprite.Translation = new Vertex(PlayerSprite.Translation.X + 5, PlayerSprite.Translation.Y, PlayerSprite.Translation.Z);
            }
            if (E.KeyDown == KeyType.A)
            {
                _CharDirection = 1;
                Sprite PlayerSprite = _Player.Representation as Sprite;
                PlayerSprite.UpdateSpriteSet(3);
                PlayerSprite.Translation = new Vertex(PlayerSprite.Translation.X - 5, PlayerSprite.Translation.Y, PlayerSprite.Translation.Z);
            }
        }
        private void KeyDownEvent(Game G, EventArguments E)
        {
            
        }
        private void KeyUpEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.D || E.KeyDown == KeyType.A)
            {
                Sprite PlayerSprite = _Player.Representation as Sprite;
                PlayerSprite.UpdateSpriteSet(0 + _CharDirection);
            }
        }
        private void MouseClickEvent(Game G, EventArguments E)
        {

        }
        private void GameUpdateEvent(Game G, EventArguments E)
        {

        }
        private void MouseMoveEvent(Game G, EventArguments E)
        {

        }
    }
}

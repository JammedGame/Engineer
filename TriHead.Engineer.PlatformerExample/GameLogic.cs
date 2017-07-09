using Engineer.Engine;
using Engineer.Engine.IO;
using Engineer.Engine.Physics;
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
        private Point _WindowSize;
        private DrawnSceneObject _Player;
        private Game _CurrentGame;
        private Scene2D _CurrentScene;
        private ExternRunner _Runner;
        private ResourceManager _ResMan;
        private BulletPhysics _Physics;
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
            _Physics = new BulletPhysics();
            
            EFXInterface Interface = new EFXInterface();
            //_CurrentGame = (Game)Interface.Load("Data/game.efx");
            _CurrentGame.Scenes.Add(new Scene2D("Default"));
            _CurrentScene = (Scene2D)_CurrentGame.Scenes[0];
            _CurrentScene.Events.Extern.KeyPress += new GameEventHandler(KeyPressEvent);
            _CurrentScene.Events.Extern.KeyDown += new GameEventHandler(KeyDownEvent);
            _CurrentScene.Events.Extern.KeyUp += new GameEventHandler(KeyUpEvent);
            _CurrentScene.Events.Extern.MouseClick += new GameEventHandler(MouseClickEvent);
            _CurrentScene.Events.Extern.MouseMove += new GameEventHandler(MouseMoveEvent);
            _CurrentScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdateEvent);
            _CurrentScene.Events.Extern.Resize += new GameEventHandler(WindowResize);
            _CurrentScene.BackColor = Color.FromArgb(41, 216, 238);
            //_Player = (DrawnSceneObject)_CurrentScene.Objects[_CurrentScene.Objects.Count - 1];
            CreateFloor();
            CreateCharacter();
            _Physics.UpdateScene(_CurrentScene);
            //Interface.Save(_CurrentGame, "Data/game.efx");
        }
        private void CreateFloor()
        {
            SpriteSet BackSet = new SpriteSet("Tile", global::Engineer.PlatformerExample.Properties.Resources.BG);
            Sprite BackSprite = new Sprite();
            BackSprite.SpriteSets.Add(BackSet);
            BackSprite.Translation = new Vertex();
            BackSprite.Scale = new Vertex(1920, 1080, 0);
            DrawnSceneObject Back = new DrawnSceneObject("Back", BackSprite);
            _CurrentScene.AddSceneObject(Back);

            TileCollection FloorCollection = new TileCollection();
            FloorCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources._1);
            FloorCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources._2);
            FloorCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources._3);
            FloorCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources._13);
            FloorCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources._14);
            FloorCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources._15);
            FloorCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources._17);
            TileCollection DodoadCollection = new TileCollection();
            DodoadCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources.Sign_2);
            DodoadCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources.Bush__1_);
            DodoadCollection.TileImages.Add(global::Engineer.PlatformerExample.Properties.Resources.Tree_2);

            Tile FloorTile = new Tile();
            FloorTile.Translation = new Vertex(0, _WindowSize.Y - FloorTile.Scale.Y, 0);
            FloorTile.Collection = FloorCollection;
            DrawnSceneObject FloorC = new DrawnSceneObject("Floor", FloorTile);
            FloorC.Data["Collision"] = true;
            FloorC.Data["Weight"] = 0;

            Tile DodoadTile = new Tile();
            DodoadTile.Translation = new Vertex(0, _WindowSize.Y - FloorTile.Scale.Y, 0);
            DodoadTile.Collection = DodoadCollection;
            DrawnSceneObject Dodoad = new DrawnSceneObject("Dodoad", DodoadTile);

            DrawnSceneObject Floor = null;

            Floor = new DrawnSceneObject(Dodoad, _CurrentScene);
            Floor.Visual.Translation = new Vertex(100, 580, 0);
            Floor.Visual.Scale = new Vertex(100, 100, 0);
            ((Tile)Floor.Visual).SetIndex(0);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(Dodoad, _CurrentScene);
            Floor.Visual.Translation = new Vertex(600, 380, 0);
            Floor.Visual.Scale = new Vertex(200, 100, 0);
            ((Tile)Floor.Visual).SetIndex(1);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(Dodoad, _CurrentScene);
            Floor.Visual.Translation = new Vertex(1000, 380, 0);
            Floor.Visual.Scale = new Vertex(300, 300, 0);
            ((Tile)Floor.Visual).SetIndex(2);
            _CurrentScene.AddSceneObject(Floor);

            for (int i = 0; i < 10; i++)
            {
                Floor = new DrawnSceneObject(FloorC, _CurrentScene);
                Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + (2 + i) * 98, Floor.Visual.Translation.Y, Floor.Visual.Translation.Z);
                ((Tile)Floor.Visual).SetIndex(6);
                Floor.Data["Collision"] = false;
                _CurrentScene.AddSceneObject(Floor);
            }

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            ((Tile)Floor.Visual).SetIndex(1);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 1 * 98, Floor.Visual.Translation.Y, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(1);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 2 * 98, Floor.Visual.Translation.Y, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(2);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 4 * 98, Floor.Visual.Translation.Y - 200, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(3);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 5 * 98, Floor.Visual.Translation.Y - 200, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(4);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 6 * 98, Floor.Visual.Translation.Y - 200, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(4);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 7 * 98, Floor.Visual.Translation.Y - 200, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(4);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 8 * 98, Floor.Visual.Translation.Y - 200, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(5);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 11 * 98, Floor.Visual.Translation.Y, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(0);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 12 * 98, Floor.Visual.Translation.Y, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(1);
            _CurrentScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CurrentScene);
            Floor.Visual.Translation = new Vertex(Floor.Visual.Translation.X + 13 * 98, Floor.Visual.Translation.Y, Floor.Visual.Translation.Z);
            ((Tile)Floor.Visual).SetIndex(1);
            _CurrentScene.AddSceneObject(Floor);

            
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
            _Player.Data["Direction"] = 0;
            _Player.Data["Collision"] = true;
            _Player.Data["Weight"] = 100;
            Char.Events.Extern.KeyPress += new GameEventHandler(KeyPressEvent);
            _CurrentScene.AddSceneObject(Char);
        }
        private void KeyPressEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.D)
            {
                _Player.Data["Direction"] = 0;
                Sprite PlayerSprite = _Player.Visual as Sprite;
                PlayerSprite.UpdateSpriteSet(2);
                Vertex Velocities = _Physics.GetVelocities((int)_Player.Data["PhysicsIndex"]);
                _Physics.SetVelocities((int)_Player.Data["PhysicsIndex"], new Vertex(3, Velocities.Y, 0));
            }
            if (E.KeyDown == KeyType.A)
            {
                _Player.Data["Direction"] = 1;
                Sprite PlayerSprite = _Player.Visual as Sprite;
                PlayerSprite.UpdateSpriteSet(3);
                Vertex Velocities = _Physics.GetVelocities((int)_Player.Data["PhysicsIndex"]);
                _Physics.SetVelocities((int)_Player.Data["PhysicsIndex"], new Vertex(-3, Velocities.Y, 0));
            }
            if (E.KeyDown == KeyType.Space)
            {
                Sprite PlayerSprite = _Player.Visual as Sprite;
                PlayerSprite.UpdateSpriteSet(0 + (int)_Player.Data["Direction"]);
                Vertex Velocities = _Physics.GetVelocities((int)_Player.Data["PhysicsIndex"]);
                _Physics.SetVelocities((int)_Player.Data["PhysicsIndex"], new Vertex(Velocities.X, 8f, 0));
            }
        }
        private void KeyDownEvent(Game G, EventArguments E)
        {
            
        }
        private void KeyUpEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.D || E.KeyDown == KeyType.A)
            {
                Sprite PlayerSprite = _Player.Visual as Sprite;
                PlayerSprite.UpdateSpriteSet(0 + (int)_Player.Data["Direction"]);
                Vertex Velocities = _Physics.GetVelocities((int)_Player.Data["PhysicsIndex"]);
                _Physics.SetVelocities((int)_Player.Data["PhysicsIndex"], new Vertex(0, Velocities.Y, 0));
            }
        }
        private void MouseClickEvent(Game G, EventArguments E)
        {
        }
        private void GameUpdateEvent(Game G, EventArguments E)
        {
            _Physics.RunSimulation();
        }
        private void MouseMoveEvent(Game G, EventArguments E)
        {

        }
        private void WindowResize(Game G, EventArguments E)
        {
            float ScaleRate = this._Runner.Height / 768.0f;
            this._CurrentScene.Transformation.Scale = new Vertex(ScaleRate, ScaleRate, 1);
        }
    }
}

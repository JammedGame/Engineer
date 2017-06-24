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
        private BulletPhysics _Physics;
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
        private DrawnSceneObject CreateStaticSprite(string Name, Bitmap Image, Vertex Positon, Vertex Size)
        {
            SpriteSet StaticSet = new SpriteSet("Static", Image);
            Sprite StaticSprite = new Sprite();
            StaticSprite.SpriteSets.Add(StaticSet);
            StaticSprite.Translation = Positon;
            StaticSprite.Scale = Size;
            DrawnSceneObject Static = new DrawnSceneObject(Name, StaticSprite);
            return Static;
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
            CharSprite.Translation = new Vertex(CharSprite.Translation.X, Runner.Height - CharSprite.Scale.Y - 95, CharSprite.Translation.Z);
            DrawnSceneObject Char = new DrawnSceneObject("Char", CharSprite);
            Char.ParentScene = _CScene;

            _Player = Char;
            _Player.Data["Direction"] = 0;
            _Player.Data["Collision"] = true;
            
            Char.Events.Extern.KeyPress += new GameEventHandler(KeyPressEvent);
            _CScene.AddSceneObject(Char);
        }

        private void CreateFloor()
        {
            SpriteSet BackSet = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources.BG);
            Sprite BackSprite = new Sprite();
            BackSprite.SpriteSets.Add(BackSet);
            BackSprite.Translation = new Vertex();
            BackSprite.Scale = new Vertex(1920, 1080, 0);
            DrawnSceneObject Back = new DrawnSceneObject("Back", BackSprite);
            _CScene.AddSceneObject(Back);

            SpriteSet FloorSet1 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources._1);
            SpriteSet FloorSet2 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources._2);
            SpriteSet FloorSet3 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources._3);
            SpriteSet FloorSet4 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources._13);
            SpriteSet FloorSet5 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources._14);
            SpriteSet FloorSet6 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources._15);
            SpriteSet FloorSet7 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources._17);
            SpriteSet FloorSet8 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources.Sign_2);
            SpriteSet FloorSet9 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources.Bush__1_);
            SpriteSet FloorSet10 = new SpriteSet("Tile", global::GameJam.FrogShift.Properties.Resources.Tree_2);
            Sprite FloorSprite = new Sprite();
            FloorSprite.SpriteSets.Add(FloorSet1);
            FloorSprite.SpriteSets.Add(FloorSet2);
            FloorSprite.SpriteSets.Add(FloorSet3);
            FloorSprite.SpriteSets.Add(FloorSet4);
            FloorSprite.SpriteSets.Add(FloorSet5);
            FloorSprite.SpriteSets.Add(FloorSet6);
            FloorSprite.SpriteSets.Add(FloorSet7);
            FloorSprite.SpriteSets.Add(FloorSet8);
            FloorSprite.SpriteSets.Add(FloorSet9);
            FloorSprite.SpriteSets.Add(FloorSet10);
            FloorSprite.Translation = new Vertex(FloorSprite.Translation.X, Runner.Height - FloorSprite.Scale.Y, FloorSprite.Translation.Z);
            DrawnSceneObject FloorC = new DrawnSceneObject("Floor", FloorSprite);
            FloorC.Data["Collision"] = true;
            
            DrawnSceneObject FloorNC = new DrawnSceneObject("Floor", FloorSprite);

            DrawnSceneObject Floor = null;

            Floor = new DrawnSceneObject(FloorNC, _CScene);
            Floor.Representation.Translation = new Vertex(100, 580, 0);
            Floor.Representation.Scale = new Vertex(100, 100, 0);
            ((Sprite)Floor.Representation).UpdateSpriteSet(7);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorNC, _CScene);
            Floor.Representation.Translation = new Vertex(600, 380, 0);
            Floor.Representation.Scale = new Vertex(200, 100, 0);
            ((Sprite)Floor.Representation).UpdateSpriteSet(8);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorNC, _CScene);
            Floor.Representation.Translation = new Vertex(1000, 380, 0);
            Floor.Representation.Scale = new Vertex(300, 300, 0);
            ((Sprite)Floor.Representation).UpdateSpriteSet(9);
            _CScene.AddSceneObject(Floor);

            for (int i = 0; i < 10; i++)
            {
                Floor = new DrawnSceneObject(FloorNC, _CScene);
                Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + (2 + i) * 98, Floor.Representation.Translation.Y, Floor.Representation.Translation.Z);
                ((Sprite)Floor.Representation).UpdateSpriteSet(6);
                _CScene.AddSceneObject(Floor);
            }

            Floor = new DrawnSceneObject(FloorC, _CScene);
            ((Sprite)Floor.Representation).UpdateSpriteSet(1);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 1 * 98, Floor.Representation.Translation.Y, Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(1);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 2 * 98, Floor.Representation.Translation.Y, Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(2);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 4 * 98, Floor.Representation.Translation.Y , Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(3);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 5 * 98, Floor.Representation.Translation.Y , Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(4);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 6 * 98, Floor.Representation.Translation.Y , Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(4);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 7 * 98, Floor.Representation.Translation.Y - 200, Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(4);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 8 * 98, Floor.Representation.Translation.Y , Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(5);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 11 * 98, Floor.Representation.Translation.Y, Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(0);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 12 * 98, Floor.Representation.Translation.Y, Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(1);
            _CScene.AddSceneObject(Floor);

            Floor = new DrawnSceneObject(FloorC, _CScene);
            Floor.Representation.Translation = new Vertex(Floor.Representation.Translation.X + 13 * 98, Floor.Representation.Translation.Y, Floor.Representation.Translation.Z);
            ((Sprite)Floor.Representation).UpdateSpriteSet(1);
            _CScene.AddSceneObject(Floor);


        }

        private void KeyPressEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.D)
            {
                _Player.Data["Direction"] = 0;
                Sprite PlayerSprite = _Player.Representation as Sprite;
                PlayerSprite.UpdateSpriteSet(2);
                Vertex Velocities = _Physics.GetVelocities((int)_Player.Data["PhysicsIndex"]);
                _Physics.SetVelocities((int)_Player.Data["PhysicsIndex"], new Vertex(3, Velocities.Y, 0));
            }
            if (E.KeyDown == KeyType.A)
            {
                _Player.Data["Direction"] = 1;
                Sprite PlayerSprite = _Player.Representation as Sprite;
                PlayerSprite.UpdateSpriteSet(3);
                Vertex Velocities = _Physics.GetVelocities((int)_Player.Data["PhysicsIndex"]);
                _Physics.SetVelocities((int)_Player.Data["PhysicsIndex"], new Vertex(-3, Velocities.Y, 0));
            }
            if (E.KeyDown == KeyType.Space)
            {
                Sprite PlayerSprite = _Player.Representation as Sprite;
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
                Sprite PlayerSprite = _Player.Representation as Sprite;
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
    }
}

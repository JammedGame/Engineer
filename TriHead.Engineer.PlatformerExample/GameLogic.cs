using Engineer.Engine;
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
            _CurrentScene.Events.Extern.KeyPress += new GameEventHandler(KeyPressEvent);
            _CurrentScene.Events.Extern.MouseClick += new GameEventHandler(MouseClickEvent);
            _CurrentScene.Events.Extern.MouseMove += new GameEventHandler(MouseMoveEvent);
            _CurrentScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdateEvent);
            _CurrentScene.BackColor = Color.FromArgb(41, 216, 238);
            CreateFloor();
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
        private void KeyPressEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.Escape) _Runner.Exit();
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

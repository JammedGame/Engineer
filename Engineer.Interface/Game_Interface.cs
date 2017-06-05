using Engineer.Data;
using Engineer.Engine;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Engineer.Interface
{
    public enum InterfaceUpdateMessage
    {
        GameUpdated,
        SceneUpdated,
        SceneObjectsUpdated,
        SelectionUpdated,
        AssetsUpdated,
        LibraryUpdated
    }
    public delegate void InterfaceUpdate(InterfaceUpdateMessage Message);
    public class Game_Interface
    {
        private object _CurrentSelection;
        private SceneObject _CurrentSceneObject;
        private Scene _CurrentScene;
        private Game _CurrentGame;
        private List<OBJContainer> _Scene3DContainers;
        private List<SceneObject> _GlobalSceneObjects;
        public event InterfaceUpdate Update;
        public object CurrentSelection
        {
            get
            {
                return _CurrentSelection;
            }

            set
            {
                _CurrentSelection = value;
                Update.Invoke(InterfaceUpdateMessage.SelectionUpdated);
            }
        }
        public SceneObject CurrentSceneObject
        {
            get
            {
                return _CurrentSceneObject;
            }

            set
            {
                _CurrentSceneObject = value;
                Update.Invoke(InterfaceUpdateMessage.SceneObjectsUpdated);
            }
        }
        public Scene CurrentScene
        {
            get
            {
                return _CurrentScene;
            }

            set
            {
                _CurrentScene = value;
                Update.Invoke(InterfaceUpdateMessage.SceneUpdated);
            }
        }
        public Game CurrentGame
        {
            get
            {
                return _CurrentGame;
            }

            set
            {
                _CurrentGame = value;
                Update.Invoke(InterfaceUpdateMessage.GameUpdated);
            }
        }
        public List<SceneObject> GlobalSceneObjects
        {
            get
            {
                return _GlobalSceneObjects;
            }

            set
            {
                _GlobalSceneObjects = value;
            }
        }
        public Game_Interface(Game CurrentGame)
        {
            Update = new Interface.InterfaceUpdate(OnInterfaceUpdate);
            if (CurrentGame != null) this.CurrentGame = CurrentGame;
            else
            {
                this.CurrentGame = new Game();
                this.CurrentGame.Name = "New Game";
            }
            AcquireData();
        }
        public void ForceUpdate(InterfaceUpdateMessage Message)
        {
            Update.Invoke(Message);
        }
        public void SetGameName(string NewName)
        {
            _CurrentGame.Name = NewName;
            Update.Invoke(InterfaceUpdateMessage.GameUpdated);
        }
        public void NewGame()
        {
            _CurrentSelection = null;
            _CurrentScene = null;
            _CurrentSceneObject = null;
            _CurrentGame = new Game();
            _CurrentGame.Name = "New Game";
            ForceUpdate(InterfaceUpdateMessage.GameUpdated);
        }
        public bool AddSceneItem(int Index, ref string ErrorString)
        {
            bool ReturnValue = false;
            try
            {
                if (_GlobalSceneObjects[Index].Type == SceneObjectType.DrawnSceneObject)
                {
                    this.CurrentScene.AddSceneObject(new DrawnSceneObject((DrawnSceneObject)_GlobalSceneObjects[Index], _CurrentScene));
                    ReturnValue = true;
                }
                else if (_GlobalSceneObjects[Index].Type == SceneObjectType.ScriptSceneObject)
                {
                    this.CurrentScene.AddSceneObject(new ScriptSceneObject((ScriptSceneObject)_GlobalSceneObjects[Index], _CurrentScene));
                    ReturnValue = true;
                }
                else if (_GlobalSceneObjects[Index].Type == SceneObjectType.SoundSceneObject)
                {
                    ReturnValue = false;
                }
                else ReturnValue = false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ErrorString = ex.InnerException.ToString();
                }
                else ErrorString = ex.Message;
                ReturnValue = false;
            }
            if (ReturnValue) Update.Invoke(InterfaceUpdateMessage.SceneUpdated);
            if (ReturnValue) Update.Invoke(InterfaceUpdateMessage.SceneObjectsUpdated);
            return ReturnValue;
        }
        public bool AddEmptyScene(SceneType Type, ref string ErrorString)
        {
            bool ReturnValue = false;
            try
            {
                if (Type == SceneType.Scene2D)
                {
                    Actor Floor_Actor = new Actor(_Scene3DContainers[0], "Floor");
                    Floor_Actor.Scale = new Vertex(0.001f, 0.001f, 0.001f);
                    Camera MainCamera_Camera = new Camera();
                    MainCamera_Camera.Translation = new Vertex(0, 0.6f, 0.8f);
                    MainCamera_Camera.Rotation = new Vertex(30, 0, 0);
                    Light Light1_Light = new Light();
                    Light1_Light.Translation = new Vertex(4, -4, -4);
                    Light1_Light.Color = Color.White;
                    Light1_Light.Attenuation = new Vertex(0.6f, 0.2f, 0.2f);
                    Light1_Light.Intensity = 0.3f;
                    Scene2D NewScene = new Scene2D("Scene_" + (CurrentGame.Scenes.Count + 1).ToString("000"));
                    NewScene.BackColor = Color.FromArgb(40, 40, 40);
                    NewScene.Type = SceneType.Scene2D;
                    this.CurrentScene = NewScene;
                    this.CurrentGame.Scenes.Add(NewScene);
                    ReturnValue = true;
                }
                else if (Type == SceneType.Scene3D)
                {
                    Actor Floor_Actor = new Actor(_Scene3DContainers[0], "Floor");
                    Floor_Actor.Scale = new Vertex(0.001f, 0.001f, 0.001f);
                    Camera MainCamera_Camera = new Camera();
                    MainCamera_Camera.Translation = new Vertex(0, 0.6f, 0.8f);
                    MainCamera_Camera.Rotation = new Vertex(30, 0, 0);
                    Light Light1_Light = new Light();
                    Light1_Light.Translation = new Vertex(4, -4, -4);
                    Light1_Light.Color = Color.White;
                    Light1_Light.Attenuation = new Vertex(0.6f, 0.2f, 0.2f);
                    Light1_Light.Intensity = 0.3f;
                    Scene3D NewScene = new Scene3D("Scene_" + (CurrentGame.Scenes.Count + 1).ToString("000"));
                    NewScene.BackColor = Color.FromArgb(40, 40, 40);
                    NewScene.Type = SceneType.Scene3D;
                    DrawnSceneObject Floor_Object = new DrawnSceneObject("Floor", Floor_Actor);
                    NewScene.AddSceneObject(Floor_Object);
                    DrawnSceneObject Camera_Object = new DrawnSceneObject("Camera", MainCamera_Camera);
                    NewScene.AddSceneObject(Camera_Object);
                    DrawnSceneObject Light_Object = new DrawnSceneObject("Light", Light1_Light);
                    NewScene.AddSceneObject(Light_Object);
                    NewScene.ActiveCamera = new Camera(MainCamera_Camera);
                    NewScene.EditorCamera = new Camera(MainCamera_Camera);
                    this.CurrentScene = NewScene;
                    this.CurrentGame.Scenes.Add(NewScene);
                    ReturnValue = true;
                }
                else ReturnValue = false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ErrorString = ex.InnerException.ToString();
                }
                else ErrorString = ex.Message;
                ReturnValue = false;
            }
            if (ReturnValue) Update.Invoke(InterfaceUpdateMessage.SceneUpdated);
            return ReturnValue;
        }
        public bool AddAsset(SceneObject NewAsset)
        {
            if (NewAsset == null) return false;
            _CurrentGame.Assets.Add(NewAsset);
            Update.Invoke(InterfaceUpdateMessage.AssetsUpdated);
            return true;
        }
        public bool SetCurrentScene(int Index, ref string ErrorString)
        {
            bool Value;
            if (Index == -1)
            {
                Value = AddEmptyScene(SceneType.Scene2D, ref ErrorString);
            }
            else if (Index == -2)
            {
                Value = AddEmptyScene(SceneType.Scene3D, ref ErrorString);
            }
            else
            {
                if (this.CurrentGame.Scenes.Count > Index)
                {
                    this.CurrentScene = this.CurrentGame.Scenes[Index];
                    Value = true;
                }
                else
                {
                    ErrorString = "No Scene at requested index";
                    Value = false;
                }
                if(Value) Update.Invoke(InterfaceUpdateMessage.SceneUpdated);
            }
            return Value;
        }       
        private void OnInterfaceUpdate(InterfaceUpdateMessage Message)
        {

        }
        private void AcquireData()
        {
            this._Scene3DContainers = new List<OBJContainer>();

            String LibPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Engineer/";

            OBJContainer Floor_OBJ = new OBJContainer();
            Floor_OBJ.Load(LibPath + "/Library/Mesh/Floor.obj", null);
            if (Floor_OBJ.Geometries[0].Normals.Count == 0) Floor_OBJ.RecalculateNormals();
            Floor_OBJ.Repack();
            _Scene3DContainers.Add(Floor_OBJ);

            OBJContainer Cube_OBJ = new OBJContainer();
            Cube_OBJ.Load(LibPath + "Library/Mesh/Cube.obj", null);
            if (Cube_OBJ.Geometries[0].Normals.Count == 0) Cube_OBJ.RecalculateNormals();
            Cube_OBJ.Repack();
            _Scene3DContainers.Add(Cube_OBJ);

            OBJContainer Soldier_OBJ = new OBJContainer();
            Soldier_OBJ.Load(LibPath + "Library/Mesh/Soldier.obj", null);
            if (Soldier_OBJ.Geometries[0].Normals.Count == 0) Soldier_OBJ.RecalculateNormals();
            Soldier_OBJ.Repack();
            _Scene3DContainers.Add(Soldier_OBJ);

            XmlDocument Document = new XmlDocument();
            Document.Load(LibPath + "Library/Material/Default.mtx");
            XmlNode Main = Document.FirstChild;
            Material Mat = new Material(Main);
            Material.Default = Mat;

            GlobalSceneObjects = new List<SceneObject>();
            for(int i = 0; i < _Scene3DContainers.Count; i++)
            {
                Actor New_Actor = new Actor(_Scene3DContainers[i], _Scene3DContainers[i].Geometries[0].Name);
                New_Actor.Scale = new Vertex(0.001f, 0.001f, 0.001f);
                DrawnSceneObject New_Actor_Object = new DrawnSceneObject(_Scene3DContainers[i].Geometries[0].Name, New_Actor);
                GlobalSceneObjects.Add(New_Actor_Object);
            }
            Light NewLight = new Light();
            DrawnSceneObject New_Object = new DrawnSceneObject("New Light", NewLight);
            GlobalSceneObjects.Add(New_Object);
            Camera NewCamera = new Camera();
            New_Object = new DrawnSceneObject("New Camera", NewCamera);
            GlobalSceneObjects.Add(New_Object);
            Sprite NewSprite = new Sprite();
            New_Object = new DrawnSceneObject("New Sprite", NewSprite);
            _GlobalSceneObjects.Add(New_Object);
            ScriptSceneObject New_Script_Object = new ScriptSceneObject("New Event");
            GlobalSceneObjects.Add(New_Script_Object);
        }
        public static bool LoadGame(string FilePath, ref Game CurrentGame, ref string ErrorString)
        {
            try
            {
                string DirPath = FilePath.Replace(Path.GetFileName(FilePath), Path.GetFileNameWithoutExtension(FilePath)) + "\\";
                ZipFile.ExtractToDirectory(FilePath, DirPath);
                CurrentGame = Game.Deserialize(DirPath + Path.GetFileNameWithoutExtension(FilePath) + ".xml");
                List<string> Files = new List<string>(Directory.GetFiles(DirPath));
                for (int i = 0; i < CurrentGame.Scenes.Count; i++)
                {
                    for (int j = 0; j < CurrentGame.Scenes[i].Objects.Count; j++)
                    {
                        SceneObject_Interface.PostLoad(CurrentGame.Scenes[i].Objects[i], DirPath, Files);
                        CurrentGame.Scenes[i].Objects[j].ParentScene = CurrentGame.Scenes[i];
                    }
                }
                for (int i = 0; i < Files.Count; i++) File.Delete(Files[i]);
                Directory.Delete(DirPath);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ErrorString = ex.InnerException.ToString();
                }
                else ErrorString = ex.Message;
                return false;
            }
        }
        public static bool SaveGame(string FilePath, ref Game CurrentGame, ref string ErrorString)
        {
            try
            {
                if (File.Exists(FilePath)) File.Delete(FilePath);
                string DirPath = FilePath.Replace(Path.GetFileName(FilePath), Path.GetFileNameWithoutExtension(FilePath)) + "\\";
                Directory.CreateDirectory(DirPath);
                List<string> Files = new List<string>();
                for (int i = 0; i < CurrentGame.Scenes.Count; i++)
                {
                    for (int j = 0; j < CurrentGame.Scenes[i].Objects.Count; j++)
                    {
                        SceneObject_Interface.PostSave(CurrentGame.Scenes[i].Objects[j], DirPath, Files);
                    }
                }
                Files.Add(DirPath + Path.GetFileNameWithoutExtension(FilePath) + ".xml");
                Game.Serialize(CurrentGame, DirPath + Path.GetFileNameWithoutExtension(FilePath) + ".xml");
                ZipFile.CreateFromDirectory(DirPath, FilePath);
                for (int i = 0; i < Files.Count; i++) File.Delete(Files[i]);
                Directory.Delete(DirPath);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ErrorString = ex.InnerException.ToString();
                }
                else ErrorString = ex.Message;
                return false;
            }
        }
    }
}

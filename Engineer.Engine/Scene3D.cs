using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Engine
{
    public class Scene3D : Scene
    {
        protected Camera _ActiveCamera;
        protected Camera _EditorCamera;
        public Camera ActiveCamera
        {
            get
            {
                return _ActiveCamera;
            }

            set
            {
                _ActiveCamera = value;
            }
        }
        public Camera EditorCamera
        {
            get
            {
                return _ActiveCamera;
            }

            set
            {
                _ActiveCamera = value;
            }
        }
        public List<Actor> Actors
        {
            get
            {
                List<Actor> NewList = new List<Actor>();
                for (int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i].Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Objects[i]).Representation.Type == DrawObjectType.Actor)
                        NewList.Add(((DrawnSceneObject)Objects[i]).Representation as Actor);
                }
                return NewList;
            }
        }
        public List<Light> Lights
        {
            get
            {
                List<Light> NewList = new List<Light>();
                for (int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i].Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Objects[i]).Representation.Type == DrawObjectType.Light)
                        NewList.Add(((DrawnSceneObject)Objects[i]).Representation as Light);
                }
                return NewList;
            }
        }
        public List<Camera> Cameras
        {
            get
            {
                List<Camera> NewList = new List<Camera>();
                for(int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i].Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Objects[i]).Representation.Type == DrawObjectType.Camera)
                        NewList.Add(((DrawnSceneObject)Objects[i]).Representation as Camera);
                }
                return NewList;
            }
        }
        public Background Background
        {
            get
            {
                for (int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i].Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Objects[i]).Representation.Type == DrawObjectType.Background)
                        return ((DrawnSceneObject)Objects[i]).Representation as Background;
                }
                return null;
            }
        }
        public override bool AddSceneObject(SceneObject Object)
        {
            if (Object.Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Object).Representation.Type == DrawObjectType.Sprite) return false;
            Object.ParentScene = this;
            this._Objects.Add(Object);
            return true;
        }
        public Scene3D() : base()
        {
            this._ActiveCamera = new Camera();
            this._EditorCamera = new Camera();
        }
        public Scene3D(string Name) : base(Name)
        {
            this._ActiveCamera = new Camera();
            this._EditorCamera = new Camera();
        }
        public Scene3D(Scene3D S3D) : base(S3D)
        {
            this._ActiveCamera = new Camera(S3D._ActiveCamera);
            this._EditorCamera = new Camera(S3D.EditorCamera);
        }
    }
}

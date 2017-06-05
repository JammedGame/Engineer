using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Engineer.Engine
{
    public class Scene2D : Scene
    {
        [XmlIgnore]
        private Scene2DTransformation _Transformation;
        public Scene2DTransformation Transformation
        {
            get
            {
                return _Transformation;
            }

            set
            {
                _Transformation = value;
            }
        }
        [XmlIgnore]
        public List<Sprite> Sprites
        {
            get
            {
                List<Sprite> NewList = new List<Sprite>();
                for (int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i].Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Objects[i]).Representation.Type == DrawObjectType.Sprite)
                        NewList.Add(((DrawnSceneObject)Objects[i]).Representation as Sprite);
                }
                return NewList;
            }
        }
        public override bool AddSceneObject(SceneObject Object)
        {
            if (Object.Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Object).Representation.Type == DrawObjectType.Actor) return false;
            if (Object.Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Object).Representation.Type == DrawObjectType.Camera) return false;
            if (Object.Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Object).Representation.Type == DrawObjectType.Light) return false;
            if (Object.Type == SceneObjectType.DrawnSceneObject && ((DrawnSceneObject)Object).Representation.Type == DrawObjectType.Undefined) return false;
            Object.ParentScene = this;
            this._Objects.Add(Object);
            return true;
        }
        public Scene2D() : base()
        {
            this._Transformation = new Scene2DTransformation();
        }
        public Scene2D(string Name) : base(Name)
        {
            this._Transformation = new Scene2DTransformation();
        }
        public Scene2D(Scene2D S2D)
        {
            this._Transformation = new Scene2DTransformation(S2D._Transformation);
        }
    }
}

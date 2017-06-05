using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Engineer.Engine
{
    [XmlInclude(typeof(DrawObject))]
    [XmlInclude(typeof(Actor))]
    [XmlInclude(typeof(Background))]
    [XmlInclude(typeof(Camera))]
    [XmlInclude(typeof(Light))]
    [XmlInclude(typeof(Sprite))]
    public class DrawnSceneObject : SceneObject
    {
        private DrawObject _Representation;
        public DrawObject Representation
        {
            get
            {
                return _Representation;
            }

            set
            {
                _Representation = value;
            }
        }
        public DrawnSceneObject() : base()
        {
            this.Type = SceneObjectType.DrawnSceneObject;
            this.Representation = null;
            this.Events = new EventsPackage(EventHandlersPackage.NewDrawnSceneObjectEventsPackage());
        }
        public DrawnSceneObject(string Name, DrawObject Representation) : base(Name)
        {
            this.Type = SceneObjectType.DrawnSceneObject;
            this.Representation = Representation;
            this.Events = new EventsPackage(EventHandlersPackage.NewDrawnSceneObjectEventsPackage());
        }
        public DrawnSceneObject(DrawnSceneObject DSO, Scene ParentScene) : base(DSO, ParentScene)
        {
            if (DSO._Representation.Type == DrawObjectType.Actor) this._Representation = new Actor((Actor)DSO._Representation);
            else if (DSO._Representation.Type == DrawObjectType.Background) this._Representation = new Background((Background)DSO._Representation);
            else if (DSO._Representation.Type == DrawObjectType.Camera) this._Representation = new Camera((Camera)DSO._Representation);
            else if (DSO._Representation.Type == DrawObjectType.Light) this._Representation = new Light((Light)DSO._Representation);
            else if (DSO._Representation.Type == DrawObjectType.Sprite) this._Representation = new Sprite((Sprite)DSO._Representation);
            this.Events = new EventsPackage(DSO.Events, ParentScene);
        }
        public static void Serialize(DrawnSceneObject CurrentDrawnSceneObject, string Path)
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(DrawnSceneObject));
            using (TextWriter Writer = new StreamWriter(Path))
            {
                Serializer.Serialize(Writer, CurrentDrawnSceneObject);
            }
        }
        public static DrawnSceneObject Deserialize(string Path)
        {
            XmlSerializer Deserializer = new XmlSerializer(typeof(DrawnSceneObject));
            TextReader Reader = new StreamReader(Path);
            DrawnSceneObject CurrentDrawnSceneObject = (DrawnSceneObject)Deserializer.Deserialize(Reader);
            Reader.Close();
            return CurrentDrawnSceneObject;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Engineer.Engine
{
    public enum SceneType
    {
        Scene2D = 0,
        Scene3D = 1
    }
    [XmlInclude(typeof(SceneObject))]
    [XmlInclude(typeof(ScriptSceneObject))]
    [XmlInclude(typeof(DrawnSceneObject))]
    public class Scene
    {
        private string _ID;
        protected string _Name;
        protected SceneType _Type;
        protected Color _BackColor;
        private EventsPackage _Events;
        protected List<SceneObject> _Objects;
        private Dictionary<string, object> _Data;
        public string ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }
        public SceneType Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value;
            }
        }
        [XmlIgnore]
        public Color BackColor
        {
            get
            {
                return _BackColor;
            }

            set
            {
                _BackColor = value;
            }
        }
        public EventsPackage Events
        {
            get
            {
                return _Events;
            }

            set
            {
                _Events = value;
            }
        }
        public List<SceneObject> Objects
        {
            get
            {
                return _Objects;
            }
        }
        [XmlIgnore]
        public Dictionary<string, object> Data
        {
            get
            {
                return _Data;
            }

            set
            {
                _Data = value;
            }
        }
        public List<KeyValuePair<string, object>> IO_DataList
        {
            get
            {
                if (_Data == null) return null;
                List<KeyValuePair<string, object>> NewList = _Data.ToList();
                return NewList;
            }
            set
            {
                foreach (KeyValuePair<string, object> Pair in value)
                {
                    if (!_Data.ContainsKey(Pair.Key))
                    {
                        _Data.Add(Pair.Key, Pair.Value);
                    }
                }
            }
        }
        public List<ScriptSceneObject> Scripts
        {
            get
            {
                List<ScriptSceneObject> NewList = new List<ScriptSceneObject>();
                for (int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i].Type == SceneObjectType.ScriptSceneObject)
                    {
                        NewList.Add((ScriptSceneObject)Objects[i]);
                    }
                }
                return NewList;
            }
        }
        public virtual bool AddSceneObject(SceneObject Object)
        {
            Object.ParentScene = this;
            this._Objects.Add(Object);
            return true;
        }
        public Scene()
        {
            this._ID = Guid.NewGuid().ToString();
            this._Name = this._ID;
            this._BackColor = Color.FromArgb(40, 40, 40);
            this._Objects = new List<SceneObject>();
            this._Events = new EventsPackage(EventHandlersPackage.NewSceneEventsPackage());
            this._Data = new Dictionary<string, object>();
        }
        public Scene(string Name)
        {
            this._ID = Guid.NewGuid().ToString();
            this._Name = Name;
            this._BackColor = Color.FromArgb(40, 40, 40);
            this._Objects = new List<SceneObject>();
            this._Events = new EventsPackage(EventHandlersPackage.NewSceneEventsPackage());
            this._Data = new Dictionary<string, object>();
        }
        public Scene(Scene S)
        {
            this._ID = Guid.NewGuid().ToString();
            this._Name = S.Name;
            this._Type = S._Type;
            this._BackColor = S._BackColor;
            this._Events = new EventsPackage(S._Events, this);
            this._Objects = new List<SceneObject>();
            for(int i = 0; i < S._Objects.Count; i++)
            {
                if (S._Objects[i].Type == SceneObjectType.DrawnSceneObject) this._Objects.Add(new DrawnSceneObject((DrawnSceneObject)S._Objects[i], this));
                else if (S._Objects[i].Type == SceneObjectType.ScriptSceneObject) this._Objects.Add(new ScriptSceneObject((ScriptSceneObject)S._Objects[i], this));
            }
            this._Data = new Dictionary<string, object>(S.Data);
        }
    }
}

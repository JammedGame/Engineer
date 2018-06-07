using Engineer.Data;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Engineer.Engine.Updated
{
    public enum SceneObjectType
    {
        Undefined = 0,
        Drawn = 1,
        Script = 2,
        Sound = 3,
        Other = 4
    }
    public class SceneObject
    {
        private string _ID;
        private string _Name;
        private SceneObjectType _Type;
        private Scene _ParentScene;
        private EventsPackage _Events;
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
        public SceneObjectType Type
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
        public List<DictionaryEntry> IO_DataList
        {
            get
            {
                if (_Data == null) return null;
                List<DictionaryEntry> Entries = new List<DictionaryEntry>(_Data.Count);
                foreach (string Key in _Data.Keys)
                {
                    Entries.Add(new DictionaryEntry(Key, _Data[Key]));
                }
                return Entries;
            }
            set
            {
                Console.WriteLine("test");
                foreach (DictionaryEntry Pair in value)
                {
                    if (!_Data.ContainsKey(Pair.Key))
                    {
                        _Data[Pair.Key] = Pair.Value;
                    }
                }
            }
        }
        public SceneObject() : this(null)
        {
        }
        public SceneObject(SceneObject Old)
        {
            if (Old != null)
            {
                this._ID = Guid.NewGuid().ToString();
                this._Name = Old._Name;
                this._Type = Old._Type;
                this._Events = new EventsPackage();
                this._Data = new Dictionary<string, object>(Old._Data);
            }
            else
            {
                this._Name = Name;
                this._ID = Guid.NewGuid().ToString();
                this._Type = SceneObjectType.Undefined;
                this._Events = new EventsPackage();
                this._Data = new Dictionary<string, object>();
            }
        }
        public SceneObject(SceneObject Old, string Name) : this(Old)
        {
            this._Name = Name;
        }
        public SceneObject Copy()
        {
            return new SceneObject(this);
        }
    }
}

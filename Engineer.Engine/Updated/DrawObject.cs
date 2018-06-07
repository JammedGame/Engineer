using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Engineer.Engine.Updated
{
    public enum DrawObjectType
    {
        Undefined = 0,
        Image = 1,
        Sprite = 2,
        Tile = 3,
        Light = 4,
        Model = 5
    }
    class DrawObject
    {
        private bool _Active;
        private bool _Fixed;
        private bool _Visible;
        private bool _Modified;
        private Color _Paint;
        public bool Fixed { get => _Fixed; set => _Fixed = value; }
        public bool Active { get => _Active; set => _Active = value; }
        public bool Visible { get => _Visible; set => _Visible = value; }
        public bool Modified { get => _Modified; set => _Modified = value; }
        public Color Paint { get => _Paint; set => _Paint = value; }
        public DrawObject() : base()
        {
            this._Active = false;
            this._Fixed = false;
            this._Visible = false;
            this._Modified = false;
            this._Paint = Color.White;
        }
        public DrawObject(DrawObject Old)
    }
}

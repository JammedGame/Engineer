using Engineer.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameJam.FrogShift
{
    public class ResourceManager
    {
        public static ResourceManager RM;
        public ResourceManager()
        {
            XmlDocument Document = new XmlDocument();
            Document.Load("Library/Material/Default.mtx");
            XmlNode Main = Document.FirstChild;
            Material Mat = new Material(Main);
            Material.Default = Mat;
        }
        public void Init()
        {
            RM = this;
        }
    }
}

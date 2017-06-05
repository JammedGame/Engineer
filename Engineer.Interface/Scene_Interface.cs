using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineer.Engine;

namespace Engineer.Interface
{
    public class Scene_Interface
    {
        public static List<string> GetPossibleEventNames(Scene CurrentScene, string Identifier)
        {
            List<string> Names = new List<string>();
            List<ScriptSceneObject> Events = GetPossibleEvents(CurrentScene, Identifier);
            for (int i = 0; i < Events.Count; i++) Names.Add(Events[i].Name);
            return Names;
        }
        public static List<ScriptSceneObject> GetPossibleEvents(Scene CurrentScene, string Identifier)
        {
            string CodePart = "public void " + Identifier;
            List<ScriptSceneObject> Events = new List<ScriptSceneObject>();
            List<ScriptSceneObject> AllEvents = CurrentScene.Scripts;
            for (int i = 0; i < AllEvents.Count; i++)
            {
                if(AllEvents[i].Script!=null && AllEvents[i].Script.Contains(CodePart))
                {
                    Events.Add(AllEvents[i]);
                }
            }
            return Events;
        }
    }
}

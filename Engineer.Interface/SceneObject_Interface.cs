using Engineer.Data;
using Engineer.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Interface
{
    public class SceneObject_Interface
    {
        public static void PostLoad(SceneObject CurrentSceneObject, string DirPath, List<string> Files)
        {
            if (CurrentSceneObject.Type == SceneObjectType.DrawnSceneObject)
            {
                DrawnSceneObject Drawn = (DrawnSceneObject)CurrentSceneObject;
                if (Drawn.Representation.Type == DrawObjectType.Actor)
                {
                    Actor CurrentActor = (Actor)Drawn.Representation;
                    if (Files.Contains(DirPath + CurrentActor.ID + ".obj"))
                    {
                        OBJContainer OBJ = new OBJContainer();
                        OBJ.Load(DirPath + CurrentActor.ID + ".obj", null);
                        CurrentActor.Geometries = OBJ.Geometries;
                    }
                    for (int k = 0; k < CurrentActor.Materials.Count; k++)
                    {
                        for (int l = 0; l < CurrentActor.Materials[k].Nodes.Count; l++)
                        {
                            CurrentActor.Materials[k].Nodes[l].Holder = CurrentActor.Materials[k];
                            for (int m = 0; m < CurrentActor.Materials[k].Nodes[l].Values.Count; m++)
                            {
                                CurrentActor.Materials[k].Nodes[l].Values[m].Parent = CurrentActor.Materials[k].Nodes[l];
                            }
                            for (int m = 0; m < CurrentActor.Materials[k].Nodes[l].Inputs.Count; m++)
                            {
                                CurrentActor.Materials[k].Nodes[l].Inputs[m].Parent = CurrentActor.Materials[k].Nodes[l];
                                CurrentActor.Materials[k].Nodes[l].Inputs[m].InputTarget = MaterialNodeValue.FindConnection(CurrentActor.Materials[k].Nodes[l], CurrentActor.Materials[k].Nodes[l].Inputs[m].IO_InputParentID, CurrentActor.Materials[k].Nodes[l].Inputs[m].IO_InputName, MaterialValueType.Output);
                                if (CurrentActor.Materials[k].Nodes[l].Inputs[m].InputTarget != null) CurrentActor.Materials[k].Nodes[l].Inputs[m].InputTarget.OutputTargets.Add(CurrentActor.Materials[k].Nodes[l].Inputs[m]);
                            }
                            for (int m = 0; m < CurrentActor.Materials[k].Nodes[l].Outputs.Count; m++)
                            {
                                CurrentActor.Materials[k].Nodes[l].Outputs[m].Parent = CurrentActor.Materials[k].Nodes[l];
                            }
                        }
                    }
                }
                else if (Drawn.Representation.Type == DrawObjectType.Sprite)
                {
                    Sprite CurrentSprite = (Sprite)Drawn.Representation;
                    for (int k = 0; k < CurrentSprite.SpriteSets.Count; k++)
                    {
                        for (int l = 0;; l++)
                        {
                            string NewFilePath = DirPath + CurrentSprite.ID + "_" + k + "_" + l + ".png";
                            if (Files.Contains(NewFilePath))
                            {
                                FileStream Stream = new FileStream(NewFilePath, FileMode.Open);
                                CurrentSprite.SpriteSets[k].Sprite.Add(new Bitmap(Stream));
                                Stream.Close();
                            }
                            else break;
                        }
                    }
                }
            }
        }
        public static void PostSave(SceneObject CurrentSceneObject, string DirPath, List<string> Files)
        {
            if (CurrentSceneObject.Type == SceneObjectType.DrawnSceneObject)
            {
                DrawnSceneObject Drawn = (DrawnSceneObject)CurrentSceneObject;
                if (Drawn.Representation.Type == DrawObjectType.Actor)
                {
                    Actor CurrentActor = (Actor)Drawn.Representation;
                    if (CurrentActor.Geometries.Count > 0)
                    {
                        OBJContainer OBJ = new OBJContainer();
                        OBJ.Geometries = CurrentActor.Geometries;
                        string NewFilePath = DirPath + CurrentActor.ID + ".obj";
                        Files.Add(NewFilePath);
                        OBJ.Save(NewFilePath, null);
                    }
                }
                else if (Drawn.Representation.Type == DrawObjectType.Sprite)
                {
                    Sprite CurrentSprite = (Sprite)Drawn.Representation;
                    for (int k = 0; k < CurrentSprite.SpriteSets.Count; k++)
                    {
                        for (int l = 0; l < CurrentSprite.SpriteSets[k].Sprite.Count; l++)
                        {
                            string NewFilePath = DirPath + CurrentSprite.ID + "_" + k + "_" + l + ".png";
                            CurrentSprite.SpriteSets[k].Sprite[l].Save(NewFilePath, System.Drawing.Imaging.ImageFormat.Png);
                            Files.Add(NewFilePath);
                        }
                    }
                }
            }
        }
    }
}

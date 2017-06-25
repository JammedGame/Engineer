using Engineer.Engine;
using Engineer.Mathematics;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    public class Level
    {
        public static void Create(Scene2D CScene, ExternRunner Runner)
        {
            int[] LilipadsX = new int[] { 200, 600, 1000, 1400, 1800 };

            DrawnSceneObject Back = GameLogic.CreateStaticSprite("Back", global::GameJam.FrogShift.Properties.Resources.BG, new Vertex(0, 0, 0), new Vertex(Runner.Width, Runner.Height, 0), false);
            CScene.Data["Back"] = Back;
            CScene.AddSceneObject(Back);

            for (int i = 0; i < LilipadsX.Length; i++)
            {
                DrawnSceneObject Floor = GameLogic.CreateStaticSprite("Floor" + i, global::GameJam.FrogShift.Properties.Resources.lokvanj2, new Vertex(LilipadsX[i], 830, 0), new Vertex(200, 30, 0));
                CScene.AddSceneObject(Floor);                
                ((List<SceneObject>)(CScene.Data["Colliders"])).Add(Floor);
            }

            DrawnSceneObject Water = GameLogic.CreateStaticSprite("Water", global::GameJam.FrogShift.Properties.Resources.voda, new Vertex(0, 850, 0), new Vertex(1920, 1080, 0));
            CScene.Data["Water"] = Water;
            CScene.AddSceneObject(Water);
        }
    }
}

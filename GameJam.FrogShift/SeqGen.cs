using Engineer.Engine;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    class SeqGen
    {
        private List<int> intList;       

        public SeqGen()
        {            
            intList = null;
        }

        public List<int> GenerateSequence(int Num)
        {
            intList = new List<int>();
            int max = 0;
            int prev = 0;
            int temp = 0;
            int cnt = 0;
            int n = 0;
            Random rnd = new Random();
            for (int i = 0; i < Num + n; i++)
            {
                int num = rnd.Next(1, 5);
                temp = num;
                if ((Math.Abs(temp - prev)) <= 1) { cnt++; }
                else cnt = 0;
                if (cnt < 2)
                {
                    max += num;
                    intList.Add(max);
                }
                else n++;

            }
            return intList;
        }
        public List<int> GenerateArtIndexSequence(int Num, int maxVal)
        {
            List<int> indexList = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < Num; i++)
            {
                int num = rnd.Next(0, maxVal);
                indexList.Add(num);
            }
            return indexList;
        }
        public void CheckEnd(Scene CScene)
        {
            DrawnSceneObject Char = (DrawnSceneObject)CScene.Data["Frog"];
            List<DrawnSceneObject> Colliders = (List<DrawnSceneObject>)CScene.Data["Colliders"];
            DrawnSceneObject Last = Colliders[Colliders.Count - 1];
            if (Char.Visual.Translation.X + 2000 > Last.Visual.Translation.X)
            {
                List<int> LilipadList = this.GenerateSequence(100);
                List<int> LilipadArtList = this.GenerateArtIndexSequence(100, 2);

                for (int i = 0; i < LilipadList.Count; i++)
                {
                    DrawnSceneObject Floor = GameLogic.CreateStaticSprite("Floor" + i, ResourceManager.Images["lokvanj" + LilipadArtList[i]], new Vertex(Last.Visual.Translation.X + LilipadList[i] * 180, 830, 0), new Vertex(200, 30, 0));
                    CScene.AddSceneObject(Floor);
                    ((List<DrawnSceneObject>)(CScene.Data["Colliders"])).Add(Floor);
                }

                CScene.Objects.Remove((DrawnSceneObject)Char.Data["LL"]);
                CScene.Objects.Remove(Char);
                CScene.Objects.Remove((DrawnSceneObject)Char.Data["RL"]);
                CScene.Objects.Add((DrawnSceneObject)Char.Data["LL"]);
                CScene.Objects.Add(Char);
                CScene.Objects.Add((DrawnSceneObject)Char.Data["RL"]);
            }
        }
    }
}

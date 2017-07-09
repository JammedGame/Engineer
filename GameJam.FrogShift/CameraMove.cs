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
    class CameraMove
    {
        public static int moveRatio = 2;
        float move;
        float Difficulty = 1.0f;
        int Delay = 500;
        
        Scene2D _CScene2D;

        public CameraMove(Scene _CScene)
        {
            _CScene2D = (Scene2D)_CScene;
        }

        public void MoveCamera(Scene _CScene, Runner Run)
        {
            Delay--;
            if (Delay > 0) return;

            move = CameraMove.moveRatio;
            move *= GameLogic._GlobalScale;
           
            DrawnSceneObject Frog = (DrawnSceneObject)_CScene.Data["Frog"];
            Frog.Visual.Translation = new Vertex(Frog.Visual.Translation.X - MoveSpeed(), Frog.Visual.Translation.Y, 0);
            List<DrawnSceneObject> Colliders = (List<DrawnSceneObject>)_CScene.Data["Colliders"];
            for (int i = 0; i < Colliders.Count; i++)
            {
                Colliders[i].Visual.Translation = new Vertex(Colliders[i].Visual.Translation.X - MoveSpeed(), Colliders[i].Visual.Translation.Y, 0);
            }

            if (_CScene.Data.ContainsKey("Splash") && _CScene.Data["Splash"] != null)
            {
                ((DrawnSceneObject)_CScene.Data["Splash"]).Visual.Translation = new Vertex(((DrawnSceneObject)_CScene.Data["Splash"]).Visual.Translation.X - MoveSpeed(), ((DrawnSceneObject)_CScene.Data["Splash"]).Visual.Translation.Y, 0);
            }
        }
        private float MoveSpeed()
        {
            if (((GameLogic.DiffTime /20) == 1) && Difficulty < 6) { Difficulty++; GameLogic.DiffTime = 0; }
            float movement = this.move * Difficulty;
            return movement;
        }
    }
}

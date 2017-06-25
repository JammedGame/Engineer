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
        Scene2D _CScene2D;

        public CameraMove(Scene _CScene)
        {
            _CScene2D = (Scene2D)_CScene;
        }
    
        public void MoveCamera(Scene _CScene, Runner Run)
        {
            move = CameraMove.moveRatio;
            move *= GameLogic._GlobalScale;
            /*_CScene2D = (Scene2D)_CScene;
            _CScene2D.Transformation.Translation = new Vertex(_CScene2D.Transformation.Translation.X - move, _CScene2D.Transformation.Translation.Y, 0);
            DrawnSceneObject Water = (DrawnSceneObject)_CScene.Data["Water"];
            DrawnSceneObject WaterSurface = (DrawnSceneObject)_CScene.Data["WaterSurface"];
            DrawnSceneObject Back = (DrawnSceneObject)_CScene.Data["Back"];
            DrawnSceneObject TimerHigh = (DrawnSceneObject)_CScene.Data["TimerHigh"];
            DrawnSceneObject TimerLow = (DrawnSceneObject)_CScene.Data["TimerLow"];
            WaterSurface.Representation.Translation = new Vertex(Water.Representation.Translation.X + move, 825*GameLogic._GlobalScale, 0);
            Water.Representation.Translation = new Vertex(Water.Representation.Translation.X + move, 850*GameLogic._GlobalScale, 0);
            Back.Representation.Translation = new Vertex(Back.Representation.Translation.X + move, 0, 0);*/
            DrawnSceneObject Frog = (DrawnSceneObject)_CScene.Data["Frog"];
            Frog.Representation.Translation = new Vertex(Frog.Representation.Translation.X - move, Frog.Representation.Translation.Y, 0);
            List<DrawnSceneObject> Colliders = (List<DrawnSceneObject>)_CScene.Data["Colliders"];
            for (int i = 0; i < Colliders.Count; i++)
            {
                Colliders[i].Representation.Translation = new Vertex(Colliders[i].Representation.Translation.X - move, Colliders[i].Representation.Translation.Y, 0);
            }
        }
    }
}

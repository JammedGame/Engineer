using Engineer.Engine;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    class CameraMove
    {
        int move;
        Scene2D _CScene2D;

        public CameraMove(Scene _CScene)
        {
            move = 1;
            _CScene2D = (Scene2D)_CScene;
        }
    
        public void MoveCamera(Scene _CScene)
        {
            _CScene2D = (Scene2D)_CScene;
            _CScene2D.Transformation.Translation = new Vertex(_CScene2D.Transformation.Translation.X - move, 0, 0);
            DrawnSceneObject Water = (DrawnSceneObject)_CScene.Data["Water"];
            DrawnSceneObject Back = (DrawnSceneObject)_CScene.Data["Back"];
            DrawnSceneObject TimerHigh = (DrawnSceneObject)_CScene.Data["TimerHigh"];
            DrawnSceneObject TimerLow = (DrawnSceneObject)_CScene.Data["TimerLow"];
            Water.Representation.Translation = new Vertex(Water.Representation.Translation.X + move, 850*GameLogic._GlobalScale, 0);
            Back.Representation.Translation = new Vertex(Back.Representation.Translation.X + move, 0, 0);
            TimerHigh.Representation.Translation = new Vertex(TimerHigh.Representation.Translation.X + move, 0, 0);
            TimerLow.Representation.Translation = new Vertex(TimerLow.Representation.Translation.X + move, 0, 0);

        }
    }
}

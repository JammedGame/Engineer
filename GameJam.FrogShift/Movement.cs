using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineer.Engine;
using Engineer.Runner;
using System.Drawing;

namespace GameJam.FrogShift
{
    public class Movement
    {
        private Runner _Runner;
        private DrawnSceneObject _Player;
        private List<SceneObject> _Colliders = new List<SceneObject>();
        public Movement(Runner NewRunner, DrawnSceneObject Player, List<SceneObject> Colliders)
        {
            this._Runner = NewRunner;
            this._Player = Player;
            this._Colliders = Colliders;
        }
        public void KeyDownEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.A)
            {
                if (Convert.ToBoolean(_Player.Data["flying"]))
                {
                    _Player.Representation.Translation = new Vertex(_Player.Representation.Translation.X - 6, _Player.Representation.Translation.Y, 0);
                    Character.UpdateLegs(_Player);
                }
            }
            if (E.KeyDown == KeyType.D)
            {
                if (Convert.ToBoolean(_Player.Data["flying"]))
                {
                    _Player.Representation.Translation = new Vertex(_Player.Representation.Translation.X + 6, _Player.Representation.Translation.Y, 0);
                    Character.UpdateLegs(_Player);
                }
            }
            if (E.KeyDown == KeyType.Space)
            {
                if (Convert.ToBoolean(_Player.Data["flying"]) == false)
                {
                    _Player.Data["skokBrojac"] = 25;
                    _Player.Data["flying"] = true;
                    ((Sprite)(_Player.Representation)).SetSpriteSet(1);
                    ((Sprite)(((DrawnSceneObject)(_Player.Data["LL"])).Representation)).SetSpriteSet(1);
                    ((Sprite)(((DrawnSceneObject)(_Player.Data["RL"])).Representation)).SetSpriteSet(1);
                }
            }
            if (E.KeyDown == KeyType.Escape)
            {
                _Runner.Close();
            }
        }
        public void CheckCollision()
        {
            bool collided = Convert.ToBoolean(_Player.Data["colliding"]);
            bool flying = Convert.ToBoolean(_Player.Data["flying"]);
            int tmpSkokBrojac = Convert.ToInt32(_Player.Data["skokBrojac"]);
            int waterLevel = (int)(850 * GameLogic._GlobalScale);
            float frogCenter = _Player.Representation.Translation.Y + _Player.Representation.Scale.Y / 2;
            if (tmpSkokBrojac > 0)
            {

                tmpSkokBrojac -= 1;
                _Player.Data["skokBrojac"] = tmpSkokBrojac;

                Vertex lastPos = _Player.Representation.Translation;



                if (waterLevel > frogCenter)
                {
                    lastPos.Y -= tmpSkokBrojac;
                    _Player.Representation.Translation = lastPos;
                }
                else
                {
                    lastPos.Y += tmpSkokBrojac;
                    _Player.Representation.Translation = lastPos;
                }
            }
            else
            {
                Vertex lastPos = _Player.Representation.Translation;
                Vertex playerScale = _Player.Representation.Scale;
                //lastPos.Y += (float)12;
                collided = false;

                _Player.Representation.Translation = lastPos;

                for (int i = 0; i < _Colliders.Count; i++)
                {
                    DrawnSceneObject colliderDSO = (DrawnSceneObject)_Colliders[i];
                    Vertex colliderPos = colliderDSO.Representation.Translation;
                    Vertex coliderScale = colliderDSO.Representation.Scale;

                    Rectangle playerRect = new Rectangle(Convert.ToInt32(lastPos.X + playerScale.X / 4), Convert.ToInt32(lastPos.Y), (int)(playerScale.X / 2), (int)playerScale.Y);
                    Rectangle colliderRect = new Rectangle(Convert.ToInt32(colliderPos.X), Convert.ToInt32(colliderPos.Y), Convert.ToInt32(coliderScale.X), Convert.ToInt32(coliderScale.Y));

                    if (playerRect.IntersectsWith(colliderRect))
                    {
                        collided = true;
                        _Player.Representation.Translation = new Vertex(_Player.Representation.Translation.X, colliderPos.Y - _Player.Representation.Scale.Y + 1, _Player.Representation.Translation.Z);
                        flying = false;
                        break;
                    }
                }

                if (!collided)
                {
                    if (waterLevel > frogCenter)
                    {
                        lastPos.Y += (int)_Player.Data["padBrojac"] + 1;
                        _Player.Data["padBrojac"] = (int)_Player.Data["padBrojac"] + 1;
                        _Player.Data["flying"] = true;
                        _Player.Representation.Translation = lastPos;

                    }
                    else
                    {
                        int tmpBrojac = (int)_Player.Data["padBrojac"];
                        _Player.Data["padBrojac"] = (int)_Player.Data["skokBrojac"];
                        _Player.Data["skokBrojac"] = tmpBrojac;

                        //lastPos.Y += (int)_Player.Data["padBrojac"] + 1;
                        //_Player.Data["padBrojac"] = (int)_Player.Data["padBrojac"] + 1;
                        //_Player.Data["flying"] = true;
                        //_Player.Representation.Translation = lastPos;
                    }
                }
                else
                {
                    _Player.Data["flying"] = false;
                    ((Sprite)(_Player.Representation)).SetSpriteSet(0);
                    ((Sprite)(((DrawnSceneObject)(_Player.Data["LL"])).Representation)).SetSpriteSet(0);
                    ((Sprite)(((DrawnSceneObject)(_Player.Data["RL"])).Representation)).SetSpriteSet(0);
                    _Player.Data["padBrojac"] = 0;
                }
            }
            //_Physics.RunSimulation();
            _Player.Data["colliding"] = collided;
        }
    }
}

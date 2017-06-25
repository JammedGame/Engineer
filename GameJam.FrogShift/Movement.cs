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
        private bool _ADown;
        private bool _DDown;
        private Runner _Runner;
        private DrawnSceneObject _Player;
        private List<DrawnSceneObject> _Colliders = new List<DrawnSceneObject>();
        public Movement(Runner NewRunner, DrawnSceneObject Player, List<DrawnSceneObject> Colliders)
        {
            this._Runner = NewRunner;
            this._Player = Player;
            this._Colliders = Colliders;
            this._Player.Data["underWater"] = false;
        }
        public void KeyPressEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.A) _ADown = true;
            if (E.KeyDown == KeyType.D) _DDown = true;
        }
        public void KeyUpEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.A) _ADown = false;
            if (E.KeyDown == KeyType.D) _DDown = false;
        }
        public void KeyDownEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.Space)
            {
                if (Convert.ToBoolean(_Player.Data["flying"]) == false)
                {
                    _Player.Data["skokBrojac"] = 30;
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
            if (Convert.ToBoolean(_Player.Data["flying"]))
            {
                if (_ADown)
                {
                    _Player.Representation.Translation = new Vertex(_Player.Representation.Translation.X - (10* GameLogic._GlobalScale), _Player.Representation.Translation.Y, 0);
                    Character.UpdateLegs(_Player);
                }
                if (_DDown)
                {
                    _Player.Representation.Translation = new Vertex(_Player.Representation.Translation.X + (10* GameLogic._GlobalScale), _Player.Representation.Translation.Y, 0);
                    Character.UpdateLegs(_Player);
                }
            }

            Vertex lastPos = _Player.Representation.Translation;
            if (Convert.ToBoolean(_Player.Data["underWater"]))
            {
                lastPos = new Vertex(_Player.Representation.Translation.X - _Player.Representation.Scale.X, _Player.Representation.Translation.Y - _Player.Representation.Scale.Y, 0);
            }

            bool collided = Convert.ToBoolean(_Player.Data["colliding"]);
            bool flying = Convert.ToBoolean(_Player.Data["flying"]);
            int tmpSkokBrojac = Convert.ToInt32(_Player.Data["skokBrojac"]);
            int waterLevel = (int)(850 * GameLogic._GlobalScale);
            float frogCenter = lastPos.Y + Math.Abs(_Player.Representation.Scale.Y) / 2;

            
            Vertex playerScale = new Vertex(_Player.Representation.Scale.X, Math.Abs(_Player.Representation.Scale.Y), 0);
            if (tmpSkokBrojac > 0)
            {

                tmpSkokBrojac -= 1;
                _Player.Data["skokBrojac"] = tmpSkokBrojac;

                if (waterLevel > frogCenter)
                {
                    lastPos.Y -= tmpSkokBrojac * GameLogic._GlobalScale;
                    _Player.Representation.Translation = lastPos;
                }
                else
                {
                    lastPos.Y += tmpSkokBrojac * GameLogic._GlobalScale;
                    _Player.Representation.Translation = lastPos;
                }
            }
            else
            {
                //lastPos.Y += (float)12;
                collided = false;

                _Player.Representation.Translation = lastPos;

                for (int i = 0; i < _Colliders.Count; i++)
                {
                    DrawnSceneObject colliderDSO = (DrawnSceneObject)_Colliders[i];
                    Vertex colliderPos = colliderDSO.Representation.Translation;
                    Vertex coliderScale = colliderDSO.Representation.Scale;

                    Rectangle playerRect = new Rectangle(Convert.ToInt32(lastPos.X + (playerScale.X / 4) + (playerScale.X / 8)), Convert.ToInt32(lastPos.Y + (playerScale.Y / 4)), (int)(playerScale.X / 4), (int)(playerScale.Y / 2));
                    Rectangle colliderRect = new Rectangle(Convert.ToInt32(colliderPos.X), Convert.ToInt32(colliderPos.Y), Convert.ToInt32(coliderScale.X), Convert.ToInt32(coliderScale.Y));


                    if (playerRect.IntersectsWith(colliderRect))
                    {
                        collided = true;

                        if (!Convert.ToBoolean(_Player.Data["underWater"]))
                        {
                            _Player.Representation.Translation = lastPos = new Vertex(lastPos.X, colliderPos.Y - coliderScale.Y - playerScale.Y / 2 + 1, 0);

                        }
                        else
                        {
                             _Player.Representation.Translation = lastPos = new Vertex(lastPos.X, colliderPos.Y - coliderScale.Y, 0);
                        }

                        flying = false;
                        break;
                    }
                }

                if (!collided)
                {
                    if (waterLevel > frogCenter)
                    {

                        if (Convert.ToBoolean(_Player.Data["underWater"]))
                        {
                            int tmpBrojac = (int)_Player.Data["padBrojac"];
                            _Player.Data["padBrojac"] = (int)_Player.Data["skokBrojac"];
                            _Player.Data["skokBrojac"] = tmpBrojac;
                            _Player.Data["underWater"] = false;

                        }
                        lastPos.Y += ((int)_Player.Data["padBrojac"] + 1)*GameLogic._GlobalScale;
                        _Player.Data["padBrojac"] = (int)_Player.Data["padBrojac"] + 1;
                        _Player.Data["flying"] = true;
                        _Player.Representation.Translation = lastPos;

                    }
                    else
                    {

                        if (!Convert.ToBoolean(_Player.Data["underWater"]))
                        {
                            int tmpBrojac = (int)_Player.Data["padBrojac"];
                            _Player.Data["padBrojac"] = (int)_Player.Data["skokBrojac"];
                            _Player.Data["skokBrojac"] = tmpBrojac;
                            _Player.Data["underWater"] = true;

                        }

                        lastPos.Y -= ((int)_Player.Data["padBrojac"] + 1)* GameLogic._GlobalScale;
                        _Player.Data["padBrojac"] = (int)_Player.Data["padBrojac"] + 1;
                        _Player.Data["flying"] = true;
                        _Player.Representation.Translation = lastPos;
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
            _Player.Data["colliding"] = collided;
            if(Convert.ToBoolean(_Player.Data["underWater"]))
            {
                _Player.Representation.Rotation = new Vertex(0, 0, 180);
                _Player.Representation.Translation = new Vertex(lastPos.X + _Player.Representation.Scale.X, lastPos.Y + _Player.Representation.Scale.Y, 0);
            }
            else _Player.Representation.Rotation = new Vertex(0, 0, 0);
        }
        public void CheckWaterLevel(Scene2D CScene)
        {
            if (Convert.ToBoolean(_Player.Data["underWater"]))
            {
                CScene.Transformation.Translation = new Vertex(CScene.Transformation.Translation.X, -600 * GameLogic._GlobalScale, 0);
            }
            else
            {
                CScene.Transformation.Translation = new Vertex(CScene.Transformation.Translation.X, 0, 0);
            }
        }
    }
}

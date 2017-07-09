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
        public bool _ADown;
        public bool _DDown;
        private Runner _Runner;
        private DrawnSceneObject _Player;
        private List<DrawnSceneObject> _Colliders = new List<DrawnSceneObject>();
        private GameTimer Time;
        public Movement(Runner NewRunner, DrawnSceneObject Player, List<DrawnSceneObject> Colliders, GameTimer NTime)
        {
            this._Runner = NewRunner;
            this._Player = Player;
            this._Colliders = Colliders;
            this._Player.Data["underWater"] = false;
            this.Time = NTime;
        }
        public void KeyPressEvent(Game G, EventArguments E)
        {
            if (GameLogic.GameOver) return;
            if (E.KeyDown == KeyType.A) _ADown = true;
            if (E.KeyDown == KeyType.D) _DDown = true;
        }
        public void KeyUpEvent(Game G, EventArguments E)
        {
            if (GameLogic.GameOver)
            {
                return;
            }
            if (E.KeyDown == KeyType.A) _ADown = false;
            if (E.KeyDown == KeyType.D) _DDown = false;
        }
        public void KeyDownEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.Escape)
            {
                _Runner.Close();
            }
            if (GameLogic.GameOver) return;
            if (E.KeyDown == KeyType.Space)
            {
                if (Convert.ToBoolean(_Player.Data["flying"]) == false)
                {
                    _Player.Data["skokBrojac"] = 30;
                    _Player.Data["flying"] = true;
                    ((Sprite)(_Player.Visual)).SetSpriteSet(1);
                    ((Sprite)(((DrawnSceneObject)(_Player.Data["LL"])).Visual)).SetSpriteSet(1);
                    ((Sprite)(((DrawnSceneObject)(_Player.Data["RL"])).Visual)).SetSpriteSet(1);
                    AudioPlayer.PlaySound(AudioPlayer.Kre, false, 100);
                }
            }
        }
        public void CheckCollision()
        {
            if (Convert.ToBoolean(_Player.Data["flying"]))
            {
                if (_ADown)
                {
                    _Player.Visual.Translation = new Vertex(_Player.Visual.Translation.X - (10 * GameLogic._GlobalScale), _Player.Visual.Translation.Y, 0);
                    Character.UpdateLegs(_Player);
                }
                if (_DDown)
                {
                    _Player.Visual.Translation = new Vertex(_Player.Visual.Translation.X + (10 * GameLogic._GlobalScale), _Player.Visual.Translation.Y, 0);
                    Character.UpdateLegs(_Player);
                }
            }

            Vertex lastPos = _Player.Visual.Translation;
            if (Convert.ToBoolean(_Player.Data["underWater"]))
            {
                lastPos = new Vertex(_Player.Visual.Translation.X - _Player.Visual.Scale.X, _Player.Visual.Translation.Y - _Player.Visual.Scale.Y, 0);
            }

            bool collided = Convert.ToBoolean(_Player.Data["colliding"]);
            bool flying = Convert.ToBoolean(_Player.Data["flying"]);
            int tmpSkokBrojac = Convert.ToInt32(_Player.Data["skokBrojac"]);
            int waterLevel = (int)(850 * GameLogic._GlobalScale);
            float frogCenter = lastPos.Y + Math.Abs(_Player.Visual.Scale.Y) / 2;


            Vertex playerScale = new Vertex(_Player.Visual.Scale.X, Math.Abs(_Player.Visual.Scale.Y), 0);
            if (tmpSkokBrojac > 0)
            {

                tmpSkokBrojac -= 1;
                _Player.Data["skokBrojac"] = tmpSkokBrojac;

                if (waterLevel > frogCenter)
                {
                    lastPos.Y -= tmpSkokBrojac * GameLogic._GlobalScale;
                    _Player.Visual.Translation = lastPos;
                }
                else
                {
                    lastPos.Y += tmpSkokBrojac * GameLogic._GlobalScale;
                    _Player.Visual.Translation = lastPos;
                }
            }
            else
            {
                //lastPos.Y += (float)12;
                collided = false;

                _Player.Visual.Translation = lastPos;

                for (int i = 0; i < _Colliders.Count; i++)
                {
                    DrawnSceneObject colliderDSO = (DrawnSceneObject)_Colliders[i];
                    Vertex colliderPos = colliderDSO.Visual.Translation;
                    Vertex coliderScale = colliderDSO.Visual.Scale;

                    Rectangle playerRect = new Rectangle(Convert.ToInt32(lastPos.X + (playerScale.X / 4) + (playerScale.X / 8)), Convert.ToInt32(lastPos.Y + (playerScale.Y / 4)), (int)(playerScale.X / 4), (int)(playerScale.Y / 2));
                    Rectangle colliderRect = new Rectangle(Convert.ToInt32(colliderPos.X), Convert.ToInt32(colliderPos.Y), Convert.ToInt32(coliderScale.X), Convert.ToInt32(coliderScale.Y));


                    if (playerRect.IntersectsWith(colliderRect))
                    {
                        collided = true;

                        if (!Convert.ToBoolean(_Player.Data["underWater"]))
                        {
                            _Player.Visual.Translation = lastPos = new Vertex(lastPos.X, colliderPos.Y - coliderScale.Y - playerScale.Y / 2 + 1, 0);

                        }
                        else
                        {
                            _Player.Visual.Translation = lastPos = new Vertex(lastPos.X, colliderPos.Y - coliderScale.Y, 0);
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
                        if(!GameLogic.GameOver) lastPos.Y += ((int)_Player.Data["padBrojac"] + 1) * GameLogic._GlobalScale;
                        _Player.Data["padBrojac"] = (int)_Player.Data["padBrojac"] + 1;
                        _Player.Data["flying"] = true;
                        _Player.Visual.Translation = lastPos;

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

                        if (!GameLogic.GameOver) lastPos.Y -= ((int)_Player.Data["padBrojac"] + 1) * GameLogic._GlobalScale;
                        _Player.Data["padBrojac"] = (int)_Player.Data["padBrojac"] + 1;
                        _Player.Data["flying"] = true;
                        _Player.Visual.Translation = lastPos;
                    }
                }
                else
                {
                    _Player.Data["flying"] = false;
                    ((Sprite)(_Player.Visual)).SetSpriteSet(0);
                    ((Sprite)(((DrawnSceneObject)(_Player.Data["LL"])).Visual)).SetSpriteSet(0);
                    ((Sprite)(((DrawnSceneObject)(_Player.Data["RL"])).Visual)).SetSpriteSet(0);
                    _Player.Data["padBrojac"] = 0;
                }
            }
            _Player.Data["colliding"] = collided;
            if (Convert.ToBoolean(_Player.Data["underWater"]))
            {
                _Player.Visual.Rotation = new Vertex(0, 0, 180);
                _Player.Visual.Translation = new Vertex(lastPos.X + _Player.Visual.Scale.X, lastPos.Y + _Player.Visual.Scale.Y, 0);
            }
            else _Player.Visual.Rotation = new Vertex(0, 0, 0);

            if (Convert.ToBoolean(_Player.Data["underWater"]))
            {
                if(GameLogic.Up && !GameLogic.Switch)
                {
                    GameLogic.Up = false;
                    GameLogic.GameOver = true;
                }
                else if (GameLogic.Up && GameLogic.Switch)
                {
                    Time.ResetTime();
                    GameLogic.Up = false;
                    GameLogic.Switch = false;
                    GameLogic.SplashFlag = true;
                }
            }
            else
            {
                if (!GameLogic.Up && !GameLogic.Switch)
                {
                    GameLogic.Up = true;
                    GameLogic.GameOver = true;
                }
                else if (!GameLogic.Up && GameLogic.Switch)
                {
                    Time.ResetTime();
                    GameLogic.Up = true;
                    GameLogic.Switch = false;
                    GameLogic.SplashFlag = true;
                }
            }
        }
        public void CheckWaterLevel(Scene2D CScene,Runner Runner)
        {
            DrawnSceneObject TimerHigh = (DrawnSceneObject)CScene.Data["TimerHigh"];
            DrawnSceneObject TimerLow = (DrawnSceneObject)CScene.Data["TimerLow"];

            List<DrawnSceneObject> HSDigit = new List<DrawnSceneObject>();

            for (int i = 0; i < 8; i++)
            {
                HSDigit.Add((DrawnSceneObject)CScene.Data["number" + i]);
            }


            if (Convert.ToBoolean(_Player.Data["underWater"]))
            {
                CScene.Transformation.Translation = new Vertex(CScene.Transformation.Translation.X, -600 * GameLogic._GlobalScale, 0);
               
                TimerHigh.Visual.Translation = new Vertex((Runner.Width / 2.0f) - 100 * GameLogic._GlobalScale, Runner.Height + 400 * GameLogic._GlobalScale, 0);
                TimerLow.Visual.Translation = new Vertex((Runner.Width / 2.0f), Runner.Height+400 * GameLogic._GlobalScale, 0);

                for (int i = 0; i < 8; i++)
                {
                    HSDigit[i].Visual.Translation = new Vertex((Runner.Width - 40 * GameLogic._GlobalScale * (i + 1)), Runner.Height + 475 * GameLogic._GlobalScale, 0);

                }
            }
            else
            {
                CScene.Transformation.Translation = new Vertex(CScene.Transformation.Translation.X, 0, 0);

                TimerHigh.Visual.Translation = new Vertex((Runner.Width / 2.0f) - 100 * GameLogic._GlobalScale, 0, 0);
                TimerLow.Visual.Translation = new Vertex((Runner.Width / 2.0f), 0, 0);


                for (int i = 0; i < 8; i++)
                {
                    HSDigit[i].Visual.Translation = new Vertex((Runner.Width - 40 * GameLogic._GlobalScale * (i + 1)),0, 0);

                }           
            }
        }
    }
}

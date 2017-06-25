using Engineer.Engine;
using Engineer.Mathematics;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    public class GameTimer
    {
        private long gameTime;        
        DrawnSceneObject t1;
        DrawnSceneObject t2;
        Random rnd = new Random();

        public GameTimer(Scene _CScene, Runner Runner)
        {
            gameTime = rnd.Next(10,15);
            SpriteSet Digit0 = new SpriteSet("0", global::GameJam.FrogShift.Properties.Resources.broj0);
            SpriteSet Digit1 = new SpriteSet("1", global::GameJam.FrogShift.Properties.Resources.broj1);
            SpriteSet Digit2 = new SpriteSet("2", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit3 = new SpriteSet("3", global::GameJam.FrogShift.Properties.Resources.broj3);
            SpriteSet Digit4 = new SpriteSet("4", global::GameJam.FrogShift.Properties.Resources.broj4);
            SpriteSet Digit5 = new SpriteSet("5", global::GameJam.FrogShift.Properties.Resources.broj5);
            SpriteSet Digit6 = new SpriteSet("6", global::GameJam.FrogShift.Properties.Resources.broj6);
            SpriteSet Digit7 = new SpriteSet("7", global::GameJam.FrogShift.Properties.Resources.broj7);
            SpriteSet Digit8 = new SpriteSet("8", global::GameJam.FrogShift.Properties.Resources.broj8);
            SpriteSet Digit9 = new SpriteSet("9", global::GameJam.FrogShift.Properties.Resources.broj9);
            Sprite Digits = new Sprite();
            Digits.SpriteSets.Add(Digit0);
            Digits.SpriteSets.Add(Digit1);
            Digits.SpriteSets.Add(Digit2);
            Digits.SpriteSets.Add(Digit3);
            Digits.SpriteSets.Add(Digit4);
            Digits.SpriteSets.Add(Digit5);
            Digits.SpriteSets.Add(Digit6);
            Digits.SpriteSets.Add(Digit7);
            Digits.SpriteSets.Add(Digit8);
            Digits.SpriteSets.Add(Digit9);
            Sprite HighDigits = new Sprite(Digits);

            Digits.Scale = new Vertex(100 * GameLogic._GlobalScale, 160* GameLogic._GlobalScale, 0);
            Digits.Translation = new Vertex((Runner.Width/2.0f), 0 , 0);
            t1 = new DrawnSceneObject("Time1", Digits);
            _CScene.Data["TimerLow"] = t1;
            _CScene.AddSceneObject(t1);
            HighDigits.Scale = new Vertex(100 * GameLogic._GlobalScale, 160 * GameLogic._GlobalScale, 0);
            HighDigits.Translation = new Vertex((Runner.Width / 2.0f) - 100* GameLogic._GlobalScale, 0, 0);
            t2 = new DrawnSceneObject("Time2", HighDigits);            
            _CScene.AddSceneObject(t2);
            _CScene.Data["TimerHigh"]=t2;
        }
        public void ResetTime()
        {
            gameTime = rnd.Next(10, 15);
        }

        public void DecTime()
        {
            gameTime--;
            if (gameTime <= 3)
            {
                GameLogic.Switch = true;
            }
            if (gameTime <= 0)
            {
                GameLogic.GameOver = true;
            }
            if (gameTime >= 0)
                for (int i = 0; i < 10; i++)
                {
                    if (((gameTime) % 10) == i) ((Sprite)(t1.Representation)).UpdateSpriteSet(i);
                    if (((gameTime) / 10) == i) ((Sprite)(t2.Representation)).UpdateSpriteSet(i);
                }
        }
        public static void ChangeDrawnSceneObjectImage(DrawnSceneObject Object, Bitmap Image)
        {
            SpriteSet Set = ((Sprite)(Object.Representation)).SpriteSets[0];
            Set.Sprite.Clear();
            Set.Sprite.Add(Image);
        }
    }   
}

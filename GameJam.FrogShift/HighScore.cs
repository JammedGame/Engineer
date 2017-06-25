using System;
using Engineer.Engine;
using Engineer.Mathematics;
using Engineer.Runner;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    public class HighScore
    {
        private int  score;
        List<DrawnSceneObject> dsoDigits;
        public HighScore(Scene _CScene, Runner Runner)
        {
            this.score = 0;
            dsoDigits = new List<DrawnSceneObject>();
            SpriteSet Digit0 = new SpriteSet("0", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit1 = new SpriteSet("1", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit2 = new SpriteSet("2", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit3 = new SpriteSet("3", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit4 = new SpriteSet("4", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit5 = new SpriteSet("5", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit6 = new SpriteSet("6", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit7 = new SpriteSet("7", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit8 = new SpriteSet("8", global::GameJam.FrogShift.Properties.Resources.broj2);
            SpriteSet Digit9 = new SpriteSet("9", global::GameJam.FrogShift.Properties.Resources.broj2);
            Sprite Digits = new Sprite();
            List<Sprite> DigitsSprites = new List<Sprite>();
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
            

            for (int i = 0; i< 8; i++)
            {
                DigitsSprites.Add(new Sprite(Digits));
                DigitsSprites[i].Scale = new Vertex(40 * GameLogic._GlobalScale, 60 * GameLogic._GlobalScale, 0);
                DigitsSprites[i].Translation = new Vertex((Runner.Width - 40 * GameLogic._GlobalScale*(i+1)), 0, 0);
                DrawnSceneObject t1 = new DrawnSceneObject("number"+i, DigitsSprites[i]);
                dsoDigits.Add(new DrawnSceneObject("number" + i, DigitsSprites[i]));
                _CScene.Data["number"+i] = dsoDigits[i];
                _CScene.AddSceneObject(dsoDigits[i]);
            }

        }

        public int Score { get => score; set => score = value; }

        public void updateHighscore()
        {
            int currentScore = score;
            currentScore += CameraMove.moveRatio - 1;
            score = currentScore;

            
           
            int left =score;
            int rev = 0;
            while (left > 0)
            {
                int r = left % 10;
                rev = rev * 10 + r;
                left = left / 10;  //left = Math.floor(left / 10); 
            }
            string tmpScore = "" + rev;
           
            for (int i = 0; i < tmpScore.Length; i++)
            {
                ((Sprite)(dsoDigits[i].Representation)).UpdateSpriteSet(Convert.ToInt32(tmpScore.Substring(i,1)));
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

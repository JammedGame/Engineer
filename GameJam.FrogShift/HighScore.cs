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

            
           
            int left =Convert.ToInt32( score/100);
            int rev = 0;
            string tmpScore ="";
            while (left > 0)
            {
                int r = left % 10;
                rev = rev * 10 + r;
                left = left / 10;  //left = Math.floor(left / 10); 
                tmpScore += "" + r;
            }
             
           
            for (int i = 0; i < tmpScore.Length; i++)
            {
               dsoDigits[i].Visual.Active = true;
               ((Sprite)(dsoDigits[i].Visual)).UpdateSpriteSet(Convert.ToInt32(tmpScore.Substring(i,1)));
            }
            if(tmpScore.Length < dsoDigits.Count)
            {
                for (int i = tmpScore.Length; i < dsoDigits.Count; i++)
                {
                    dsoDigits[i].Visual.Active = false;
                }
            }
        }

       

        public static void ChangeDrawnSceneObjectImage(DrawnSceneObject Object, Bitmap Image)
        {
            SpriteSet Set = ((Sprite)(Object.Visual)).SpriteSets[0];
            Set.Sprite.Clear();
            Set.Sprite.Add(Image);
        }
    }
}

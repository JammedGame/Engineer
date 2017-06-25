using Engineer.Engine;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    public class Predators
    {
        public static DrawnSceneObject Snake;
        public static void CreateSnake()
        {
            Sprite CharSprite = new Sprite();
            SpriteSet Phase1 = new SpriteSet("Phase1");
            for (int i = 1; i < 4; i++) for (int j = 0; j < 2; j++) Phase1.Sprite.Add(ResourceManager.Images["zmijo" + i]);
            CharSprite.SpriteSets.Add(Phase1);
            SpriteSet Phase2 = new SpriteSet("Phase2");
            for (int i = 4; i < 7; i++) for (int j = 0; j < 2; j++) Phase2.Sprite.Add(ResourceManager.Images["zmijo" + i]);
            CharSprite.SpriteSets.Add(Phase2);
            SpriteSet Phase3 = new SpriteSet("Phase3");
            for (int i = 7; i < 10; i++) for (int j = 0; j < 2; j++) Phase3.Sprite.Add(ResourceManager.Images["zmijo" + i]);
            CharSprite.SpriteSets.Add(Phase3);
            CharSprite.Translation = new Vertex();
            CharSprite.Scale = new Vertex(2000 * GameLogic._GlobalScale, 400 * GameLogic._GlobalScale, 0);
            DrawnSceneObject Char = new DrawnSceneObject("Char", CharSprite);
            Char.Data["Speed"] = 20;
            Snake = Char;
        }
    }
}

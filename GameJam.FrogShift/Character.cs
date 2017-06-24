using Engineer.Engine;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    public class Character
    {
        public static DrawnSceneObject Create()
        {
            return Character.CreateCharacter();
        }
        private static DrawnSceneObject CreateCharacter()
        {
            SpriteSet IdleSet = new SpriteSet("Idle");
            IdleSet.Sprite.Add(ResourceManager.Images["zaba1"]);
            SpriteSet JumpSet = new SpriteSet("Jump");
            for (int i = 2; i < 15; i++) JumpSet.Sprite.Add(ResourceManager.Images["zaba" + i]);
            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(IdleSet);
            CharSprite.SpriteSets.Add(JumpSet);
            CharSprite.Translation = new Vertex(200 * GameLogic._GlobalScale, 600 * GameLogic._GlobalScale, 0);
            CharSprite.Scale = new Vertex(200 * GameLogic._GlobalScale, 200 * GameLogic._GlobalScale, 0);
            DrawnSceneObject Char = new DrawnSceneObject("Char", CharSprite);
            return Char;
        }
        public static void CreateLegs()
        {
        }
    }
}

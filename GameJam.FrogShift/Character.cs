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
        public static DrawnSceneObject Create(Scene2D CScene)
        {
            return Character.CreateCharacter(CScene);
        }
        private static DrawnSceneObject CreateCharacter(Scene2D CScene)
        {
            DrawnSceneObject LL = CreateLeg("LeftLeg", "leva_noga", new Vertex(40, -88, 0));
            CScene.AddSceneObject(LL);

            SpriteSet IdleSet = new SpriteSet("Idle");
            IdleSet.Sprite.Add(ResourceManager.Images["zaba1"]);
            SpriteSet JumpSet = new SpriteSet("Jump");
            for (int i = 2; i < 15; i++) JumpSet.Sprite.Add(ResourceManager.Images["zaba" + i]);
            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(IdleSet);
            CharSprite.SpriteSets.Add(JumpSet);
            CharSprite.Translation = new Vertex(600 * GameLogic._GlobalScale, 600 * GameLogic._GlobalScale, 0);
            CharSprite.Scale = new Vertex(200 * GameLogic._GlobalScale, 200 * GameLogic._GlobalScale, 0);
            DrawnSceneObject Char = new DrawnSceneObject("Char", CharSprite);

            CScene.AddSceneObject(Char);

            DrawnSceneObject RL = CreateLeg("RightLeg", "desna_noga", new Vertex(-40, -88, 0));
            CScene.Data["Frog"] = Char;
            CScene.AddSceneObject(RL);

            Char.Data["RL"] = RL;
            Char.Data["LL"] = LL;

            return Char;
        }
        private static DrawnSceneObject CreateLeg(string Name, string ResName, Vertex Offset)
        {
            Offset = new Vertex(Offset.X * GameLogic._GlobalScale, Offset.Y * GameLogic._GlobalScale, 0);
            SpriteSet IdleSet = new SpriteSet("Idle");
            IdleSet.Sprite.Add(ResourceManager.Images[ResName + 1]);
            SpriteSet JumpSet = new SpriteSet("Jump");
            for (int i = 1; i <= 9; i++) JumpSet.Sprite.Add(ResourceManager.Images[ResName + i]);
            for (int i = 9; i >= 1; i--) JumpSet.Sprite.Add(ResourceManager.Images[ResName + i]);
            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(IdleSet);
            CharSprite.SpriteSets.Add(JumpSet);
            CharSprite.Translation = new Vertex(0, 0, 0);
            CharSprite.Scale = new Vertex(200 * GameLogic._GlobalScale, 400 * GameLogic._GlobalScale, 0);
            DrawnSceneObject Char = new DrawnSceneObject(Name, CharSprite);
            Char.Data["Offset"] = Offset;
            return Char;
        }
        public static void UpdateLegs(DrawnSceneObject Char)
        {
            Vertex FrogTranslate = ((Sprite)(Char.Representation)).Translation;
            Vertex LeftLegOffset = (Vertex)(((DrawnSceneObject)(Char.Data["LL"])).Data["Offset"]);
            if ((bool)Char.Data["underWater"])
            {
                LeftLegOffset= VertexBuilder.RotateZ(LeftLegOffset, 180);
            }
            Sprite LeftLegSprite = (Sprite)(((DrawnSceneObject)(Char.Data["LL"])).Representation);
            LeftLegSprite.Translation = new Vertex(LeftLegOffset.X + FrogTranslate.X, LeftLegOffset.Y + FrogTranslate.Y, 0);
            if ((bool)Char.Data["underWater"]) LeftLegSprite.Rotation = new Vertex(0, 0, 180);
            else LeftLegSprite.Rotation = new Vertex(0, 0, 0);
            Vertex RightLegOffset = (Vertex)(((DrawnSceneObject)(Char.Data["RL"])).Data["Offset"]);
            if ((bool)Char.Data["underWater"])
            {
                RightLegOffset = VertexBuilder.RotateZ(RightLegOffset, 180);
            }
            Sprite RightLegSprite = (Sprite)(((DrawnSceneObject)(Char.Data["RL"])).Representation);
            RightLegSprite.Translation = new Vertex(RightLegOffset.X + FrogTranslate.X, RightLegOffset.Y + FrogTranslate.Y, 0);
            if ((bool)Char.Data["underWater"]) RightLegSprite.Rotation = new Vertex(0, 0, 180);
            else RightLegSprite.Rotation = new Vertex(0, 0, 0);
        }
    }
}

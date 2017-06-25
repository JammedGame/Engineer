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
    public class Splash
    {
        public static DrawnSceneObject MakeSplash(Scene _CScene, DrawnSceneObject _Player)
        {
            DrawnSceneObject t1;
            SpriteSet splash1 = new SpriteSet("0", global::GameJam.FrogShift.Properties.Resources.splash1);
            for (int i = 2; i < 13; i++) splash1.Sprite.Add(ResourceManager.Images["splash" + i]);
            Sprite Splashes = new Sprite();
            Splashes.SpriteSets.Add(splash1);
            Splashes.Scale = new Vertex(300 * GameLogic._GlobalScale, 220 * GameLogic._GlobalScale, 0);
            Splashes.Translation = _Player.Representation.Translation;
            if(!GameLogic.Up) Splashes.Translation = new Vertex(Splashes.Translation.X - 300 * GameLogic._GlobalScale, Splashes.Translation.Y - 250 * GameLogic._GlobalScale, 0);
            else Splashes.Translation = new Vertex(Splashes.Translation.X, Splashes.Translation.Y - 30 * GameLogic._GlobalScale, 0);
            t1 = new DrawnSceneObject("Splash", Splashes);
            _CScene.Data["Splash"] = t1;
            t1.Data["Life"] = 33;
            _CScene.AddSceneObject(t1);
            return t1;
        }
    }
}
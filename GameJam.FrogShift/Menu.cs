using Engineer.Engine;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJam.FrogShift
{
    public class Menu
    {
        public Scene CreateMenuScene()
        {
            Scene2D MenuScene = new Scene2D("Menu");
            MenuScene.BackColor = Color.FromArgb(41, 216, 238);
            SceneObject Play = GameLogic.CreateStaticSprite("Play", global::GameJam.FrogShift.Properties.Resources.play, new Engineer.Mathematics.Vertex(350, 200, 0), new Engineer.Mathematics.Vertex(300, 60, 0), false);
            Play.Events.Extern.MouseClick += new GameEventHandler(this.PlayClickEvent);
            MenuScene.AddSceneObject(Play);
            SceneObject Exit = GameLogic.CreateStaticSprite("Exit", global::GameJam.FrogShift.Properties.Resources.exit, new Engineer.Mathematics.Vertex(350, 400, 0), new Engineer.Mathematics.Vertex(300, 60, 0), false);
            Exit.Events.Extern.MouseClick += new GameEventHandler(this.ExitClickEvent);
            MenuScene.AddSceneObject(Exit);
            return MenuScene;
        }
        public void PlayClickEvent(Game G, EventArguments E)
        {
            ((ExternRunner)G.Scenes[0].Data["Runner"]).Init(G, G.Scenes[1]);
        }
        public void ExitClickEvent(Game G, EventArguments E)
        {
            ((ExternRunner)G.Scenes[0].Data["Runner"]).Close();
        }
    }
}

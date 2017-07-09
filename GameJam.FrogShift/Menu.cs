using Engineer.Engine;
using Engineer.Mathematics;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJam.FrogShift
{
    public class Menu
    {
        private GameLogic Logic;
        private Scene2D PlayScene;
        private BackgroundWorker bw = new BackgroundWorker();
        public static int percentage = 0;
        private bool clicked = false;
        DrawnSceneObject Loading;


        public struct ArgBgWor
        {
            public Game Game;
            public EventArguments ea;
        }


        public Scene CreateMenuScene(Runner Run)
        {
            Scene2D MenuScene = new Scene2D("Menu");
            MenuScene.BackColor = Color.FromArgb(41, 216, 238);

            DrawnSceneObject Back = GameLogic.CreateStaticSprite("Back", global::GameJam.FrogShift.Properties.Resources.naslovna, new Vertex(0, 0, 0), new Vertex(Run.Width, Run.Height, 0), false);
            MenuScene.AddSceneObject(Back);

            GameLogic._GlobalScale = Run.Height / 1080.0f;

            SceneObject Play = GameLogic.CreateStaticSprite("Play", global::GameJam.FrogShift.Properties.Resources.pley, new Engineer.Mathematics.Vertex(200, 790, 0), new Engineer.Mathematics.Vertex(300, 120, 0), true);
            Play.Events.Extern.MouseClick += new GameEventHandler(this.PlayClickEvent);
            this.bw.WorkerSupportsCancellation = true;
            this.bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            MenuScene.AddSceneObject(Play);

            SceneObject Exit = GameLogic.CreateStaticSprite("Exit", global::GameJam.FrogShift.Properties.Resources.kvit, new Engineer.Mathematics.Vertex(1400, 790, 0), new Engineer.Mathematics.Vertex(300, 120, 0), true);
            Exit.Events.Extern.MouseClick += new GameEventHandler(this.ExitClickEvent);
            MenuScene.AddSceneObject(Exit);

            Loading = GameLogic.CreateStaticSprite("Loading", global::GameJam.FrogShift.Properties.Resources.loading, new Vertex(0, 0, 0), new Vertex(Run.Width, Run.Height, 0), false);
            Loading.Visual.Active = false;
            MenuScene.AddSceneObject(Loading);

            return MenuScene;
        }
        public void PlayClickEvent(Game G, EventArguments E)
        {
            if (!this.clicked)
            {
                this.clicked = true;
                if (bw.IsBusy != true)
                {
                    ArgBgWor Arg = new ArgBgWor();
                    Arg.Game = G;
                    Arg.ea = E;
                    Loading.Visual.Active = true;
                    bw.RunWorkerAsync(Arg);
                }
                this.clicked = false;
            }
        }
        public void ResetPlay(ArgBgWor Arg)
        {
            if (Arg.Game.Scenes.Count > 1) Arg.Game.Scenes.RemoveAt(1);
            Logic = new GameLogic();
            PlayScene = new Scene2D("Play Scene");
            PlayScene.BackColor = Color.FromArgb(0, 210, 127);
            Logic.Init(((ExternRunner)Arg.Game.Scenes[0].Data["Runner"]), Arg.Game, PlayScene);
            Arg.Game.Scenes.Add(PlayScene);
        }
        public void ExitClickEvent(Game G, EventArguments E)
        {
            ((ExternRunner)G.Scenes[0].Data["Runner"]).Close();
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            this.ResetPlay((ArgBgWor)e.Argument);
            ((ExternRunner)(((Scene)((Game)((ArgBgWor)e.Argument).Game).Scenes[0]).Data["Runner"])).Init((Game)((ArgBgWor)e.Argument).Game, (Scene)((Game)((ArgBgWor)e.Argument).Game).Scenes[1], bw);
            Loading.Visual.Active = false;
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                //this.tbProgress.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                //this.tbProgress.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                //this.tbProgress.Text = "Done!";
            }
        }
    }
}

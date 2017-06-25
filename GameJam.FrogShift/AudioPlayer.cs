using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Media;
using System.Windows.Media;

namespace GameJam.FrogShift
{
    public class AudioPlayer
    {
        public static MediaPlayer Looped;
        public static MediaPlayer Music;
        public static MediaPlayer Kre;
        public static void Init()
        {
            Music = new System.Windows.Media.MediaPlayer();
            Music.Open(new Uri(@"Data\yeah.wav", UriKind.Relative));
            Kre = new System.Windows.Media.MediaPlayer();
            Kre.Open(new Uri(@"Data\bamboo.wav", UriKind.Relative));
        }
        public static void PlaySound(MediaPlayer Sound, bool Loop, int Volume)
        {
            Sound.Stop();
            Sound.Volume = Volume / 100.0f;
            if(Loop)
            {
                Looped = Sound;
                Sound.MediaEnded += new EventHandler(Ended);
            }
            Sound.Play();
        }
        private static void Ended(object sender, EventArgs e)
        {
            Looped.Play();
        }
    }
}

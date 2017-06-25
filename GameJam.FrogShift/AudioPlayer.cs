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
        public static MediaPlayer Splash;
        public static void Init()
        {
            Music = new System.Windows.Media.MediaPlayer();
            Music.Open(new Uri(@"Data\theme.wav", UriKind.Relative));
            Kre = new System.Windows.Media.MediaPlayer();
            Kre.Open(new Uri(@"Data\kre.wav", UriKind.Relative));
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
            Looped.Stop();
            Looped.Play();
        }
        public static void PlaySplash()
        {
            Splash = new System.Windows.Media.MediaPlayer();
            Splash.Volume = 1.0f;
            Splash.Open(new Uri(@"Data\splash.wav", UriKind.Relative));
            Splash.Play();
        }
    }
}

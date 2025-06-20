using System;
using System.Windows.Media;

namespace The_Fallen_Snow_WPF.classes
{
    public class Sound
    {
        private MediaPlayer player;
        private string src;
        private Boolean isLoop;

        public Sound(string src, Boolean isLoop)
        {
            this.src = src;
            this.isLoop = isLoop;

            player = new MediaPlayer();
            player.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, src)));

            if (this.isLoop)
            {
                player.MediaEnded += (s, e) => {
                    player.Position = TimeSpan.Zero;
                    player.Play();
                };
            }
        }

        public void Play()
        {
            player.Play();
        }

        public void Stop()
        {
            player.Stop();
        }
    }
}
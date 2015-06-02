using System.Collections.Generic;
using System.Media;
using System.Threading.Tasks;
using Pasamonte.Speech.Core;
using sl = SpeechLib;

namespace Pasamonte.Speech.SAPI
{
    public class Speech : ISpeech
    {
        //
        IDictionary<string, RegisteredMedia> MediaCache;
        // voice
        sl.SpVoice Voice { get; set; }
        // cache
        IDictionary<string, string> FileRegister;

        public Speech()
        {
            Voice = new sl.SpVoice();
            FileRegister = new Dictionary<string, string>();
            MediaCache = new Dictionary<string, RegisteredMedia>();
        }

        public async Task SpeakAsync(string texto)
        {
            if (!Enabled)
                return;
            if (FileRegister.ContainsKey(texto))
            {
                var media = default(RegisteredMedia);
                if (MediaCache.ContainsKey(texto))
                {
                    media = MediaCache[texto];
                }
                else
                {
                    media = new RegisteredMedia(FileRegister[texto]);
                    MediaCache[texto] = media;
                }
                media.Play();
            }
            else
                try
                {
                    await Task.Run(() => Voice.Speak(texto));
                }
                catch
                {

                }
        }

        public void RegistrarTexto(string texto, string path)
        {
            if (FileRegister.ContainsKey(texto))
                return;
            FileRegister[texto] = path;
        }

        public bool Enabled { get; set; }
    }

    public class RegisteredMedia
    {
        SoundPlayer SoundPlayer;

        public RegisteredMedia(string path)
        {
            SoundPlayer = new SoundPlayer(path);
        }

        public void Play()
        {
            SoundPlayer.Play();
        }
    }
}

using IcarianEngine.Audio;

namespace Summoned
{
    public static class SoundMixers
    {
        public static AudioMixer MusicMixer;
        public static AudioMixer SoundMixer;

        public static void Init()
        { 
            SoundMixer = new AudioMixer()
            {
                Gain = 1.0f
            };
            MusicMixer = new AudioMixer()
            {
                Gain = 0.5f
            };
        }

        public static void Destroy()
        {
            SoundMixer.Dispose();
            MusicMixer.Dispose();
        }
    }
}
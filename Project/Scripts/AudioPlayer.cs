using IcarianEngine;
using IcarianEngine.Audio;
using IcarianEngine.Maths;

namespace Summoned
{
    public static class AudioPlayer
    {
        static GameObject s_audioObject;

        static AudioSource s_startMusic;
        static AudioSource s_loopMusic;

        static AudioSource[] s_souces;

        public static void Init()
        {
            s_audioObject = GameObject.Instantiate();

            s_startMusic = s_audioObject.AddComponent<AudioSource>();
            s_startMusic.AudioClip = AssetLibrary.LoadAudioClip("Audio/Youve Been Summoned - Music Intro.ogg");
            s_startMusic.AudioMixer = SoundMixers.MusicMixer;
            s_startMusic.Play();

            s_loopMusic = s_audioObject.AddComponent<AudioSource>();
            s_loopMusic.AudioClip = AssetLibrary.LoadAudioClip("Audio/Youve Been Summoned - Music Loop.ogg");
            s_loopMusic.Loop = true;
            s_loopMusic.AudioMixer = SoundMixers.MusicMixer;

            s_souces = new AudioSource[10];
            for (int i = 0; i < s_souces.Length; ++i)
            {
                s_souces[i] = s_audioObject.AddComponent<AudioSource>();
                s_souces[i].AudioMixer = SoundMixers.SoundMixer;
            }
        }
        public static void Update()
        {
            s_audioObject.Transform.Translation = new Vector3(CameraController.Position, 0.0f, 0.0f);

            if (!s_startMusic.IsPlaying && !s_loopMusic.IsPlaying)
            {
                s_loopMusic.Play();
            }
        }

        public static void PlayClip(string a_souce)
        {
            for (int i = 0; i < s_souces.Length; ++i)
            {
                if (!s_souces[i].IsPlaying)
                {
                    s_souces[i].AudioClip = AssetLibrary.LoadAudioClip(a_souce);
                    s_souces[i].Play();
                }
            }
        }

        public static void Destroy()
        {
            s_audioObject.Dispose();
        }
    }
}
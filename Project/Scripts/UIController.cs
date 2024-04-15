using IcarianEngine;
using IcarianEngine.Audio;
using IcarianEngine.Maths;
using IcarianEngine.Rendering.UI;

namespace Summoned
{
    public static class UIController
    {
        static GameObject s_servedObject = null;
        static Canvas s_servedCanvas = null;

        static GameObject s_victoryObject = null;
        static Canvas s_victoryCanvas = null;

        public static void Serve()
        {
            if (s_victoryObject != null)
            {
                return;
            }

            if (s_servedObject == null)
            {
                YouveBeenSummonedAssemblyControl.Instance.Blend = true;

                s_servedCanvas = Canvas.FromFile("UI/Served.ui");
                s_servedCanvas.CapturesInput = true;

                s_servedObject = GameObject.Instantiate();
                s_servedObject.Transform.Translation = new Vector3(CameraController.Position, 0.0f, 10.0f);
                CanvasRenderer renderer = s_servedObject.AddComponent<CanvasRenderer>();
                renderer.Canvas = s_servedCanvas;

                // AudioPlayer.PlayClip("Audio/Youve Been Summoned - Lose.ogg");
                AudioSource source = s_servedObject.AddComponent<AudioSource>();
                source.AudioClip = AssetLibrary.LoadAudioClip("Audio/Youve Been Summoned - Lose.ogg");
                source.AudioMixer = SoundMixers.SoundMixer;
                source.Play();
            }
        }

        public static void Victory()
        {
            if (s_servedObject != null)
            {
                return;
            }

            if (s_victoryObject == null)
            {
                s_victoryCanvas = Canvas.FromFile("UI/Victory.ui");
                s_victoryCanvas.CapturesInput = true;

                s_victoryObject = GameObject.Instantiate();
                s_victoryObject.Transform.Translation = new Vector3(CameraController.Position, 0.0f, 10.0f);

                CanvasRenderer renderer = s_victoryObject.AddComponent<CanvasRenderer>();
                renderer.Canvas = s_victoryCanvas;

                // AudioPlayer.PlayClip("Audio/Youve Been Summoned - Win.ogg");
                AudioSource source = s_victoryObject.AddComponent<AudioSource>();
                source.AudioClip = AssetLibrary.LoadAudioClip("Audio/Youve Been Summoned - Win.ogg");
                source.AudioMixer = SoundMixers.SoundMixer;
                source.Play();
            }
        }

        static void OnNormal(Canvas a_canvas, UIElement a_element)
        {
            a_element.Color = Color.White;
        }
        static void OnHover(Canvas a_canvas, UIElement a_element)
        {
            a_element.Color = Color.Blue;
        }
        static void OnPressed(Canvas a_canvas, UIElement a_element)
        {
            a_element.Color = Color.Red;
        }
        static void OnRestart(Canvas a_canvas, UIElement a_element)
        {
            // AudioPlayer.PlayClip("Audio/Youve Been Summoned - UI.ogg");

            // Dodgy hack because I still have not updated my UI to use deletion queues
            // Need to do work on UI down the line
            ThreadPool.PushJob(() =>
            {
                SceneManager.RestartScene();

                Reset();

                PlayerController.Instance.Reset();
                
                YouveBeenSummonedAssemblyControl.Instance.Blend = false;
            });
        }
        static void OnQuit(Canvas a_canvas, UIElement a_element)
        {
            // Technically leaking everything but...... 
            // I think I can be forgiven given this line and Game Jam
            Application.Close();
        }

        static void Reset()
        {
            if (s_servedObject != null)
            {
                s_servedObject.Dispose();
                s_servedCanvas.Dispose();
            }

            s_servedObject = null;
            s_servedCanvas = null;

            if (s_victoryObject != null)
            {
                s_victoryObject.Dispose();
                s_victoryCanvas.Dispose();
            }

            s_victoryObject = null;
            s_victoryCanvas = null;
        }
    }
}
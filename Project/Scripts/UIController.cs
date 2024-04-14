using IcarianEngine;
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
                s_servedCanvas = Canvas.FromFile("UI/Served.ui");
                s_servedCanvas.CapturesInput = true;

                s_servedObject = GameObject.Instantiate();
                CanvasRenderer renderer = s_servedObject.AddComponent<CanvasRenderer>();
                renderer.Canvas = s_servedCanvas;
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
                CanvasRenderer renderer = s_victoryObject.AddComponent<CanvasRenderer>();
                renderer.Canvas = s_victoryCanvas;
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
            // Dodgy hack because I still have not updated my UI to use deletion queues
            // Need to do work on UI down the line
            ThreadPool.PushJob(() =>
            {
                SceneManager.RestartScene();

                Reset();

                PlayerController.Instance.InteractLock = false;
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
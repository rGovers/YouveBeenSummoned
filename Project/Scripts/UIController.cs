using IcarianEngine;
using IcarianEngine.Maths;
using IcarianEngine.Rendering.UI;

namespace Summoned
{
    public static class UIController
    {
        static GameObject s_servedObject = null;
        static Canvas s_servedCanvas = null;

        public static void Serve()
        {
            if (s_servedObject == null)
            {
                s_servedCanvas = Canvas.FromFile("UI/Served.ui");
                s_servedCanvas.CapturesInput = true;

                s_servedObject = GameObject.Instantiate();
                CanvasRenderer renderer = s_servedObject.AddComponent<CanvasRenderer>();
                renderer.Canvas = s_servedCanvas;
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
        }
    }
}
using IcarianEngine;
using IcarianEngine.Maths;
using IcarianEngine.Rendering.UI;
using Summoned.Definitions;
using System;

namespace Summoned
{
    public abstract class MinigameController : Scriptable, IDisposable
    {
        Canvas m_canvas;

        public Canvas Canvas
        {
            get
            {
                return m_canvas;
            }
        }

        public MinigameControllerDef MinigameControllerDef
        {
            get
            {
                return Def as MinigameControllerDef;
            }
        }

        public override void Init()
        {
            m_canvas = null;

            Minigames.AddMinigame(this);
        }

        protected abstract void InitGame();

        public void Interact()
        {
            m_canvas = new Canvas(new Vector2(1280, 720));

            InitGame();

            PlayerController.Instance.InteractLock = true;

            m_canvas.CapturesInput = true;

            CanvasRenderer renderer = AddComponent<CanvasRenderer>();
            renderer.Canvas = m_canvas;
        }

        public void Dispose()
        {
            Minigames.RemoveMinigame(this);
            
            if (m_canvas != null)
            {
                m_canvas.CapturesInput = false;
                // m_canvas.Dispose();

                Minigames.ClearLock();
                PlayerController.Instance.InteractLock = false;
            }
        }
    }
}
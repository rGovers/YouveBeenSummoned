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
            m_canvas = new Canvas(new Vector2(1280, 720));

            Minigames.AddMinigame(this);
        }

        protected abstract void InitGame();

        public void Interact()
        {
            InitGame();

            PlayerController.Instance.InteractLock = true;

            m_canvas.CapturesInput = true;

            CanvasRenderer renderer = AddComponent<CanvasRenderer>();
            renderer.Canvas = m_canvas;
        }

        public void Dispose()
        {
            PlayerController.Instance.InteractLock = false;

            Minigames.RemoveMinigame(this);
            Minigames.ClearLock();

            m_canvas.Dispose();
        }
    }
}
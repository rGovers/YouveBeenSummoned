using IcarianEngine;
using IcarianEngine.Definitions;
using IcarianEngine.Maths;
using IcarianEngine.Rendering;
using IcarianEngine.Rendering.UI;
using Summoned.Definitions;

namespace Summoned
{
    public class ClickMinigameController : MinigameController
    {
        ImageUIElement m_imageElement;
        float          m_energy;

        public ClickMinigameControllerDef ClickMinigameControllerDef
        {
            get
            {
                return Def as ClickMinigameControllerDef;
            }
        }

        void OnPressed(Canvas a_canvas, UIElement a_element)
        {
            ClickMinigameControllerDef def = ClickMinigameControllerDef;

            a_element.Size = new Vector2(120.0f);

            m_energy += def.ClickEnergy;
        }

        public override void Init()
        {
            base.Init();

            m_imageElement = null;
        }

        protected override void InitGame()
        {
            m_imageElement = new ImageUIElement();
            m_imageElement.Position = new Vector2(Random.Range(600, 800), Random.Range(300, 400));
            m_imageElement.Size = new Vector2(100.0f);
            m_imageElement.Sampler = AssetLibrary.GetSampler(new TextureInput()
            {
                Slot = 0,
                Path = "Textures/UI/ClickTemp.png",
                AddressMode = TextureAddress.Repeat,
                FilterMode = TextureFilter.Linear
            });
            m_imageElement.OnPressed += OnPressed;

            Canvas.AddChild(m_imageElement);
        }

        public override void Update()
        {
            ClickMinigameControllerDef def = ClickMinigameControllerDef;

            if (m_imageElement != null)
            {
                m_imageElement.Size = Vector2.Lerp(m_imageElement.Size, new Vector2(100.0f), Time.DeltaTime);
            }

            if (m_energy >= def.RequiredClickEnergy)
            {
                GameObject.Dispose();
            }

            m_energy = Mathf.Max(m_energy - Time.DeltaTime, 0.0f);
        }
    }
}
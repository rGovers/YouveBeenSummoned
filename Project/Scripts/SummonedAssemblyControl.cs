using IcarianEngine;
using IcarianEngine.Definitions;
using IcarianEngine.Maths;
using IcarianEngine.Mod;
using IcarianEngine.Rendering;
using IcarianEngine.Rendering.Lighting;
using Summoned.Definitions;

namespace Summoned
{
    public class YouveBeenSummonedAssemblyControl : AssemblyControl
    {
        public static YouveBeenSummonedAssemblyControl Instance;

        CellRenderPipeline m_pipeline;

        bool               m_fullscreen;
        Model              m_sphereModel;

        bool               m_blend = false;

        public bool Blend
        {
            get
            {
                return m_blend;
            }
            set
            {
                m_blend = value;
            }
        }

        public override void Init()
        {
            Instance = this;

            m_fullscreen = true;
            if (!Application.IsHeadless)
            {
                Monitor[] monitors = Application.GetMonitors();

                Application.SetFullscreen(monitors[0], true, monitors[0].Width, monitors[0].Height);
            }

            m_pipeline = new CellRenderPipeline();
            RenderPipeline.SetPipeline(m_pipeline);

            AudioPlayer.Init();
            SoundMixers.Init();
            CameraController.Init();
            Minigames.Init();

            m_sphereModel = PrimitiveGenerator.CreateIcoSphere(2);

            GameObject lightObject = GameObject.Instantiate();
            lightObject.Transform.Rotation = Quaternion.FromAxisAngle(Vector3.Normalized(new Vector3(1.0f, 0.0f, 0.3f)), 1.3f);
            AmbientLight ambLight = lightObject.AddComponent<AmbientLight>();
            ambLight.Color = Color.White;
            ambLight.Intensity = 0.75f;
            DirectionalLight dirLight = lightObject.AddComponent<DirectionalLight>();
            dirLight.Color = Color.White;
            dirLight.Intensity = 1.0f;

            GameObject player = GameObject.Instantiate();
            MeshRenderer renderer = player.AddComponent<MeshRenderer>();
            renderer.Material = AssetLibrary.GetMaterial(MaterialDefTable.WhiteMaterial);
            renderer.Model = m_sphereModel;
            player.AddComponent(DefLibrary.GetDef<PlayerControllerDef>("PlayerController"));

            SceneManager.LoadScene("Market.iscene");
        }

        public override void Update()
        {
            if (Input.AltModifier)
            {
                if (!Application.IsHeadless && Input.IsKeyReleased(KeyCode.Enter))
                {
                    Monitor[] monitors = Application.GetMonitors();

                    m_fullscreen = !m_fullscreen;

                    if (m_fullscreen)
                    {
                        Application.SetFullscreen(monitors[0], true, monitors[0].Width, monitors[0].Height);
                    }
                    else
                    {
                        Application.SetFullscreen(monitors[0], false, 1280, 720);
                    }
                }
            }

            Minigames.Update();

            AudioPlayer.Update();

            if (m_blend)
            {
                m_pipeline.BlendFactor = Mathf.Min(m_pipeline.BlendFactor + Time.DeltaTime, 1.0f);
            }
            else
            {
                m_pipeline.BlendFactor = Mathf.Max(m_pipeline.BlendFactor - Time.DeltaTime, 0.0f);
            }
        }
        public override void FixedUpdate()
        {
            // Assembly FixedUpdate
        }

        public override void Close()
        {
            m_sphereModel.Dispose();

            AudioPlayer.Destroy();
            SceneManager.Destroy();
            SoundMixers.Destroy();
        }
    }
}

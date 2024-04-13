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
        Model m_sphereModel;

        Scene m_scene;

        public override void Init()
        {
            RenderPipeline.SetPipeline(new CellRenderPipeline());

            CameraController.Init();
            Minigames.Init();

            m_sphereModel = PrimitiveGenerator.CreateIcoSphere(2);

            GameObject lightObject = GameObject.Instantiate();
            lightObject.Transform.Rotation = Quaternion.FromAxisAngle(Vector3.Normalized(new Vector3(1.0f, 0.0f, 0.3f)), 1.2f);
            AmbientLight ambLight = lightObject.AddComponent<AmbientLight>();
            ambLight.Color = Color.White;
            ambLight.Intensity = 0.1f;
            DirectionalLight dirLight = lightObject.AddComponent<DirectionalLight>();
            dirLight.Color = Color.White;
            dirLight.Intensity = 1.5f;

            GameObject player = GameObject.Instantiate();
            MeshRenderer renderer = player.AddComponent<MeshRenderer>();
            renderer.Material = AssetLibrary.GetMaterial(MaterialDefTable.WhiteMaterial);
            renderer.Model = m_sphereModel;
            player.AddComponent(DefLibrary.GetDef<PlayerControllerDef>("PlayerController"));

            m_scene = Scene.LoadScene("Market.iscene");
            m_scene.GenerateScene(Matrix4.Identity);
        }

        public override void Update()
        {
            Minigames.Update();
        }
        public override void FixedUpdate()
        {
            // Assembly FixedUpdate
        }

        public override void Close()
        {
            m_sphereModel.Dispose();

            m_scene.Dispose();
        }
    }
}

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
        Model      m_sphereModel;
        Model      m_boxModel;

        GameObject m_player;

        public override void Init()
        {
            RenderPipeline.SetPipeline(new CellRenderPipeline());

            CameraController.Init();

            m_sphereModel = PrimitiveGenerator.CreateIcoSphere(2);
            m_boxModel = PrimitiveGenerator.CreateCube();

            GameObject lightObject = GameObject.Instantiate();
            lightObject.Transform.Rotation = Quaternion.FromAxisAngle(Vector3.Normalized(new Vector3(1.0f, 0.0f, 0.3f)), 1.2f);
            AmbientLight ambLight = lightObject.AddComponent<AmbientLight>();
            ambLight.Color = Color.White;
            ambLight.Intensity = 0.1f;
            DirectionalLight dirLight = lightObject.AddComponent<DirectionalLight>();
            dirLight.Color = Color.White;
            dirLight.Intensity = 1.5f;

            GameObject testCube = GameObject.Instantiate();
            testCube.Transform.Translation = new Vector3(0.0f, 0.0f, -2.0f);
            MeshRenderer cubeMeshRenderer = testCube.AddComponent<MeshRenderer>();
            cubeMeshRenderer.Material = AssetLibrary.GetMaterial(MaterialDefTable.WhiteMaterial);
            cubeMeshRenderer.Model = m_boxModel;

            m_player = GameObject.Instantiate();
            MeshRenderer renderer = m_player.AddComponent<MeshRenderer>();
            renderer.Material = AssetLibrary.GetMaterial(MaterialDefTable.WhiteMaterial);
            renderer.Model = m_sphereModel;
            m_player.AddComponent(DefLibrary.GetDef<PlayerControllerDef>("PlayerController"));
        }

        public override void Update()
        {
            // Assembly Update
        }
        public override void FixedUpdate()
        {
            // Assembly FixedUpdate
        }

        public override void Close()
        {
            m_boxModel.Dispose();
            m_sphereModel.Dispose();
        }
    }
}

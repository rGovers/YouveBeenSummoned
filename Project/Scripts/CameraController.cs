using IcarianEngine;
using IcarianEngine.Maths;
using IcarianEngine.Rendering;

namespace Summoned
{
    public static class CameraController
    {
        static GameObject s_cameraObj;

        public static void Init()
        {
            s_cameraObj = GameObject.Instantiate();

            SetPosition(0.0f);

            Camera cam = s_cameraObj.AddComponent<Camera>();
        }

        public static void SetPosition(float a_pos)
        {
            s_cameraObj.Transform.Translation = new Vector3(a_pos, -2.0f, 10.0f);
        }
    }
}
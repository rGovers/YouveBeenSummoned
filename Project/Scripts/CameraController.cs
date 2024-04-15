using IcarianEngine;
using IcarianEngine.Audio;
using IcarianEngine.Maths;
using IcarianEngine.Rendering;

namespace Summoned
{
    public static class CameraController
    {
        static GameObject s_cameraObj;

        public static float Position
        {
            get
            {
                return s_cameraObj.Transform.Translation.X;
            }
            set
            {
                s_cameraObj.Transform.Translation = new Vector3(value, -2.0f, 10.0f);
            }
        }

        public static void Init()
        {
            s_cameraObj = GameObject.Instantiate();

            Position = 0.0f;

            s_cameraObj.AddComponent<Camera>();
            s_cameraObj.AddComponent<AudioListener>();

            
        }
    }
}
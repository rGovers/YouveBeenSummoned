using IcarianEngine;
using IcarianEngine.Maths;

namespace Summoned
{
    public static class SceneManager
    {
        static Scene s_scene = null;

        public static void LoadScene(string a_scenePath)
        {
            if (s_scene != null)
            {
                s_scene.Dispose();
            }

            s_scene = Scene.LoadScene(a_scenePath);
            s_scene.GenerateScene(Matrix4.Identity);
        }
        public static void RestartScene()
        {
            if (s_scene != null)
            {
                s_scene.FlushScene();
                s_scene.GenerateScene(Matrix4.Identity);
            }
        }

        public static void Destroy()
        {
            if (s_scene != null)
            {
                s_scene.Dispose();
                s_scene = null;
            }
        }
    }
}
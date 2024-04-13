using IcarianEngine.Definitions;

namespace Summoned.Definitions
{
    public class MinigameControllerDef : ComponentDef
    {
        public float InteractRadius = 2.0f;

        public MinigameControllerDef()
        {
            ComponentType = typeof(MinigameController);
        }
    }
}
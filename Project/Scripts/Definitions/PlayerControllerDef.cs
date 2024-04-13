using IcarianEngine.Definitions;

namespace Summoned.Definitions
{
    public class PlayerControllerDef : ComponentDef
    {
        public float MoveSpeed = 10.0f;

        public PlayerControllerDef()
        {
            ComponentType = typeof(PlayerController);
        }
    }
}
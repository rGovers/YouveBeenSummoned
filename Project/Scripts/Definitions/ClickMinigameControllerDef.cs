namespace Summoned.Definitions
{
    public class ClickMinigameControllerDef : MinigameControllerDef
    {
        public float ClickEnergy = 1.0f;
        public float RequiredClickEnergy = 10.0f;

        public ClickMinigameControllerDef()
        {
            ComponentType = typeof(ClickMinigameController);
        }
    }
}
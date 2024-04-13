using IcarianEngine.Definitions;

namespace Summoned.Definitions
{
    public class EnemyControllerDef : ComponentDef
    {
        public struct TimePeriod
        {
            public float MinTime;
            public float MaxTime;
        };

        public float MoveSpeed = 2.0f;
        public float ServeRadius = 2.0f;

        public TimePeriod WaitPeriod = new TimePeriod() { MinTime = 0.5f, MaxTime = 1.5f };
        public TimePeriod WalkPeriod = new TimePeriod() { MinTime = 2.0f, MaxTime = 5.0f };

        public EnemyControllerDef()
        {
            ComponentType = typeof(EnemyController);
        }
    }
}
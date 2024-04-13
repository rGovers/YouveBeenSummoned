using IcarianEngine;

namespace Summoned
{
    public class PlayerController : Scriptable
    {
        public override void Init()
        {
            Logger.Message("started");
        }

        public override void Update()
        {
            Logger.Message("?");

            if (Input.IsKeyDown(KeyCode.A))
            {
                Logger.Message("left");
            }
            if (Input.IsKeyDown(KeyCode.D))
            {
                Logger.Message("right");
            }
        }
    }
}
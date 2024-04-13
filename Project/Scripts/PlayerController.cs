using IcarianEngine;
using IcarianEngine.Maths;
using Summoned.Definitions;

namespace Summoned
{
    public class PlayerController : Scriptable
    {
        public static PlayerController Instance = null;

        public PlayerControllerDef PlayerControllerDef
        {
            get
            {
                return Def as PlayerControllerDef;
            }
        }

        public void Serve()
        {
            Logger.Message("Served");
        }

        public override void Init()
        {
            Instance = this;
        }

        public override void Update()
        {
            PlayerControllerDef def = PlayerControllerDef;

            float mov = 0.0f;

            if (Input.IsKeyDown(KeyCode.A))
            {
                mov -= 1.0f;
            }
            if (Input.IsKeyDown(KeyCode.D))
            {
                mov += 1.0f;
            }

            float xPos = Transform.Translation.X + mov * def.MoveSpeed * Time.DeltaTime;

            CameraController.SetPosition(xPos);

            Transform.Translation = new Vector3(xPos, 0.0f, 0.0f);
        }
    }
}
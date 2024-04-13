using IcarianEngine;
using IcarianEngine.Maths;
using Summoned.Definitions;

namespace Summoned
{
    public class PlayerController : Scriptable
    {
        public static PlayerController Instance = null;

        bool m_lock;
        bool m_interactLock;

        public PlayerControllerDef PlayerControllerDef
        {
            get
            {
                return Def as PlayerControllerDef;
            }
        }

        public bool InteractLock
        {
            get
            {
                return m_interactLock;
            }
            set
            {
                m_interactLock = value;
            }
        }

        public override void Init()
        {
            Instance = this;

            Reset();
        }

        public void Reset()
        {
            m_lock = false;
            m_interactLock = false;

            Transform.Translation = Vector3.Zero;
        }

        public void Serve()
        {
            Minigames.Clear();
            UIController.Serve();

            m_lock = true;
        }

        public override void Update()
        {
            if (m_lock || m_interactLock)
            {
                return;
            }

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
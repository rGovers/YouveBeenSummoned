using IcarianEngine;
using IcarianEngine.Maths;
using Summoned.Definitions;

namespace Summoned
{
    public class PlayerController : Scriptable
    {
        public static PlayerController Instance = null;

        bool  m_lock;
        bool  m_interactLock;

        float m_targetDirection;

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

            m_targetDirection = 0.0f;

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

            m_targetDirection = Mathf.Lerp(m_targetDirection, Mathf.HalfPI * mov, Time.DeltaTime * 5);

            float xPos = Transform.Translation.X + mov * def.MoveSpeed * Time.DeltaTime;
            Transform.Rotation = Quaternion.FromAxisAngle(Vector3.UnitX, Mathf.Sin(Time.TimePassed * 7.5f) * mov * 0.1f) * Quaternion.FromAxisAngle(Vector3.UnitX, -Mathf.HalfPI) * Quaternion.FromAxisAngle(Vector3.UnitZ, m_targetDirection);

            CameraController.Position = xPos;

            Transform.Translation = new Vector3(Mathf.Min(xPos, 55.0f), 1.0f - Mathf.Abs(Mathf.Sin(Time.TimePassed * 7.5f) * mov * 0.1f), 0.0f);
        }   
    }
}
using IcarianEngine;
using IcarianEngine.Maths;
using Summoned.Definitions;

namespace Summoned
{
    public class EnemyController : Scriptable
    {
        bool m_lock;

        float m_waitTime;

        public EnemyControllerDef EnemyControllerDef
        {
            get
            {
                return Def as EnemyControllerDef;
            }
        }

        public override void Init()
        {
            m_lock = false;

            m_waitTime = 2.0f;
        }

        public override void FixedUpdate()
        {
            if (m_lock)
            {
                return;
            }

            if (m_waitTime > 0.0f)
            {
                m_waitTime -= Time.DeltaTime;

                return;
            }

            EnemyControllerDef def = EnemyControllerDef;

            float xPos = Transform.Translation.X + def.MoveSpeed * Time.FixedDeltaTime;

            Transform.Translation = new Vector3(xPos, -0.2f - Mathf.Abs(Mathf.Sin(Time.FixedTimePassed * def.BobSpeed)) * 0.5f, 0.0f);
            // Transform.Rotation = Quaternion.FromAxisAngle(Vector3.UnitX, Mathf.Sin(Time.FixedTimePassed * def.BobSpeed) * 0.1f) * Quaternion.FromAxisAngle(Vector3.UnitY, Mathf.HalfPI);
            // Transform.Translation = new Vector3(-4.0f, -0.2f, 0.0f);
            Transform.Rotation = Quaternion.FromAxisAngle(Vector3.UnitX, Mathf.Sin(Time.FixedTimePassed * def.BobSpeed) * 0.1f) * Quaternion.FromAxisAngle(Vector3.UnitX, -Mathf.HalfPI) * Quaternion.FromAxisAngle(Vector3.UnitZ, Mathf.HalfPI);

            if (Mathf.Abs(xPos - PlayerController.Instance.Transform.Translation.X) < def.ServeRadius)
            {
                PlayerController.Instance.Serve();

                m_lock = true;
            }
        }
    }   
}
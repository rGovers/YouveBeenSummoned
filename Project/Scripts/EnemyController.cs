using IcarianEngine;
using IcarianEngine.Maths;
using Summoned.Definitions;

namespace Summoned
{
    public class EnemyController : Scriptable
    {
        public EnemyControllerDef EnemyControllerDef
        {
            get
            {
                return Def as EnemyControllerDef;
            }
        }

        public override void Init()
        {

        }

        public override void FixedUpdate()
        {
            EnemyControllerDef def = EnemyControllerDef;

            float xPos = Transform.Translation.X + def.MoveSpeed * Time.FixedDeltaTime;

            Transform.Translation = new Vector3(xPos, 0.0f, 0.0f);

            if (Mathf.Abs(xPos - PlayerController.Instance.Transform.Translation.X) < def.ServeRadius)
            {
                PlayerController.Instance.Serve();
            }
        }
    }   
}
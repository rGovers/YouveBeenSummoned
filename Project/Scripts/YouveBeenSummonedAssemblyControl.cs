using IcarianEngine;
using IcarianEngine.Mod;

namespace YouveBeenSummoned
{
    public class YouveBeenSummonedAssemblyControl : AssemblyControl
    {
        public override void Init()
        {
            // Assembly Initialization
            Logger.Message("Hello World from Icarian Engine");
        }

        public override void Update()
        {
            // Assembly Update
        }
        public override void FixedUpdate()
        {
            // Assembly FixedUpdate
        }

        public override void Close()
        {
            // Assembly Shutdown
            Logger.Message("Icarian Engine says goodbye");
        }
    }
}

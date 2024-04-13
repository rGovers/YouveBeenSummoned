using System.Collections.Generic;
using IcarianEngine;
using IcarianEngine.Maths;
using Summoned.Definitions;

namespace Summoned
{
    public static class Minigames
    {
        static List<MinigameController> s_minigames;

        static bool s_lock;

        public static void Init()
        {
            s_minigames = new List<MinigameController>();
            s_lock = false;
        }
        public static void Clear()
        {
            foreach (MinigameController minigame in s_minigames)
            {
                minigame.GameObject.Dispose();
            }

            s_minigames.Clear();
        }

        public static void ClearLock()
        {
            s_lock = false;
        }

        public static void AddMinigame(MinigameController a_minigame)
        {
            s_minigames.Add(a_minigame);
        }
        public static void RemoveMinigame(MinigameController a_minigame)
        {
            s_minigames.Remove(a_minigame);
        }

        public static void Update()
        {
            if (s_lock)
            {
                return;
            }

            if (s_minigames.Count <= 0)
            {
                Logger.Message("You Win!");
            }

            if (Input.IsKeyReleased(KeyCode.E))
            {
                PlayerController playerController = PlayerController.Instance;
                float xPlayerPox = playerController.Transform.Translation.X;

                foreach (MinigameController minigame in s_minigames)
                {
                    MinigameControllerDef def = minigame.MinigameControllerDef;

                    if (Mathf.Abs(minigame.Transform.Translation.X - xPlayerPox) <= def.InteractRadius)
                    {
                        s_lock = true;

                        minigame.Interact();

                        break;
                    }
                }
            }
        }
    }
}
using MelonLoader;
using UnityEngine;

namespace Raicuparta.UnityScaleAdjuster
{
    public class UnityScaleAdjuster : MelonMod
    {
        const string CameraScaleArg = "cameraScale";
        const float ScaleUpMultiplier = 0.9f;
        const float ScaleDownMultiplier = 1.1f;

        public override void OnLevelWasLoaded(int level)
        {
            base.OnLevelWasLoaded(level);
            SetScaleToUser();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SetScaleToUser();
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                MultiplyCameraScale(ScaleUpMultiplier);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                MultiplyCameraScale(ScaleDownMultiplier);
            }
        }

        private void MultiplyCameraScale(float scale)
        {
            Camera.main.transform.localScale *= scale;
            MelonLogger.Log($"Change camera scale to {Camera.main.transform.localScale.x}");
        }

        private float GetUserScale()
        {
            return GetCommandLineArgument(CameraScaleArg);
        }

        private void SetScaleToUser()
        {
            Camera.main.transform.localScale = Vector3.one * GetUserScale();
        }

        private float GetCommandLineArgument(string name)
        {
            string[] args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                Debug.Log("ARG " + i + ": " + args[i]);
                if (args[i] == $"-{name}")
                {
                    return float.Parse(args[i + 1]);
                }
            }

            return 1;
        }
    }
}

using MelonLoader;
using UnityEngine;

namespace Raicuparta.UnityScaleAdjuster
{
    public class UnityScaleAdjuster : MelonMod
    {
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                MultiplyCameraScale(0.9f);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                MultiplyCameraScale(1.1f);
            }
        }

        private void MultiplyCameraScale(float scale)
        {
            Camera.main.transform.localScale *= scale;
            MelonLogger.Log($"Change camera scale to {Camera.main.transform.localScale}");
        }
    }
}

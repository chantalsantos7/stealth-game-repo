using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Assets.Scripts
{
    public static class Extensions
    {
        public static bool IsInLayerMasks(this GameObject gameObject, int layerMasks)
        {
            return (layerMasks == (layerMasks | (1 << gameObject.layer)));
        }

        public static void SetTransparency(this UnityEngine.UI.Image image, float transparency)
        {
            if (image != null)
            {
                Color alpha = image.color;
                alpha.a = transparency;
                image.color = alpha;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

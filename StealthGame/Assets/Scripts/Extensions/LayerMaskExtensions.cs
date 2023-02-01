using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class LayerMaskExtensions
    {
        public static bool IsInLayerMasks(this GameObject gameObject, int layerMasks)
        {
            return (layerMasks == (layerMasks | (1 << gameObject.layer)));
        }
    }
}

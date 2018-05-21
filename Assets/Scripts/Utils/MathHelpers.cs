using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Minego
{
    public static class MathHelpers
    {
        public static bool DiffInRadius(Transform t1, Transform t2, float radius)
        {
            var diff = t1.position - t2.position;
            return diff.sqrMagnitude <= radius * radius;
        }
    }
}

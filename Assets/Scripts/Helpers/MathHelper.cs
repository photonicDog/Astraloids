using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class MathHelper
    {
        public static Vector2 DegreesToVector2(float degrees)
        {
            return RadiansToVector2(degrees * Mathf.Deg2Rad);
        }

        public static Vector2 RadiansToVector2(float radians)
        {
            return new Vector2(-Mathf.Sin(radians), Mathf.Cos(radians));
        }

        public static float Vector2ToRadians(Vector2 vec)
        {
            return Mathf.Acos(vec.y);
        }

        public static float Vector2ToDegrees(Vector2 vec)
        {
            return Vector2ToRadians(vec) * Mathf.Rad2Deg;
        }
    }
}

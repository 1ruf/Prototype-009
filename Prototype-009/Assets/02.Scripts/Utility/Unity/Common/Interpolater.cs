using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Utility.Unity.Common
{
    public static class Interpolater
    {
        /// <summary>
        /// float 을 lerp
        /// </summary>
        /// <param name="a">현재값</param>
        /// <param name="b">목표값</param>
        /// <param name="t">변화량</param>
        /// <returns></returns>
        public static float LerpFloat(float a, float b, float t)
        {
            return Mathf.Lerp(a, b, 1f - Mathf.Exp(-t * Time.deltaTime));
        }
    }
}

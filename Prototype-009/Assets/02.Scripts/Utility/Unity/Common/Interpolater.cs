using UnityEngine;

namespace Utility.Unity.Common
{
    public static class Interpolater
    {
        private const float _epsilon = 0.001f;

        /// <summary>
        /// float 을 Lerp (t = 목표 도달 시간)
        /// </summary>
        /// <param name="a">현재값</param>
        /// <param name="b">목표값</param>
        /// <param name="t">목표 도달 시간(초)</param>
        /// <returns></returns>
        public static float Lerp(float a, float b, float t)
        {
            if (Mathf.Abs(b - a) < _epsilon) return b;
            return Mathf.Lerp(a, b, Time.deltaTime / t);
        }

        /// <summary>
        /// Vector3 를 Lerp (t = 목표 도달 시간)
        /// </summary>
        /// <param name="v1">현재값</param>
        /// <param name="v2">목표값</param>
        /// <param name="t">목표 도달 시간(초)</param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 v1, Vector3 v2, float t)
        {
            if ((v2 - v1).sqrMagnitude < _epsilon * _epsilon) return v2;
            return Vector3.Lerp(v1, v2, Time.deltaTime / t);
        }

        /// <summary>
        /// Quaternion을 Slerp (t = 목표 도달 시간)
        /// </summary>
        /// <param name="q1">현재값</param>
        /// <param name="q2">목표값</param>
        /// <param name="t">목표 도달 시간(초)</param>
        /// <returns></returns>
        public static Quaternion Lerp(Quaternion q1, Quaternion q2, float t)
        {
            if (Quaternion.Angle(q1, q2) < _epsilon) return q2;
            return Quaternion.Slerp(q1, q2, Time.deltaTime / t);
        }

        /// <summary>
        /// Color을 Lerp (t = 목표 도달 시간)
        /// </summary>
        /// <param name="c1">현재값</param>
        /// <param name="c2">목표값</param>
        /// <param name="t">목표 도달 시간(초)</param>
        /// <returns></returns>
        public static Color Lerp(Color c1, Color c2, float t)
        {
            float r = Lerp(c1.r, c2.r, t);
            float g = Lerp(c1.g, c2.g, t);
            float b = Lerp(c1.b, c2.b, t);
            float a = Lerp(c1.a, c2.a, t);

            return new Color(r, g, b, a);
        }
    }
}

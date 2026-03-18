using UnityEngine;

namespace Utils
{
    public class GizmosUtils
    {
        public static void DrawCircle(Vector3 center, float radius, int subdivisions = 32)
        {
            if (subdivisions < 3) subdivisions = 3;

            float angleStep = 360f / subdivisions;

            Vector3 prevPoint = Vector3.zero;
            Vector3 firstPoint = Vector3.zero;
            
            for (int i = 0; i <= subdivisions; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;

                Vector3 point = center + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;

                if (i > 0)
                    Gizmos.DrawLine(prevPoint, point);
                else
                    firstPoint = point;

                prevPoint = point;
            } 
            
            Gizmos.DrawLine(prevPoint, firstPoint);
        }
    }
}
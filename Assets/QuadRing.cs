using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadRing : MonoBehaviour
{
    [Range(0.01f, 2)]
    [SerializeField] float radiusInner;

    [Range(0.01f, 2)]
    [SerializeField] float thickness;

    float RadiusOuter => radiusInner + thickness; 

    [Range(3,256)]
    [SerializeField] int angularSegments = 3;

    private void OnDrawGizmosSelected()
    {

        DrawWireCircle(transform.position, transform.rotation, 1);

        /*
        Gizmos.DrawWireSphere( transform.position, radiusInner);
        Gizmos.DrawWireSphere(transform.position, RadiusOuter);
        */
    }

    const float TAU = 6.28318530718f;

    public static void DrawWireCircle(Vector3 pos, Quaternion rot, float radius, int detail = 32)
    {
        Vector2[] points3D = new Vector2[detail];
        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angRad = t * TAU;

            Vector2 point2D = new Vector2(
                Mathf.Cos(angRad) * radius,
                Mathf.Sin(angRad) * radius
                );

            points3D[i] = pos + rot * point2D;
        }

        // Draw all point as dots
        for (int i = 0; i < detail-1; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[i+1]);
        }

        Gizmos.DrawLine(points3D[detail-1], points3D[0]);
    }
}

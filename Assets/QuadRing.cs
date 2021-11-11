using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class QuadRing : MonoBehaviour
{

    public enum UvProjection
    {
        AngularRadial,
        ProjectZ
    }

    [Range(0.01f, 2)]
    [SerializeField] float radiusInner;
    [Range(0.01f, 2)]
    [SerializeField] float thickness;
    [Range(3,32)]
    [SerializeField] int angularSegmentsCount = 3;

    [SerializeField] UvProjection uvProjection = UvProjection.AngularRadial;

    Mesh mesh;

    float RadiusOuter => radiusInner + thickness;
    int VertexCount => angularSegmentsCount * 2;

    private void OnDrawGizmosSelected()
    {
        /*Gizmosfs.DrawWireCircle(transform.position, transform.rotation, radiusInner, angularSegmentsCount);
        Gizmosfs.DrawWireCircle(transform.position, transform.rotation, RadiusOuter, angularSegmentsCount);*/
    }

    void Awake()
    {

        mesh = new Mesh();
        mesh.name = "QuadRing";

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    void Update() => GenerateMesh(); 

    void GenerateMesh()
    {

        mesh.Clear();

        int vCount = VertexCount;
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < angularSegmentsCount+1; i++)
        {
            float t = i / (float)angularSegmentsCount;
            float angRad = t * Mathfs.TAU;
            Vector2 direction = Mathfs.GetUnitVectorByAngle(angRad);

            /*Vector3 zOffset = Mathf.Cos(angRad * 4);*/

            vertices.Add(direction * RadiusOuter);
            vertices.Add(direction * radiusInner);
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);

            switch (uvProjection)
            {
                case UvProjection.AngularRadial:
                    // Angular/Radial coords
                    uvs.Add(new Vector2(t, 1));
                    uvs.Add(new Vector2(t, 0));
                    break;
                case UvProjection.ProjectZ:
                    // Top-down projection
                    uvs.Add(direction * 0.5f + Vector2.one * 0.5f);
                    uvs.Add(direction * (radiusInner / RadiusOuter) * 0.5f + Vector2.one * 0.5f);
                    break;
            }
            
        }

        List<int> triangleIndices = new List<int>();
        for(int i = 0; i < angularSegmentsCount; i++)
        {

            int indexRoot = i * 2;

            int indexInnerRoot = indexRoot + 1;
            int indexOuterNext = indexRoot + 2;
            int indexInnerNext = indexRoot + 3;

            triangleIndices.Add(indexOuterNext);
            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexInnerNext);

            triangleIndices.Add(indexInnerNext);
            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexInnerRoot);
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);

    }

}

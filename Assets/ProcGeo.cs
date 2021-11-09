using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGeo : MonoBehaviour
{
    private void Awake()
    {

        Mesh mesh = new Mesh();
        mesh.name = "Procedural Quad";

        List<Vector3> points = new List<Vector3>()
        {
            new Vector3(-.5f, .5f),
            new Vector3( .5f, .5f),
            new Vector3(-.5f,-.5f),
            new Vector3( .5f,-.5f) 
        };

        int[] triIndices = new int[]
        {
            2,0,1, 
            2,1,3
        };

        List<Vector2> uvs = new List<Vector2>()
        {
            new Vector2(1,1),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,0),
        };

        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };

        mesh.SetVertices(points);
        mesh.triangles = triIndices;
        mesh.SetUVs(0,uvs);
        mesh.SetNormals(normals);

        GetComponent<MeshFilter>().sharedMesh = mesh;

    }
}

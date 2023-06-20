using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    Mesh mesh;
    public Vector3[] vertices;
    public int[] triangles;
    void Awake()
    {
        // Assuming you have a reference to the mesh you want to work with
        mesh = GetComponent<MeshFilter>().mesh;

        // Get the array of vertices
        vertices = mesh.vertices;

        // Loop through each vertex
        for (int i = 0; i < vertices.Length; i++)
        {
            // Access individual vertices
            string vertexPosition = vertices[i].ToString();
            Debug.Log(vertexPosition);
            if (i == 100 || i == 101 || i == 102) {
                vertices[i] = new(vertices[i].x + 3, vertices[i].y + 3, vertices[i].z + 3);
            }
        }
        Debug.Log(vertices.Length);
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        triangles = mesh.triangles;
        // Assign the UV coordinates to the mesh
        //mesh.uv = uvCoordinates;
        mesh.RecalculateNormals();
    }
}

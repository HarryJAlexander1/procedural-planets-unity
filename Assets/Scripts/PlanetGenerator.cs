using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    public Vector3[] meshVertices;
    public int[] meshTriangles;
    public Mesh mesh;

    private void Awake()
    {
        CreateMesh();
        UpdateMesh();
    }

    // generate icosphere
    public class Icosahedron{

        float t = (float)((1f + Math.Sqrt(5f)) / 2f);
        private List<Vector3> _vertices = new();

        public Icosahedron() {
            GenerateVertices();
        }

        private void GenerateVertices() {

            Vector3 vertex = new(-1, t, 0);
            Vector3 vertex1 = new(1, t, 0);
            Vector3 vertex2 = new(-1, -t, 0);
            Vector3 vertex3 = new(1, -t, 0);

            Vector3 vertex4 = new(0, -1, t);
            Vector3 vertex5 = new(0, 1, t);
            Vector3 vertex6 = new(0, -1, -t);
            Vector3 vertex7 = new(0, 1, -t);

            Vector3 vertex8 = new(t, 0, -1);
            Vector3 vertex9 = new(t, 0, 1);
            Vector3 vertex10 = new(-t, 0, -1);
            Vector3 vertex11 = new(-t, 0, 1);

            _vertices.Add(vertex);
            _vertices.Add(vertex1);
            _vertices.Add(vertex2);
            _vertices.Add(vertex3);
            _vertices.Add(vertex4);
            _vertices.Add(vertex5);
            _vertices.Add(vertex6);
            _vertices.Add(vertex7);
            _vertices.Add(vertex8);
            _vertices.Add(vertex9);
            _vertices.Add(vertex10);
            _vertices.Add(vertex11);
        }

        public List<Vector3> Vertices
        {
            get { return _vertices; }
        }
    }
    public class Icosphere { 

    }

    // displace vertices with perlin noise

    // Create mesh
    void CreateMesh() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Icosahedron icosahedron = new Icosahedron();
        meshVertices = icosahedron.Vertices.ToArray();
        meshTriangles = new int[]
        {
            0,11,5,
            0,5,1,
            0,1,7,
            0,7,10,
            0,10,11,

            1,5,9,
            5,11,4,
            11,10,2,
            10,7,6,
            7,1,8,

            3,9,4,
            3,4,2,
            3,2,6,
            3,6,8,
            3,8,9,

            4,9,5,
            2,4,11,
            6,2,10,
            8,6,7,
            9,8,1
        };
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = meshVertices;
        mesh.triangles = meshTriangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    // map textures to terrain
}

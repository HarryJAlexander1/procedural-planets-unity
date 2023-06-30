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

    public class Triangle {

        private Vector3 _vertexA;
        private Vector3 _vertexB;
        private Vector3 _vertexC;

        public Triangle(Vector3 vertexA, Vector3 vertexB, Vector3 vertexC) {
            _vertexA = vertexA;
            _vertexB = vertexB;
            _vertexC = vertexC;
        }

        public Vector3 VertexA 
        {
            get { return _vertexA; }
        }
        public Vector3 VertexB
        {
            get { return _vertexB; }
        }
        public Vector3 VertexC
        {
            get { return _vertexC; }
        }
    }

    // generate icosphere
    public class Icosahedron{

        private float t = (float)((1f + Math.Sqrt(5f)) / 2f);
        private List<Vector3> _vertices = new();
        private List<Triangle> _triangles = new();

        public Icosahedron() {
            GenerateVertices();
            GenerateTriangles();
        }

        private void GenerateVertices() {

            // instantiate vertices
            Debug.Log("t = " + t);
            Vector3 vertex0 = new(-1, t, 0);
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

            // add vertices
            _vertices.Add(vertex0);
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

        private void GenerateTriangles() {

            // instantiate triangles
            Triangle triangle0 = new(_vertices[0], _vertices[11], _vertices[5]);
            Triangle triangle1 = new(_vertices[0], _vertices[5], _vertices[1]);
            Triangle triangle2 = new(_vertices[0], _vertices[1], _vertices[7]);
            Triangle triangle3 = new(_vertices[0], _vertices[7], _vertices[10]);
            Triangle triangle4 = new(_vertices[0], _vertices[10], _vertices[11]);

            Triangle triangle5 = new(_vertices[1], _vertices[5], _vertices[9]);
            Triangle triangle6 = new(_vertices[5], _vertices[11], _vertices[4]);
            Triangle triangle7 = new(_vertices[11], _vertices[10], _vertices[2]);
            Triangle triangle8 = new(_vertices[10], _vertices[7], _vertices[6]);
            Triangle triangle9 = new(_vertices[7], _vertices[1], _vertices[8]);

            Triangle triangle10 = new(_vertices[3], _vertices[9], _vertices[4]);
            Triangle triangle11 = new(_vertices[3], _vertices[4], _vertices[2]);
            Triangle triangle12 = new(_vertices[3], _vertices[2], _vertices[6]);
            Triangle triangle13 = new(_vertices[3], _vertices[6], _vertices[8]);
            Triangle triangle14 = new(_vertices[3], _vertices[8], _vertices[9]);

            Triangle triangle15 = new(_vertices[4], _vertices[9], _vertices[5]);
            Triangle triangle16 = new(_vertices[2], _vertices[4], _vertices[11]);
            Triangle triangle17 = new(_vertices[6], _vertices[2], _vertices[10]);
            Triangle triangle18 = new(_vertices[8], _vertices[6], _vertices[7]);
            Triangle triangle19 = new(_vertices[9], _vertices[8], _vertices[1]);

            // add triangles
            _triangles.Add(triangle0);
            _triangles.Add(triangle1);
            _triangles.Add(triangle2);
            _triangles.Add(triangle3);
            _triangles.Add(triangle4);
            _triangles.Add(triangle5);
            _triangles.Add(triangle6);
            _triangles.Add(triangle7);
            _triangles.Add(triangle8);
            _triangles.Add(triangle9);
            _triangles.Add(triangle10);
            _triangles.Add(triangle11);
            _triangles.Add(triangle12);
            _triangles.Add(triangle13);
            _triangles.Add(triangle14);
            _triangles.Add(triangle15);
            _triangles.Add(triangle16);
            _triangles.Add(triangle17);
            _triangles.Add(triangle18);
            _triangles.Add(triangle19);
        }

        public List<Vector3> Vertices
        {
            get { return _vertices; }
        }
        public List<Triangle> Triangles
        {
            get { return _triangles; }
        }
    }
    public class Icosphere {

        private List<Vector3> _vertices = new();
        private List<Triangle> _triangles = new();

        public Icosphere(Icosahedron icosahedron, int recursions)
        {
            SubdivideTriangles(icosahedron, recursions);
        }

        public List<Vector3> Vertices
        {
            get { return _vertices; }
        }

        public List<Triangle> Triangles
        {
            get { return _triangles; }
        }

        private void SubdivideTriangles(Icosahedron icosahedron, int recursions) 
        {
            // subdivide triangles in icosahedron to create icosphere
            List<Triangle> triangles = icosahedron.Triangles;
            List<Triangle> newTriangles = new();

            for (int i = 0; i < recursions; i++)
            {
                for (int j = 0; j < triangles.Count; j++)
                {
                    // compute middlepoint between each vertices in triangle
                    Vector3 a = ComputeMiddlePoint(triangles[j].VertexA, triangles[j].VertexB);
                    Vector3 b = ComputeMiddlePoint(triangles[j].VertexB, triangles[j].VertexC);
                    Vector3 c = ComputeMiddlePoint(triangles[j].VertexC, triangles[j].VertexA);

                    newTriangles.Add(new(triangles[j].VertexA, a, c));
                    newTriangles.Add(new(triangles[j].VertexB, b, a));
                    newTriangles.Add(new(triangles[j].VertexC, c, b));
                    newTriangles.Add(new(a, b, c));
                }
                triangles = newTriangles;
            }
            // set object vertices as newTriangles vertices
            foreach (Triangle t in triangles) {
                _vertices.Add(t.VertexA);
                _vertices.Add(t.VertexB);
                _vertices.Add(t.VertexC);
            }
            // set object triangles as newTriangles
            _triangles = triangles;
        }

        private Vector3 ComputeMiddlePoint(Vector3 vertexA, Vector3 vertexB)
        {
           
            Vector3 midpoint = (vertexA + vertexB);

            //float length = (float)Math.Sqrt(Math.Pow(midpoint.x, 2) + Math.Pow(midpoint.y, 2) + Math.Pow(midpoint.z, 2));


            //midpoint = midpoint.normalized;
            midpoint.Normalize();
   
            return midpoint * 2;
        }
    }

    // displace vertices with perlin noise

    // Create mesh
    void CreateMesh() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        // create icosahedron
        Icosahedron icosahedron = new Icosahedron();
        // create icosphere
        Icosphere icosphere = new(icosahedron, 1);
        // assign mesh vertices
        meshVertices = icosphere.Vertices.ToArray();
        // assign mesh triangles
        List<int> meshTrianglesList = new();
        for (int i=0; i < meshVertices.Length; i++) {
            meshTrianglesList.Add(i);
        }
        meshTriangles = meshTrianglesList.ToArray();
        Debug.Log("Triangles: " + meshTriangles.Length * 0.33333);
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

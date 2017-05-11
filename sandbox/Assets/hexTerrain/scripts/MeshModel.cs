using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hexwork
{
    public class MeshModel
    {
        #region fields
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Vector2> uvs = new List<Vector2>();
        public List<Vector3> colVertices = new List<Vector3>();
        public List<int> colTriangles = new List<int>();

        public bool useRenderDataForCol;
        #endregion

        #region constructors
        public MeshModel()
        {

        }


        #endregion

        #region triangles
        // Add in the triangle's list the 6 triangles of a hex mesh
        public void AddHexFacesTriangles()
        {
            // 1
            triangles.Add(vertices.Count - 7);
            triangles.Add(vertices.Count - 6);
            triangles.Add(vertices.Count - 5);
            // 2
            triangles.Add(vertices.Count - 7);
            triangles.Add(vertices.Count - 5);
            triangles.Add(vertices.Count - 4);
            // 3
            triangles.Add(vertices.Count - 7);
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            // 4
            triangles.Add(vertices.Count - 7);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);
            // 5
            triangles.Add(vertices.Count - 7);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);
            // 6 - this one is special
            triangles.Add(vertices.Count - 7);
            triangles.Add(vertices.Count - 1);
            triangles.Add(vertices.Count - 6);

            if (useRenderDataForCol)
            {
                colTriangles.Add(colVertices.Count - 7);
                colTriangles.Add(colVertices.Count - 6);
                colTriangles.Add(colVertices.Count - 5);
                // 2
                colTriangles.Add(colVertices.Count - 7);
                colTriangles.Add(colVertices.Count - 5);
                colTriangles.Add(colVertices.Count - 4);
                // 3
                colTriangles.Add(colVertices.Count - 7);
                colTriangles.Add(colVertices.Count - 4);
                colTriangles.Add(colVertices.Count - 3);
                // 4
                colTriangles.Add(colVertices.Count - 7);
                colTriangles.Add(colVertices.Count - 3);
                colTriangles.Add(colVertices.Count - 2);
                // 5
                colTriangles.Add(colVertices.Count - 7);
                colTriangles.Add(colVertices.Count - 2);
                colTriangles.Add(colVertices.Count - 1);
                // 6 - this one is special
                colTriangles.Add(colVertices.Count - 7);
                colTriangles.Add(colVertices.Count - 1);
                colTriangles.Add(colVertices.Count - 6);
            }
        }

        public void AddQuadFacesTriangles()
        {
            // Triangle 1
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);

            // Triangle 2
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);

            if (useRenderDataForCol)
            {
                colTriangles.Add(colVertices.Count - 4);
                colTriangles.Add(colVertices.Count - 3);
                colTriangles.Add(colVertices.Count - 2);
                colTriangles.Add(colVertices.Count - 4);
                colTriangles.Add(colVertices.Count - 2);
                colTriangles.Add(colVertices.Count - 1);
            }
        }

        public void AddQuadTriangle(int tri)
        {
            triangles.Add(tri);
            if(useRenderDataForCol)
            {
                colTriangles.Add(tri - (vertices.Count - colVertices.Count));
            }
        }
        #endregion

        #region vertex
        public void AddVertex(Vector3 vertex)
        {
            vertices.Add(vertex);
            if(useRenderDataForCol)
            {
                colVertices.Add(vertex);
            }

        }
        #endregion


    }
}


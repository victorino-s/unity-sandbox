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




        }
        #endregion

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace hexwork 
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        HexBloc[,,] blocs = new HexBloc[chunkSize, chunkSize, chunkSize];
        public static int chunkSize = 16;
        public bool update = true;

        MeshFilter filter;
        MeshCollider coll;

        public World world;
        public WorldPos pos;

        //Use this for initialization
        void Start()
        {
            filter = gameObject.GetComponent<MeshFilter>();
            coll = gameObject.GetComponent<MeshCollider>();

        }
        //Update is called once per frame
        void Update()
        {
        }
        public HexBloc GetBlock(int x, int y, int z)
        {
            return blocs[x, y, z];
        }
        //Updates the chunk based on its contents
        void UpdateChunk()
        {
            MeshModel meshModel = new MeshModel();
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    for (int z = 0; z < chunkSize; z++)
                    {
                        meshModel = blocs[x, y, z].HexBlocData(this, x, y, z, meshModel);
                    }
                }
            }
            RenderMesh(meshModel);
        }
        //Sends the calculated mesh information
        //to the mesh and collision components
        void RenderMesh(MeshModel meshModel)
        {
            filter.mesh.Clear();
            filter.mesh.vertices = meshModel.vertices.ToArray();
            filter.mesh.triangles = meshModel.triangles.ToArray();

            Debug.Log("Vertices nb : " + filter.mesh.vertices.Length + " | UV count : " + meshModel.uvs.Count);
            filter.mesh.uv = meshModel.uvs.ToArray();
            filter.mesh.RecalculateNormals();

            // Collision
            coll.sharedMesh = null;
            Mesh mesh = new Mesh();
            mesh.vertices = meshModel.colVertices.ToArray();
            mesh.triangles = meshModel.colTriangles.ToArray();
            mesh.RecalculateNormals();

            coll.sharedMesh = mesh;
        }
    }
}


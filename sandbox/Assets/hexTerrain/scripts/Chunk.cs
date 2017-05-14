using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace hexwork
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {

        public static int chunkSize = 16;
        public bool update = true;

        MeshFilter filter;
        MeshCollider coll;

        public World world;
        public Vector3 pos;

        HexBloc[,,] blocs = new HexBloc[chunkSize, chunkSize, chunkSize];
        //Use this for initialization
        void Start()
        {
            filter = gameObject.GetComponent<MeshFilter>();
            coll = gameObject.GetComponent<MeshCollider>();

        }
        //Update is called once per frame
        void Update()
        {
            if (update)
            {
                update = false;
                UpdateChunk();
            }
        }
        public HexBloc GetBloc(float x, float y, float z)
        {
            if (InRange(x) && InRange(y) && InRange(z))
                return blocs[(int)x, (int)y, (int)z];
            return world.GetHexBloc(pos.x + x, pos.y + y, pos.z + z);
        }

        public void SetBloc(float x, float y, float z, HexBloc bloc)
        {
            if (InRange(x) && InRange(y) && InRange(z))
            {
                blocs[(int)x, (int)y, (int)z] = bloc;
            }
            else
            {
                world.SetBloc(pos.x + x, pos.y + y, pos.z + z, bloc);
            }
        }

        public static bool InRange(float index)
        {
            if (index < 0 || index >= chunkSize)
                return false;

            return true;
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
                        blocs[x, y, z].FillMeshData(this, x, y, z, ref meshModel);
                    }
                }
            }

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


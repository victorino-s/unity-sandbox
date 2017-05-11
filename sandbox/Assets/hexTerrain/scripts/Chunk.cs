using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hexwork 
{
    public class Chunk : MonoBehaviour
    {

        HexBloc[,,] blocs;
        public static int chunkSize = 16;
        public bool update = true;
        //Use this for initialization
        void Start()
        {
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
        }
        //Sends the calculated mesh information
        //to the mesh and collision components
        void RenderMesh()
        {
        }
    }
}


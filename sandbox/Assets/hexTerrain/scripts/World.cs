using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace hexwork
{
    public class World : MonoBehaviour
    {

        public string worldName = "world";
        public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
        public GameObject chunkPrefab;
        // Use this for initialization
        void Start()
        {
            for(int x = -2; x < 2; x++)
            {
                for (int y = -1; y < 1; y++)
                {
                    for (int z = -1; z < 1; z++)
                    {
                        CreateChunk(x * 16, y * 16, z * 16);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateChunk(int x, int y, int z)
        {
            WorldPos worldPos = new WorldPos(x, y, z);

            GameObject newChunkObject = Instantiate(chunkPrefab, new Vector3(x, y, z), Quaternion.Euler(Vector3.zero)) as GameObject;
            Chunk newChunk = newChunkObject.GetComponent<Chunk>();

            newChunk.pos = worldPos;
            newChunk.world = this;

            chunks.Add(worldPos, newChunk);
        }

        public Chunk GetChunk(int x, int y, int z)
        {
            WorldPos pos = new WorldPos();
            float multiple = Chunk.chunkSize;
            pos.x = Mathf.FloorToInt(x / multiple) * Chunk.chunkSize;
            pos.y = Mathf.FloorToInt(y / multiple) * Chunk.chunkSize;
            pos.z = Mathf.FloorToInt(z / multiple) * Chunk.chunkSize;

            Chunk containerChunk = null;
            chunks.TryGetValue(pos, out containerChunk);
            lol();
            return containerChunk;
        }
        
        public HexBloc GetHexBloc(int x, int y, int z)
        {
            throw new NotImplementedException();
        }

      
    }
}


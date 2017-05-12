﻿using System;
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

            float zOffset = (z != 0 ? z * .75f : z);
            GameObject newChunkObject = Instantiate(chunkPrefab, new Vector3(x, y, zOffset), Quaternion.Euler(Vector3.zero)) as GameObject;
            Chunk newChunk = newChunkObject.GetComponent<Chunk>();

            newChunk.pos = worldPos;
            newChunk.world = this;

            chunks.Add(worldPos, newChunk);
            // Generation de la grille de blocs
                // Mettre des floats
            for(int xi = 0; xi < 16; xi++)
            {
                for(int yi = 0; yi < 16; yi++)
                {
                    for(int zi = 0; zi < 16; zi++)
                    {
                        if(yi <= 7)
                        {
                            SetBloc(x + xi, y + yi, z + zi, new HexBlocGrass());
                        }
                        else
                        {
                            SetBloc(x + xi, y + yi, z + zi, new HexBlocAir());
                        }
                    }
                }
            }
        }

        public void DestroyChunk(int x, int y, int z)
        {
            Chunk chunk = null;
            if(chunks.TryGetValue(new WorldPos(x,y,z), out chunk))
            {
                UnityEngine.Object.Destroy(chunk.gameObject);
                chunks.Remove(new WorldPos(x, y, z));
            }
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

            return containerChunk;
        }

        public void SetBloc(int x, int y, int z, HexBloc bloc)
        {
            Chunk chunk = GetChunk(x, y, z);

            if(chunk != null)
            {
                chunk.SetBloc(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, bloc);
                chunk.update = true;
            }
        }

        public HexBloc GetHexBloc(int x, int y, int z)
        {
            Chunk containerChunk = GetChunk(x, y, z);
            if(containerChunk != null)
            {
                HexBloc bloc = containerChunk.GetBloc(
                    x - containerChunk.pos.x,
                    y - containerChunk.pos.y,
                    z - containerChunk.pos.z);

                return bloc;
            }
            else
            {
                return new HexBlocAir();
            }
        }

      
    }
}


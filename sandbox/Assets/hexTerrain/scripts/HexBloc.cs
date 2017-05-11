using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace hexwork
{
    public class HexBloc
    {
        #region constructors
        public HexBloc()
        {

        }
        #endregion

        #region blocdata
        public virtual MeshModel HexBlocData(Chunk chunk, int x, int y, int z, MeshModel meshModel)
        {
            if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.DOWN))
            {
                meshModel = FaceDataUp(chunk, x, y, z, meshModel);
            }

            if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.UP))
            {
                meshModel = FaceDataDown(chunk, x, y, z, meshModel);
            }

            if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.N))
            {
                meshModel = FaceDataNorth(chunk, x, y, z, meshModel);
            }

            if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.S))
            {
                meshModel = FaceDataSouth(chunk, x, y, z, meshModel);
            }

            if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.E))
            {
                meshModel = FaceDataEast(chunk, x, y, z, meshModel);
            }

            if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.W))
            {
                meshModel = FaceDataWest(chunk, x, y, z, meshModel);
            }

            return meshModel;
        }

        protected virtual MeshModel FaceDataDown
            (Chunk chunk, int x, int y, int z, MeshModel meshData)
        {
            return meshData;
        }

        protected virtual MeshModel FaceDataNorth
            (Chunk chunk, int x, int y, int z, MeshModel meshData)
        {
            return meshData;
        }

        protected virtual MeshModel FaceDataEast
            (Chunk chunk, int x, int y, int z, MeshModel meshData)
        {
            return meshData;
        }

        protected virtual MeshModel FaceDataSouth
            (Chunk chunk, int x, int y, int z, MeshModel meshModel)
        {
            return meshModel;
        }

        protected virtual MeshModel FaceDataWest
            (Chunk chunk, int x, int y, int z, MeshModel meshModel)
        {
            return meshModel;
        }

        /*
         * Return the asked bloc's face solidity by specifying the wanted face direction.
         * Should be extended by each new bloc designed.
         * 
         * Base bloc is a cube so its solid
         */
        public virtual bool IsSolid(Direction direction)
        {
            return true;
            /*switch(direction){
              case Direction.north:
                  return true;
              case Direction.east:
                  return true;
              case Direction.south:
                  return true;
              case Direction.west:
                  return true;
              case Direction.up:
                  return true;
              case Direction.down:
                  return true;
          }
          return false;*/
    }
        #endregion
        #region FaceData
        protected virtual MeshModel FaceDataUp
         (Chunk chunk, int x, int y, int z, MeshModel meshModel)
        {
            meshModel.vertices.Add(new Vector3(x, y + 0.5f, z)); // up face origin (center) : 0
            meshModel.vertices.Add(new Vector3(x, y + 0.5f, z + 0.5f)); // 1
            meshModel.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.25f)); // 2
            meshModel.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.25f)); // 3
            meshModel.vertices.Add(new Vector3(x, y + 0.5f, z - 0.5f)); // 4
            meshModel.vertices.Add(new Vector3(x - 0.25f, y + 0.5f, z - 0.25f)); // 5
            meshModel.vertices.Add(new Vector3(x - 0.25f, y + 0.5f, z + 0.25f)); // 6
            meshModel.AddHexFacesTriangles();
            return meshModel;
        }
        #endregion
    }
}


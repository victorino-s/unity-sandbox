using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace hexwork
{
    // Used to set texture coordinates
    public struct Tile
    {
        public int x;
        public int y;
    }

    public class HexBloc
    {
        #region fields
        const float tileSize = 0.25f;

        public const float outerRadius = .5f;

        public const float innerRadius = outerRadius * 0.866025404f;
        #endregion

        #region constructors
        public HexBloc()
        {

        }
        #endregion

        #region blocdata
        public virtual MeshModel HexBlocData(Chunk chunk, int x, int y, int z, MeshModel meshModel)
        {
             // Hexagone Alining
            float nx = (x + z * 0.5f - z / 2);
            float nz = z * .75f;

            meshModel.useRenderDataForCol = true;
            if (!chunk.GetBloc(x, y + 1, z).IsSolid(Direction.DOWN))
            {
                meshModel = FaceDataUp(chunk, nx, y, nz, meshModel);
            }

            if (!chunk.GetBloc(x, y - 1, z).IsSolid(Direction.UP))
            {
                meshModel = FaceDataDown(chunk, nx, y, nz, meshModel);
            }
            // NW
            if (!chunk.GetBloc(x - 1, y, z + 1).IsSolid(Direction.SE))
            {
                meshModel = FaceDataNorthWest(chunk, nx, y, nz, meshModel);
            }
            // NE
            if (!chunk.GetBloc(x + 1, y, z + 1).IsSolid(Direction.SW))
            {
                meshModel = FaceDataNorthEast(chunk, nx, y, nz, meshModel);
            }
            // W
            if (!chunk.GetBloc(x - 1, y, z).IsSolid(Direction.E))
            {
                meshModel = FaceDataWest(chunk, nx, y, nz, meshModel);
            }
            // E
            if (!chunk.GetBloc(x + 1, y, z).IsSolid(Direction.W))
            {
                meshModel = FaceDataEast(chunk, nx, y, nz, meshModel);
            }
            // SW
            if (!chunk.GetBloc(x - 1, y, z - 1).IsSolid(Direction.NE))
            {
                meshModel = FaceDataSouthWest(chunk, nx, y, nz, meshModel);
            }
            // SE
            if (!chunk.GetBloc(x + 1, y, z - 1).IsSolid(Direction.NW))
            {
                meshModel = FaceDataSouthEast(chunk, nx, y, nz, meshModel);
            }
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
        #region FacesData

        // Hex Faces
        protected virtual MeshModel FaceDataUp
         (Chunk chunk, float x, int y, float z, MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z)); // up face origin (center) : 0 - up
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z + 0.5f)); // 1 - up
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.25f)); // 2- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.25f)); // 3- up
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z - 0.5f)); // 4- up
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.25f)); // 5- up
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.25f)); // 6- up


            meshModel.AddHexFacesTriangles();

            meshModel.uvs.AddRange(HexFaceUVs(Direction.UP));
            return meshModel;
        }

        protected virtual MeshModel FaceDataDown
         (Chunk chunk, float x, int y, float z, MeshModel meshModel)
        {
            // Down face = mirror up face
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z)); // down face origin (center) : 0 - down
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z - 0.5f)); // 1 - down
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.25f)); // 2 - down
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.25f)); // 3 - down
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z + 0.5f)); // 4 - down
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.25f)); // 5 - down
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.25f)); // 6 - down
            meshModel.AddHexFacesTriangles();
            meshModel.uvs.AddRange(HexFaceUVs(Direction.DOWN));
            return meshModel;
        }

        // Quad Faces
        protected virtual MeshModel FaceDataNorthWest(Chunk chunk, float x, int y, float z, MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z + 0.5f)); // 4 - down
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z + 0.5f)); // 1 - up
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.25f)); // 6- up
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.25f)); // 5 - down
            meshModel.AddQuadFacesTriangles();

            meshModel.uvs.AddRange(QuadFaceUVs(Direction.NW));
            return meshModel;
        }
        protected virtual MeshModel FaceDataWest(Chunk chunk, float x, int y, float z, MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.25f)); // 5 - down
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.25f)); // 6- up
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.25f)); // 5- up
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.25f)); // 6 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.W));
            return meshModel;
        }

        protected virtual MeshModel FaceDataSouthWest(Chunk chunk, float x, int y, float z, MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.25f)); // 6 - down
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.25f)); // 5- up
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z - 0.5f)); // 4- up
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z - 0.5f)); // 1 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.SW));
            return meshModel;
        }
        protected virtual MeshModel FaceDataSouthEast(Chunk chunk, float x, int y, float z, MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z - 0.5f)); // 1 - down
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z - 0.5f)); // 4- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.25f)); // 3- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.25f)); // 2 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.SE));
            return meshModel;
        }

        protected virtual MeshModel FaceDataEast(Chunk chunk, float x, int y, float z, MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.25f)); // 2 - down
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.25f)); // 3- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.25f)); // 2- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.25f)); // 3 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.E));
            return meshModel;
        }

        protected virtual MeshModel FaceDataNorthEast(Chunk chunk, float x, int y, float z, MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.25f)); // 3 - down
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.25f)); // 2- up
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z + 0.5f)); // 1 - up
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z + 0.5f)); // 4 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.NE));
            return meshModel;
        }
        #endregion

        #region texture
        public virtual Tile TexturePosition(Direction direction)
        {
            Tile tile = new Tile();
            tile.x = 0;
            tile.y = 0;
            return tile;
        }

        public virtual Vector2[] HexFaceUVs(Direction direction)
        {
            Vector2[] UVs = new Vector2[7];
            Tile tilePos = TexturePosition(direction);

            UVs[0] = new Vector2(tileSize * tilePos.x + (tileSize / 2f), tileSize * tilePos.y + (tileSize / 2f));
            UVs[1] = new Vector2(tileSize * tilePos.x + (tileSize / 2f), tileSize * tilePos.y + tileSize); // N
            UVs[2] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y + (tileSize * .75f)); // NW
            UVs[3] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y + (tileSize * .25f)); // SW
            UVs[4] = new Vector2(tileSize * tilePos.x + (tileSize / 2f), tileSize * tilePos.y); // S
            UVs[5] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y + (tileSize * .25f)); // SE
            UVs[6] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y + (tileSize * .75f)); // NE
            return UVs;
        }

        public virtual Vector2[] QuadFaceUVs(Direction direction)
        {
            Vector2[] UVs = new Vector2[4];
            Tile tilePos = TexturePosition(direction);

            UVs[0] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y); // x: 0.25 y: 0
            UVs[1] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y + tileSize); // x: 0.25 y: 0.25
            UVs[2] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y + tileSize); // x: 0 y:0.25
            UVs[3] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y); // x: 0 y: 0
            return UVs;
        }
        #endregion
    }
}


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
        #endregion

        #region constructors
        public HexBloc()
        {

        }
        #endregion

        #region blocdata
        public virtual void FillMeshData(Chunk chunk, int x, int y, int z, ref MeshModel meshModel)
        {
            // Hexagone Alining
            float nx = (x + z * 0.5f - z / 2);// * (0.866025404f * 2f);
            float nz = z * .5f * 1.5f;

            meshModel.useRenderDataForCol = true;
            if (!chunk.GetBloc(x, y + 1, z).IsSolid(Direction.DOWN))
            {
                FillMesh_FaceUp(chunk, nx, y, nz,ref meshModel);
            }

            if (!chunk.GetBloc(x, y - 1, z).IsSolid(Direction.UP))
            {
                FillMesh_FaceDown(chunk, nx, y, nz, ref meshModel);
            }
            // NW
            if (!chunk.GetBloc(x - 1, y, z + 1).IsSolid(Direction.SE))
            {
                FillMesh_NW(chunk, nx, y, nz, ref meshModel);
            }
            // NE
            if (!chunk.GetBloc(x + 1, y, z + 1).IsSolid(Direction.SW))
            {
                FillMesh_NE(chunk, nx, y, nz, ref meshModel);
            }
            // W
            if (!chunk.GetBloc(x - 1, y, z).IsSolid(Direction.E))
            {
                FillMesh_W(chunk, nx, y, nz, ref meshModel);
            }
            // E
            if (!chunk.GetBloc(x + 1, y, z).IsSolid(Direction.W))
            {
                FillMesh_E(chunk, nx, y, nz, ref meshModel);
            }
            // SW
            if (!chunk.GetBloc(x - 1, y, z - 1).IsSolid(Direction.NE))
            {
                FillMesh_SW(chunk, nx, y, nz, ref meshModel);
            }
            // SE
            if (!chunk.GetBloc(x + 1, y, z - 1).IsSolid(Direction.NW))
            {
                FillMesh_SE(chunk, nx, y, nz, ref meshModel);
            }
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
        }
        #endregion
        #region FacesData

        // Hex Faces
        protected virtual void FillMesh_FaceUp
         (Chunk chunk, float x, int y, float z,ref MeshModel meshModel)
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
        }

        protected virtual void FillMesh_FaceDown
         (Chunk chunk, float x, int y, float z, ref MeshModel meshModel)
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
        }

        // Quad Faces
        protected virtual void FillMesh_NW(Chunk chunk, float x, int y, float z, ref MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z + 0.5f)); // 4 - down
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z + 0.5f)); // 1 - up
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.25f)); // 6- up
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.25f)); // 5 - down
            meshModel.AddQuadFacesTriangles();

            meshModel.uvs.AddRange(QuadFaceUVs(Direction.NW));
        }
        protected virtual void FillMesh_W(Chunk chunk, float x, int y, float z, ref MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.25f)); // 5 - down
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.25f)); // 6- up
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.25f)); // 5- up
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.25f)); // 6 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.W));
        }

        protected virtual void FillMesh_SW(Chunk chunk, float x, int y, float z, ref MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.25f)); // 6 - down
            meshModel.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.25f)); // 5- up
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z - 0.5f)); // 4- up
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z - 0.5f)); // 1 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.SW));
        }
        protected virtual void FillMesh_SE(Chunk chunk, float x, int y, float z, ref MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z - 0.5f)); // 1 - down
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z - 0.5f)); // 4- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.25f)); // 3- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.25f)); // 2 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.SE));
        }

        protected virtual void FillMesh_E(Chunk chunk, float x, int y, float z, ref MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.25f)); // 2 - down
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.25f)); // 3- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.25f)); // 2- up
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.25f)); // 3 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.E));
        }

        protected virtual void FillMesh_NE(Chunk chunk, float x, int y, float z, ref MeshModel meshModel)
        {
            meshModel.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.25f)); // 3 - down
            meshModel.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.25f)); // 2- up
            meshModel.AddVertex(new Vector3(x, y + 0.5f, z + 0.5f)); // 1 - up
            meshModel.AddVertex(new Vector3(x, y - 0.5f, z + 0.5f)); // 4 - down
            meshModel.AddQuadFacesTriangles();
            meshModel.uvs.AddRange(QuadFaceUVs(Direction.NE));
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


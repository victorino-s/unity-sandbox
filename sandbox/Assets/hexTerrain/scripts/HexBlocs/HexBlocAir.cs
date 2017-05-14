using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hexwork
{
    public class HexBlocAir : HexBloc
    {
        public HexBlocAir()
            : base()
        {

        }

        public override void FillMeshData(Chunk chunk, int x, int y, int z, ref MeshModel meshModel)
        {
            
        }

        public override bool IsSolid(Direction direction)
        {
            return false;
        }
    }
}


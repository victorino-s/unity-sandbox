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

        public override MeshModel HexBlocData(Chunk chunk, int x, int y, int z, MeshModel meshModel)
        {
            return meshModel;
        }

        public override bool IsSolid(Direction direction)
        {
            return false;
        }
    }
}


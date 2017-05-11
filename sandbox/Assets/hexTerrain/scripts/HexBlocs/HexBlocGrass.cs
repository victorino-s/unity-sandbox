using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace hexwork
{
    public class HexBlocGrass : HexBloc
    {
        public HexBlocGrass()
            : base()
        {

        }

        public override Tile TexturePosition(Direction direction)
        {
            Tile tile = new Tile();
            switch(direction)
            {
                case Direction.UP:
                    tile.x = 2;
                    tile.y = 0;
                    return tile;
                case Direction.DOWN:
                    tile.x = 1;
                    tile.y = 0;
                    return tile;
            }
            tile.x = 3;
            tile.y = 0;
            return tile;
        }
    }
}


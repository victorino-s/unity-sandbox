using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hexgrid
{
    [System.Serializable]
    public struct HexCoordinates
    {
        public int X { get; private set; }
        public int Z { get; private set; }

        public HexCoordinates(int x, int z)
        {
            X = x;
            Z = z;
        }

        public static Vector3 FromOffsetCoordinates(int x, int y, int z)
        {
            return new Vector3(x - z / 2, y, z);
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Z.ToString() + ")";
        }

        public string ToStringOnSeparateLines()
        {
            return X.ToString() + "\n" + Z.ToString();
        }
    }
}


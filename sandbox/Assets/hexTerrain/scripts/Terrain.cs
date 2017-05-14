using System;
using System.Collections.Generic;
using UnityEngine;

namespace hexwork
{
    public static class Terrain
    {
        public static Vector3 GetBlocPos(Vector3 pos)
        {
            Vector3 blocPos = new Vector3(
                Mathf.RoundToInt(pos.x),
                Mathf.RoundToInt(pos.y),
                Mathf.RoundToInt(pos.z)
                );

            return blocPos;
        }

        public static Vector3 GetBlocPos(RaycastHit hit, bool adjacent = false)
        {
            Vector3 pos = new Vector3(
                    MoveWithinBloc(hit.point.x, hit.normal.x, adjacent),
                    MoveWithinBloc(hit.point.y, hit.normal.y, adjacent),
                    MoveWithinBloc(hit.point.z, hit.normal.z, adjacent)
                );
            return GetBlocPos(pos);
        }

        static float MoveWithinBloc(float pos, float norm, bool adjacent = false)
        {
            float rpos = pos - (int)pos;
            Debug.Log("rpos" + rpos);
            if(rpos == 0.5f || rpos == -0.5f)
            {
                pos = adjacent ? (pos + (norm/2)) : (pos - (norm / 2));
            }
            else
            {
                pos = adjacent ? (pos + ((norm / 2) * (Mathf.Sqrt(3) / 2))) : (pos - ((norm / 2) * (Mathf.Sqrt(3) / 2)));
            }
            
            return (float)pos;
        }

        public static bool SetBloc(RaycastHit hit, HexBloc bloc, bool adjacent = false)
        {
            Chunk chunk = hit.collider.GetComponent<Chunk>();
            if (chunk == null)
                return false;

            Vector3 pos = GetBlocPos(hit, adjacent);
            chunk.world.SetBloc(pos.x, pos.y, pos.z, bloc);

            return true;
        }

        public static HexBloc GetBloc(RaycastHit hit, bool adjacent = false)
        {
            Chunk chunk = hit.collider.GetComponent<Chunk>();
            if (chunk == null)
                return null;

            Vector3 pos = GetBlocPos(hit, adjacent);

            HexBloc bloc = chunk.world.GetHexBloc(pos.x, pos.y, pos.z);

            return bloc;
        }
    }
}

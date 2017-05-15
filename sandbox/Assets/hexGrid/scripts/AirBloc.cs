using System;
using System.Collections.Generic;
using UnityEngine;

namespace hexgrid
{
    public class AirBloc : HexCell
    {
        public AirBloc()
            : base()
        {

        }
        public override void Triangulate(HexChunk chunk)
        {
            // L'air n'a pas de mesh
        }

        public override bool IsSolid()
        {
            return false;
        }

        public void NotifyNeighbors(HexChunk chunk)
        {

            // Lateral faces
            for(int x = -1, i = 3; x <= 1; x+= 2)
            {
                for(int z = -1; z <= 1; z++, i++)
                {
                    if(chunkPosition.x != 0 && chunkPosition.x < chunk.chunkSize && chunkPosition.y != 0 && chunkPosition.y < chunk.chunkSize && chunkPosition.z != 0 && chunkPosition.z < chunk.chunkSize)
                    {
                        HexCell neighbor = chunk.GetCellFromArray((int)chunkPosition.x + x, (int)chunkPosition.y, (int)chunkPosition.z + z);
                        if (neighbor.IsSolid())
                        {
                            neighbor.SetFaceToRender((Faces)i, true);
                        }
                    }
                }
            }
            if(chunkPosition.y < chunk.chunkSize)
            {
                if (chunk.GetCellFromArray((int)chunkPosition.x, (int)chunkPosition.y + 1, (int)chunkPosition.z).IsSolid())
                {
                    chunk.GetCellFromArray((int)chunkPosition.x, (int)chunkPosition.y + 1, (int)chunkPosition.z).SetFaceToRender(Faces.Down, true);
                }
            }
            
            if(chunkPosition.y > 0)
            {
                if (chunk.GetCellFromArray((int)chunkPosition.x, (int)chunkPosition.y - 1, (int)chunkPosition.z).IsSolid())
                {
                    chunk.GetCellFromArray((int)chunkPosition.x, (int)chunkPosition.y - 1, (int)chunkPosition.z).SetFaceToRender(Faces.Up, true);
                }
            }
            


            /*
             * Pour chaque cube en face de chaque face de ce bloc d'air, appeler une méthode sur le 
             * bloc cible du style :
             * si bloc en face de moi = bloc renderable
             * alors
             * bloc.SetRenderFaceByPosition( (this "bloc d'air".)position);
             * 
             * et dans bloc visé :
             * SetRenderFaceByPosition(Vector3 position)
             * {
             *      direction = (param)position - (ce cube)position;
             *      
             *      renderedFaces[direction convertie en face].rendered = true;
             * }
             * 
             */
        }
    }
}


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
            // Up
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


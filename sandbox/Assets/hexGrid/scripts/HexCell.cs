using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace hexgrid
{
    public class HexCell
    {
        public HexCell()
        {

        }
        public Vector3 coordinates;

        public Vector3 position;

        public Vector3 chunkPosition;

        public bool isVisible = false;

        public Dictionary<Faces, bool> renderedFaces = new Dictionary<Faces, bool>(6)
        {
            { Faces.NE, false },
            { Faces.E, false },
            { Faces.SE, false },
            { Faces.SW, false },
            { Faces.W, false },
            { Faces.NW, false }
        };
        

        public virtual void Triangulate(HexChunk chunk)
        {
            Vector3 center = position;
            Vector3 centerUp = new Vector3(center.x, center.y + .5f, center.z);
            Vector3 centerDown = new Vector3(center.x, center.y - .5f, center.z);

            /*
             * Revoir cette partie car elle va surement completement péter
             * 
             * Faire un truc optimisé pour rendre chaque face du tableau renderedFaces
             * qui a sa valeur à true.
             * 
             * ++++ Faire une fonction RenderAllBlocTriangles(); + HideAllBlocTriangles();
             * 
             * Faire un objet Face avec dedans "isRedered, Direction, RenderTriangle, HideTriangle"
             * 
             */
            if(renderedFaces[Faces.Up] || renderedFaces[Faces.Down])
            {
                for (int i = 0; i < 6; i++) // up & down triangle
                {
                    if (i == 5)
                    {
                        // Face Up & Down

                        chunk.AddTriangle(centerUp, centerUp + HexMetrics.UpFaceCorners[i], centerUp + HexMetrics.UpFaceCorners[0]);
                        chunk.AddTriangle(centerDown, centerDown + HexMetrics.DownFaceCorners[0], centerDown + HexMetrics.DownFaceCorners[i]);

                        // Lateral Faces
                        chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[i]);
                        chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[i], centerDown + HexMetrics.DownFaceCorners[i]);
                    }
                    else
                    {
                        // Face Up & Down
                        chunk.AddTriangle(centerUp, centerUp + HexMetrics.UpFaceCorners[i], centerUp + HexMetrics.UpFaceCorners[i + 1]);
                        chunk.AddTriangle(centerDown, centerDown + HexMetrics.DownFaceCorners[i + 1], centerDown + HexMetrics.DownFaceCorners[i]);

                        // Lateral Faces
                        chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[i + 1], centerUp + HexMetrics.UpFaceCorners[i + 1], centerUp + HexMetrics.UpFaceCorners[i]);
                        chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[i + 1], centerUp + HexMetrics.UpFaceCorners[i], centerDown + HexMetrics.DownFaceCorners[i]);
                    }
                }
            }
            
            else
            {
                for(Faces i = Faces.NE; i <= Faces.NW; i += 1)
                {
                    int ni = (int)i - 2;

                    if(i == Faces.NW)
                    {
                        chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[ni]);
                        chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[ni], centerDown + HexMetrics.DownFaceCorners[ni]);
                    }
                    else
                    {
                        chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[ni + 1], centerUp + HexMetrics.UpFaceCorners[ni + 1], centerUp + HexMetrics.UpFaceCorners[ni]);
                        chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[ni + 1], centerUp + HexMetrics.UpFaceCorners[ni], centerDown + HexMetrics.DownFaceCorners[ni]);
                    }
                }
            }

            
        }

        public void SetFaceToRender(Faces face, bool value)
        {
            renderedFaces[face] = value;
        }


        public virtual bool IsSolid()
        {
            return true;
        }
    }
}

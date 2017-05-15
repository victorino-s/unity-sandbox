using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace hexgrid
{
    public class HexCell
    {
        public Vector3 coordinates;

        public Vector3 position;

        public Vector3 chunkPosition;

        public bool isVisible = false;

        public Dictionary<Faces, bool> renderedFaces;
        public HexCell()
        {
            renderedFaces = new Dictionary<Faces, bool>(6)
            {
                {Faces.Up, false },
                {Faces.Down, false },
                { Faces.NE, false },
                { Faces.E, false },
                { Faces.SE, false },
                { Faces.SW, false },
                { Faces.W, false },
                { Faces.NW, false },
            };
        }





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

            if (renderedFaces[Faces.Up])
            {
                for (int i = 0; i < 6; i++) // up & down triangle
                {
                    if (i == 5)
                    {
                        chunk.AddTriangle(centerUp, centerUp + HexMetrics.UpFaceCorners[i], centerUp + HexMetrics.UpFaceCorners[0]);
                    }
                    else
                    {
                        chunk.AddTriangle(centerUp, centerUp + HexMetrics.UpFaceCorners[i], centerUp + HexMetrics.UpFaceCorners[i + 1]);
                    }
                }
            }

            if (renderedFaces[Faces.Down])
            {
                for (int i = 0; i < 6; i++) // up & down triangle
                {
                    if (i == 5)
                    {
                        chunk.AddTriangle(centerDown, centerDown + HexMetrics.DownFaceCorners[0], centerDown + HexMetrics.DownFaceCorners[i]);
                    }
                    else
                    {
                        chunk.AddTriangle(centerDown, centerDown + HexMetrics.DownFaceCorners[i + 1], centerDown + HexMetrics.DownFaceCorners[i]);
                    }
                }
            }

            if (renderedFaces[Faces.NE])
            {
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0 + 1], centerUp + HexMetrics.UpFaceCorners[0 + 1], centerUp + HexMetrics.UpFaceCorners[0]);
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0 + 1], centerUp + HexMetrics.UpFaceCorners[0], centerDown + HexMetrics.DownFaceCorners[0]);
            }

            if (renderedFaces[Faces.E])
            {
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[1 + 1], centerUp + HexMetrics.UpFaceCorners[1 + 1], centerUp + HexMetrics.UpFaceCorners[1]);
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[1 + 1], centerUp + HexMetrics.UpFaceCorners[1], centerDown + HexMetrics.DownFaceCorners[1]);
            }

            if (renderedFaces[Faces.SE])
            {
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[2 + 1], centerUp + HexMetrics.UpFaceCorners[2 + 1], centerUp + HexMetrics.UpFaceCorners[2]);
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[2 + 1], centerUp + HexMetrics.UpFaceCorners[2], centerDown + HexMetrics.DownFaceCorners[2]);
            }

            if (renderedFaces[Faces.SW])
            {
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[4], centerUp + HexMetrics.UpFaceCorners[4], centerUp + HexMetrics.UpFaceCorners[3]);
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[4], centerUp + HexMetrics.UpFaceCorners[3], centerDown + HexMetrics.DownFaceCorners[3]);
            }

            if (renderedFaces[Faces.W])
            {
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[5], centerUp + HexMetrics.UpFaceCorners[5], centerUp + HexMetrics.UpFaceCorners[4]);
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[5], centerUp + HexMetrics.UpFaceCorners[4], centerDown + HexMetrics.DownFaceCorners[4]);
            }

            if (renderedFaces[Faces.NW])
            {
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[5]);
                chunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[5], centerDown + HexMetrics.DownFaceCorners[5]);
            }
        }

        public virtual void UpdateMeshData(int x, int y, int z, HexChunk hexChunk)
        {
            if (coordinates.x == 0 || coordinates.y == 0 || coordinates.z == 0 || coordinates.x == hexChunk.chunkSize - 1 || coordinates.y == hexChunk.chunkSize - 1 || coordinates.z == hexChunk.chunkSize - 1)
            {
                return;
            }
            Vector3 center = position;
            Vector3 centerUp = new Vector3(center.x, center.y + .5f, center.z);
            Vector3 centerDown = new Vector3(center.x, center.y - .5f, center.z);
            Debug.Log("Test");
            if (!hexChunk.GetCellFromArray(coordinates.x, coordinates.y + 1, coordinates.z).IsSolid())
            {
                for (int i = 0; i < 6; i++) // up & down triangle
                {
                    if (i == 5)
                    {
                        hexChunk.AddTriangle(centerUp, centerUp + HexMetrics.UpFaceCorners[i], centerUp + HexMetrics.UpFaceCorners[0]);
                    }
                    else
                    {
                        hexChunk.AddTriangle(centerUp, centerUp + HexMetrics.UpFaceCorners[i], centerUp + HexMetrics.UpFaceCorners[i + 1]);
                    }
                }
            }

            if (!hexChunk.GetCellFromArray(coordinates.x, coordinates.y - 1, coordinates.z).IsSolid())
            {
                for (int i = 0; i < 6; i++) // up & down triangle
                {
                    if (i == 5)
                    {
                        hexChunk.AddTriangle(centerDown, centerDown + HexMetrics.DownFaceCorners[0], centerDown + HexMetrics.DownFaceCorners[i]);
                    }
                    else
                    {
                        hexChunk.AddTriangle(centerDown, centerDown + HexMetrics.DownFaceCorners[i + 1], centerDown + HexMetrics.DownFaceCorners[i]);
                    }
                }
            }

            // NE
            if (!hexChunk.GetCellFromArray(coordinates.x, coordinates.y, coordinates.z + 1).IsSolid())
            {
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[1], centerUp + HexMetrics.UpFaceCorners[1], centerUp + HexMetrics.UpFaceCorners[0]);
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[1], centerUp + HexMetrics.UpFaceCorners[0], centerDown + HexMetrics.DownFaceCorners[0]);
            }
            // E
            if (!hexChunk.GetCellFromArray(coordinates.x + 1, coordinates.y, coordinates.z).IsSolid())
            {
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[2], centerUp + HexMetrics.UpFaceCorners[2], centerUp + HexMetrics.UpFaceCorners[1]);
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[2], centerUp + HexMetrics.UpFaceCorners[1], centerDown + HexMetrics.DownFaceCorners[1]);
            }
            // SE
            if (!hexChunk.GetCellFromArray(coordinates.x, coordinates.y, coordinates.z - 1).IsSolid())
            {
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[3], centerUp + HexMetrics.UpFaceCorners[3], centerUp + HexMetrics.UpFaceCorners[2]);
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[3], centerUp + HexMetrics.UpFaceCorners[2], centerDown + HexMetrics.DownFaceCorners[2]);
            }
            // SW
            if (!hexChunk.GetCellFromArray(coordinates.x - 1, coordinates.y, coordinates.z - 1).IsSolid())
            {
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[4], centerUp + HexMetrics.UpFaceCorners[4], centerUp + HexMetrics.UpFaceCorners[3]);
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[4], centerUp + HexMetrics.UpFaceCorners[3], centerDown + HexMetrics.DownFaceCorners[3]);
            }
            // W
            if (!hexChunk.GetCellFromArray(coordinates.x - 1, coordinates.y, coordinates.z).IsSolid())
            {
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[5], centerUp + HexMetrics.UpFaceCorners[5], centerUp + HexMetrics.UpFaceCorners[4]);
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[5], centerUp + HexMetrics.UpFaceCorners[4], centerDown + HexMetrics.DownFaceCorners[4]);
            }
            // NW
            if (!hexChunk.GetCellFromArray(coordinates.x - 1, coordinates.y, coordinates.z + 1).IsSolid())
            {
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[5]);
                hexChunk.AddTriangle(centerDown + HexMetrics.DownFaceCorners[0], centerUp + HexMetrics.UpFaceCorners[5], centerDown + HexMetrics.DownFaceCorners[5]);
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

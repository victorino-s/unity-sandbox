using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hexgrid
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class HexChunk : MonoBehaviour
    {
        Mesh hexMesh;
        MeshCollider meshCollider;
        List<Vector3> vertices;
        List<int> triangles;

        public int chunkSize = 6;
        HexCell[,,] chunkCells;

        void Awake()
        {
            GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
            meshCollider = GetComponent<MeshCollider>();

            hexMesh.name = "Chunk";
            vertices = new List<Vector3>();
            triangles = new List<int>();

            chunkCells = new HexCell[chunkSize, chunkSize, chunkSize];

            for (int z = 0; z < chunkSize; z++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    for (int x = 0; x < chunkSize; x++)
                    {
                        CreateCell(x, y, z);
                    }
                }
            }
        }


        // Create TerrainClass to handle terrain's template generation insted of this

        void CreateCell(int x, int y, int z)
        {
            Vector3 position;
            position.x = (x + z * .5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.y = y;
            position.z = z * (HexMetrics.outerRadius * 1.5f);
            HexCell cell;
            if (x < (chunkSize / 2))
            {
                if(y < 2)
                {
                    cell = chunkCells[x, y, z] = new HexCell();
                }
                else
                {
                    cell = chunkCells[x, y, z] = new AirBloc();
                }
            }
            else
            {
                if (y < 4)
                {
                    cell = chunkCells[x, y, z] = new HexCell();
                }
                else
                {
                    cell = chunkCells[x, y, z] = new AirBloc();
                }
            }
            // -- End of bad bloc
            cell.position = position;
            cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, y, z);
            Debug.Log("Cell[" + x + "," + y + "," + z + "] | position : " + cell.position + " || coordinates : " + cell.coordinates);
        }

        void CreateAirCell(int x,int y, int z)
        {
            Vector3 position;
            position.x = (x + z * .5f - z / 2) * (HexMetrics.outerRadius * 1.5f);
            position.y = y;
            position.z = z * (HexMetrics.outerRadius * 1.5f);
            
        }

        // Use this for initialization
        void Start()
        {
            Triangulate(chunkCells);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Triangulate(HexCell[,,] cells)
        {
            hexMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            
            for(int z = 0; z < chunkSize; z++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    for (int x = 0; x < chunkSize; x++)
                    {
                        if(cells[x, y, z].chunkPosition == null)
                            cells[x, y, z].chunkPosition = new Vector3(x, y, z);

                        if (cells[x, y, z] is AirBloc)
                            ((AirBloc)cells[x, y, z]).NotifyNeighbors(this);

                        cells[x, y, z].Triangulate(this);
                    }
                }
            }
            hexMesh.vertices = vertices.ToArray();
            hexMesh.triangles = triangles.ToArray();
            hexMesh.RecalculateNormals();

            meshCollider.sharedMesh = hexMesh;
        }

        public void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
        }

        public HexCell GetCellFromArray(int x, int y, int z)
        {
            return chunkCells[x, y, z];
        }
    }
}


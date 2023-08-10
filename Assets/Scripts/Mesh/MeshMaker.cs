using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaker : MonoBehaviour
{
    public Material grass;
    private void Start()
    {
        MakeMesh(10,10);
    }
    
    void MakeMesh(int width, int length)
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        

        var heights = RandomHeights(width, length);
        var cells = CreateMeshCells(heights);
        //list of verts (this will only do flats for now)
        var verts = MakeCellVerts(cells);
        //list of tris
        var tris = MakeCellTris(cells);

        filter.mesh.vertices = verts.ToArray();
        filter.mesh.triangles = tris.ToArray();
        //filter.mesh.RecalculateNormals();
        renderer.material = grass;
    }
    List<int> MakeCellTris(List<MeshCell> cells)
    {
        var tris = new List<int>();
        for (int i = 0; i < cells.Count; i++)
        {
            //var c = cells[i];
            int offset = 4 * i;
            tris.AddRange(new List<int> { 0 + offset, 2 + offset, 1 + offset,
                                 0 + offset, 3 + offset, 2 + offset});
        }
        return tris;
    }
    List<Vector3> MakeCellVerts(List<MeshCell> cells)
    {
        var verts = new List<Vector3>();
        for(int i = 0; i < cells.Count; i++)
        {
            var c = cells[i];
            var corners = c.Corners();
            verts.AddRange(corners);
        }
        return verts;
    }

    List<MeshCell> CreateMeshCells(List<List<int>> heights)
    {
        float size = 1;
        var cells = new List<MeshCell>();
        for(int i = 0; i < heights.Count; i++) //i represents the x direction
        {
            for(int j = 0; j < heights[0].Count; j++) //j represents the z direction
            {
                //origin is 0,0 and corner of mesh
                Vector3 cellCentre = new Vector3(i * size + size / 2, heights[i][j], j * size + size / 2);
                var meshCell = new MeshCell(i, j, heights[i][j], size, cellCentre);
                cells.Add(meshCell);
            }
        }
        return cells;
    }

    List<List<int>> RandomHeights(int width, int length)
    {
        //start by giving a grid of zeros with a single raised square at (1,1)
        var grid = new List<List<int>>();
        for(int i = 0; i < width; i++)
        {
            var row = new List<int>();
            grid.Add(row);
            for(int j = 0; j < length; j++)
            {
                row.Add(0);
            }
        }
        grid[1][1] = 1;
        return grid;
    }
}

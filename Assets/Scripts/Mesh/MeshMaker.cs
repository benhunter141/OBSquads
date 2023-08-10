using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaker : MonoBehaviour
{
    public Material grass;
    private void Start()
    {
        int width = 5;
        int length = 5;
        MakeMesh(width, length);
    }
    
    void MakeMesh(int width, int length)
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        

        var heights = RandomHeights(width, length);
        var cells = CreateMeshCells(heights);

        var verts = MakeCellVerts(cells);
        var uv = MakeUVMap(verts, width, length);
        var tris = MakeCellTris(cells);
        var tangents = MakeTangents(verts);

        filter.mesh.vertices = verts.ToArray();
        filter.mesh.triangles = tris.ToArray();
        filter.mesh.uv = uv.ToArray();
        filter.mesh.tangents = tangents.ToArray();
        filter.mesh.RecalculateNormals();
        renderer.material = grass;
    }

    List<Vector4> MakeTangents(List<Vector3> verts)
    {
        var tangents = new List<Vector4>();
        foreach (var v in verts)
            tangents.Add(new Vector4(1, 0, 0, -1));
        return tangents;
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

    List<Vector2> MakeUVMap(List<Vector3> verts, int width, int length)
    {
        var uvs = new List<Vector2>();
        foreach(var v in verts)
        {
            var uv = new Vector2(v.x / width, v.z / length);
            uvs.Add(uv);
        }
        return uvs;
    }
    List<Vector3> MakeCellVerts(List<MeshCell> cells) // first vert should be at 0,0,0
    {
        var verts = new List<Vector3>();
        for(int i = 0; i < cells.Count; i++)
        {
            var c = cells[i];
            var corners = c.Corners();
            verts.AddRange(corners);
        }
        Debug.Log("first vert at: ");
        Helpers.Print(verts[0]);
        Debug.Log("should be 3 x zero");
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

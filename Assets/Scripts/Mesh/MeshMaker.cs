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
        float cellSize = 1;
        MakeMesh(width, length, cellSize);

        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = grass;

        gameObject.AddComponent<MeshCollider>();
    }





    MeshFilter MakeMesh(int width, int length, float cellSize)
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();

        var heights = RandomHeights(width, length);
        var verts = SmoothedVerts(heights, cellSize);
        var tris = MakeCellTris(width, length);


        //var cellGrid = CreateMeshCells(heights); //change to include neighbors
        //var verts = MakeCellVerts(cellGrid); //change to not make duplicates
        //var tris = MakeCellTris(cellGrid); //change to match non-duplicate vert list
        var uv = MakeUVMap(verts, width, length);
        var tangents = MakeTangents(verts);

        filter.mesh.vertices = verts.ToArray();
        filter.mesh.triangles = tris.ToArray();
        filter.mesh.uv = uv.ToArray();
        filter.mesh.tangents = tangents.ToArray();
        filter.mesh.RecalculateNormals();

        return filter;
    }

    List<Vector3> SmoothedVerts(List<List<int>> heights, float cellSize)
    {
        var verts = new List<Vector3>();
        for(int i = 0; i < heights.Count + 1; i++)
        {
            for(int j = 0; j < heights[0].Count + 1; j++)
            {
                var neighborHeights = new List<int>();
                if (i > 0 && j > 0) 
                    neighborHeights.Add(heights[i - 1][j - 1]);
                if (i < heights.Count && j > 0)
                    neighborHeights.Add(heights[i][j - 1]);
                if (i < heights.Count && j < heights[0].Count)
                    neighborHeights.Add(heights[i][j]);
                if (i > 0 && j < heights[0].Count) 
                    neighborHeights.Add(heights[i - 1][j]);
                float averageHeight = Helpers.AverageOf(neighborHeights);
                Vector3 position = new Vector3(cellSize * i, averageHeight, cellSize * j);
                verts.Add(position);
            }
        }
        return verts;
    }

    List<Vector4> MakeTangents(List<Vector3> verts)
    {
        var tangents = new List<Vector4>();
        foreach (var v in verts)
            tangents.Add(new Vector4(1, 0, 0, -1));
        return tangents;
    }

    List<int> MakeCellTris(int width, int length)
    {
        var tris = new List<int>();
        for(int i = 0; i < width; i++)
        {
            int rowStart = i * (length + 1);
            int nextRowStart = (i + 1) * (length + 1);
            Debug.Log($"rowStart:{rowStart}, nextRowStart:{nextRowStart}");
            for (int j = 0; j < length; j++)
            {
                var cellTris = new List<int>
                {
                    rowStart + j, rowStart + j + 1, nextRowStart + j + 1,
                    rowStart + j, nextRowStart + j + 1, nextRowStart + j
                };
                tris.AddRange(cellTris);
            }
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

    List<List<int>> RandomHeights(int width, int length)
    {
        //start by giving a grid of zeros with a few raised squares around (1,1)
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
        grid[2][3] = 3;
        grid[1][2] = 2;
        grid[1][3] = 6;
        grid[3][0] = 5;
        return grid;
    }
}

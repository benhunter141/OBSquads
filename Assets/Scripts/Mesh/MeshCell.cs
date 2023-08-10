using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCell
{
    public int x { get; private set; }
    public int z { get; private set; }
    public int height { get; private set; }
    public float size { get; private set; }
    public Vector3 centre { get; private set; }
    public MeshCell(int _x, int _z, int _height, float _size, Vector3 _centre)
    {
        x = _x;
        z = _z;
        height = _height;
        size = _size;
        centre = _centre;
    }

    public List<Vector3> Corners()       //starting with the +z,+x corner and going clockwise   
    {
        float halfSize = size / 2;
        var corners = new List<Vector3> {
            new Vector3(halfSize, 0, halfSize) + centre,
            new Vector3(-halfSize, 0, halfSize) + centre,
            new Vector3(-halfSize, 0, -halfSize) + centre,
            new Vector3(halfSize, 0, -halfSize) + centre};
        return corners;
    }
}

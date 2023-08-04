using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHolder : MonoBehaviour
{
    public GameObject body, ring, triangle;
    public Animator animator;
    MeshRenderer bodyRenderer, shoulderRenderer, armRenderer, handRenderer, ringRenderer, triangleRenderer;
    public TargetIndicator targetIndicator;
    public Rigidbody rb;
    

    private void Awake()
    {
        CacheRenderers();
    }

    public void SetColors(Material primaryColor, Material accentColor)
    {
        SetPrimaryColor(primaryColor);
        SetAccentColor(accentColor);
    }

    void CacheRenderers()
    {
        bodyRenderer = body.GetComponent<MeshRenderer>();
        ringRenderer = ring.GetComponent<MeshRenderer>();
        triangleRenderer = triangle.GetComponent<MeshRenderer>();
    }

    void SetPrimaryColor(Material primaryColor)
    {
        bodyRenderer.material = primaryColor;
    }

    void SetAccentColor(Material accentColor)
    {
        triangleRenderer.material = accentColor;
    }
}

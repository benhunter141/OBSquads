using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHolder : MonoBehaviour
{
    public BaseUnit unit;
    public GameObject body, ring, pointerAnchor, pointer, healthBarObject;
    public Animator animator;
    public MeshRenderer bodyRenderer, ringRenderer, triangleRenderer;
    public CanvasRenderer iconSprite;
    public TargetIndicator targetIndicator;
    public Rigidbody rb;
    new public Collider collider;

    public List<Renderer> AllRenderers() //Can't use this with CanvasRenderers... Solution: Fade Canvas Renderers separately
    {
        var renderers = new List<Renderer>();
        renderers.Add(bodyRenderer);
        renderers.Add(ringRenderer);
        renderers.Add(triangleRenderer);
        return renderers;
    }

    public List<CanvasRenderer> AllCanvasRenderers()
    {
        var canvasRenderers = new List<CanvasRenderer>();
        canvasRenderers.Add(iconSprite);
        return canvasRenderers;
    }

    public void SetColors(Material primaryColor, Material accentColor)
    {
        SetPrimaryColor(primaryColor);
        SetAccentColor(accentColor);
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

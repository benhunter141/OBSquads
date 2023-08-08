using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHolder : MonoBehaviour
{
    public BaseUnit unit;
    public GameObject body, ring, pointerAnchor, pointer, healthBarObject;
    public Animator animator;
    public MeshRenderer bodyRenderer, ringRenderer, triangleRenderer, weaponRenderer1, weaponRenderer2;
    public TargetIndicator targetIndicator;
    public Rigidbody rb;
    new public Collider collider;

    public List<Renderer> AllRenderers()
    {
        var renderers = new List<Renderer>();
        renderers.Add(bodyRenderer);
        renderers.Add(ringRenderer);
        renderers.Add(triangleRenderer);
        renderers.Add(weaponRenderer1);
        renderers.Add(weaponRenderer2);
        return renderers;
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

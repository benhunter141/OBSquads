using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar
{
    GameObject bar;
    public StatusBar(GameObject _bar)
    {
        bar = _bar;
        UpdateBar(1, 1);
    }
    public void UpdateBar(float currentValue, float maxValue)
    {
        float size = currentValue / maxValue;
        if (size >= 1 || size < 0) size = 0;
        Vector3 scale = bar.transform.localScale;
        scale.y = size;
        bar.transform.localScale = scale;
    }
}

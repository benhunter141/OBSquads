using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{

    public void UpdateBar(float currentValue, float maxValue)
    {
        float size = currentValue / maxValue;
        if (size >= 1 || size < 0) size = 0;
        Vector3 scale = transform.localScale;
        scale.y = size;
        transform.localScale = scale;
    }
}

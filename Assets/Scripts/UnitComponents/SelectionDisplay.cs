using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionDisplay //green, glowy, bouncy ring when actively selected
{
    GameObject ring;
    BaseUnit unit;
    Renderer renderer;

    public SelectionDisplay(BaseUnit u, GameObject sr)
    {
        ring = sr;
        unit = u;
        renderer = renderer = ring.GetComponent<Renderer>();
    }

    //public IEnumerator ShowActiveSelection()
    //{
    //    float seconds = 10;
    //    int frames = (int)(seconds * 50);
    //    float period = 2;
    //    for(int i = 0; i < frames; i++)
    //    {
    //        //turn ring green
    //        //play with alpha
    //        //make ring bigger and smaller (minimum is current size)
    //        float position = Mathf.Sin((float)i * 50f);
    //        //position will fluctuate from -1 to +1

    //        yield return Helpers.EndOfFrame;
    //    }
    //}

    public void TurnOn()
    {
        //ring.SetActive(true);
        renderer.material = ServiceLocator.Instance.colorManager.green;
    }

    public void TurnOff()
    {
        renderer.material = ServiceLocator.Instance.colorManager.yellow;
        //ring.SetActive(false);
    }
}

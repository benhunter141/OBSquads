using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tactics : ScriptableObject
{
    public abstract void Perform(BaseUnit unit);
}

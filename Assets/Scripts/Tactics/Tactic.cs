using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tactic : ScriptableObject
{
    public abstract void Perform(BaseUnit unit);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectChangeSize : SpellEffect
{
    private bool isActivated = false;
    public override void Activate(params object[] vars)
    {
        if (!isActivated)
        {
            Vector3 size = (Vector3)vars[0];
            transform.localScale = size;
            isActivated = true;
        }
    }

    public override float GetManaCost(params object[] vars)
    {
        if(!isActivated)
        {
            Vector3 size = (Vector3)vars[0];
            return size.sqrMagnitude;
        }
        return 0;
    }
}

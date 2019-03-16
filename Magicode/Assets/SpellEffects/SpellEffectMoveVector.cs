using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectMoveVector : SpellEffect
{
    public override void Activate(params object[] vars)
    {
        Vector3 direction = (Vector3)vars[0];
        transform.Translate(direction);
    }

    public override float GetManaCost(params object[] vars)
    {
        Vector3 vector = (Vector3)vars[0];
        return vector.sqrMagnitude * Time.deltaTime;
    }
}

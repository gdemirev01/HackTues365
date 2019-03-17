using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectMoveForward : SpellEffect
{

    public override void Activate(params object[] vars)
    {
        float speed = (float)Convert.ToDecimal(vars[0]);
        Debug.Log("Speed " + speed);
        transform.position += speed * transform.forward * Time.deltaTime;
    }

    public override float GetManaCost(params object[] vars)
    {
        
        float speed = (float) Convert.ToDecimal(vars[0]);
        return speed * Time.deltaTime;
    }
}

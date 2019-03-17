using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectAreaOfEffectDamage : SpellEffect
{
    public override void Activate(params object[] vars)
    {
        float damage = (float)Convert.ToDecimal(vars[0]);
        float range = (float)Convert.ToDecimal(vars[1]);

        foreach(Collider collider in Physics.OverlapSphere(transform.position, range))
        {
            if(collider.GetComponent<BaseMinionBehaviour>())
            {
                Debug.Log("Deal aoe damage to ", collider.gameObject);
                collider.GetComponent<BaseMinionBehaviour>().health -= damage;
            }
        }
    }

    public override float GetManaCost(params object[] vars)
    {
        float damage = (float)Convert.ToDecimal(vars[0]);
        float range = (float)Convert.ToDecimal(vars[1]);

        return damage * range;
    }
}

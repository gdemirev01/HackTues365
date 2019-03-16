using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectMoveForward : SpellEffect
{
    [SerializeField]
    float speed = 20;

    public override void Activate(params object[] vars)
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }
}

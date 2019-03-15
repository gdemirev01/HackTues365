using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectMoveForward : SpellEffect
{
    [SerializeField]
    float speed = 20;

    public override void Activate(params object[] vars)
    {
        speed = (int) vars[0];
        Vector3 pos = (Vector3) vars[1];

        Debug.Log(pos);
        
        transform.Translate(speed * transform.forward * Time.deltaTime);
    }

    public override VariableType[] GetExpectedVariableTypes()
    {
        return new VariableType[] { VariableType.Float };
    }
}

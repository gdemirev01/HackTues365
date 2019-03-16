using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectClone : SpellEffect
{
    private bool isActivated = false;
    public override void Activate(params object[] vars)
    {
        if (!isActivated)
        {
            var numberOfclones = (int)vars[0];
            for (int i = 0; i < numberOfclones; i++)
            {
                Instantiate(transform, new Vector3(transform.position.x + i * 2, transform.position.y, transform.position.z), transform.rotation);
            }
            isActivated = true;
        }
    }
}

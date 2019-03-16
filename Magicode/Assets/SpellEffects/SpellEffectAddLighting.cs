using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectAddLighting : SpellEffect
{
    private bool isActivated = false;
    public override void Activate(params object[] vars)
    {
        if (!isActivated)
        {
            transform.Find("ErekiBall").gameObject.SetActive(enabled);
            // set damage++
            isActivated = true;
        }
    }
}

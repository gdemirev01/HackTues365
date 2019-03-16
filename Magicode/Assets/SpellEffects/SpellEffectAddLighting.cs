using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectAddLighting : SpellEffect
{
    [SerializeField]
    private float manaCost = 50;

    [SerializeField]
    private GameObject particles;

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

    public override float GetManaCost(params object[] vars)
    {
        if(!isActivated)
        {
            return 50;
        }
        return 0;
    }
}

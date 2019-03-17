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
    private bool hasTakenMana = false;

    public override void Activate(params object[] vars)
    {
        if (!isActivated)
        {
            Debug.LogWarning("Activate!");
            Instantiate(particles, transform);
            isActivated = true;
        }
    }

    public override float GetManaCost(params object[] vars)
    {
        
        if (!hasTakenMana)
        {
            Debug.LogWarning("Return 50 mana");
            hasTakenMana = true;
            return 50;
        }
        return 0;
    }
}

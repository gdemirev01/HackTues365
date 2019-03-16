using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private List<Spell> spells;

    private bool canCast;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnSpell(0);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            SpawnSpell(1);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            SpawnSpell(2);
        }
    }

    public void SpawnSpell(int index)
    {
        Spell spell = Instantiate(
            spells[index], 
            transform.position + transform.forward * 5, 
            Quaternion.identity
        );

        spell.enabled = true;
        spell.gameObject.SetActive(true);
    }

    public Spell GetSpellObject(int index)
    {
        return spells[index];
    }
}

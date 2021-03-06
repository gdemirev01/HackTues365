﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaseMinionBehaviour : NetworkBehaviour {
    public bool isAllied = false;
    public Dictionary<string, Spell> spells;
    public GameObject bulletPrefab;
    [SyncVar] public float health;
    [SyncVar] public float mana;
    public static int minionsCounter = 6;
    
    [SerializeField][Tooltip("spell to be copied from scene")]
    private List<Spell> spellBook;

    public int[] spellsEquipped = new int [4];
    [SerializeField]
    private float manaGainPerSecond = 50f;
    private float maxMana;

    private bool canCast = true;

    private void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            spellsEquipped[i] = i;
        }
        maxMana = mana;
    }

    private void Update()
    {
        if (health <= 0)
        {
            BaseMinionBehaviour.minionsCounter--;
            Destroy(gameObject);
        }

        // TODO: Remove.
        if (canCast)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SpawnSpell(0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                // SpawnSpell(1); nqmame tolkova spellove
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                // SpawnSpell(2);
            }
        }
        mana += manaGainPerSecond * Time.deltaTime;
        if(mana > maxMana)
        {
            mana = maxMana;
        }
    }

    public void SpawnSpell(int index)
    {
        Spell spell = Instantiate(
            spellBook[index],
            transform.position + transform.forward * 5,
            transform.rotation
        );

        spell.enabled = true;
        spell.gameObject.SetActive(true);
        spell.SetMinion(this);
    }
    

    public Spell GetSpellObject(int index)
    {
        return spellBook[index];
    }

    public void RemoveMana(float mana)
    {
        this.mana -= mana;
    }

    public bool HasMana()
    {
        return mana >= 0;
    }
    
}

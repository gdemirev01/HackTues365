using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaseMinionBehaviour : NetworkBehaviour {
    public bool isAllied = false;
    public Dictionary<string, Spell> spells;
    public GameObject bulletPrefab;
    [SyncVar] public float health;
    public static int minionsCounter = 6;

    private void Update()
    {
        if (health <= 0)
        {
            BaseMinionBehaviour.minionsCounter--;
            //Destroy(this.gameObject);
        }
    }
 }

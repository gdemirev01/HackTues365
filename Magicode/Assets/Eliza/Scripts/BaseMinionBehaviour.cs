using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaseMinionBehaviour : NetworkBehaviour {
    [SerializeField] [SyncVar] private int player = 0;
    [SyncVar] public int id;

    public int Player {
        get {
            return player;
        }
        set {
            if(player==0)
            {
                player = value;
            }
        }
    }
}

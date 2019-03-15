using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaseMinionBehaviour : NetworkBehaviour {
    [SerializeField] private int player = 0;
    public int id;

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

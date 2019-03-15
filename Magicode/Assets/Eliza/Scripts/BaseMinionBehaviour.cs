using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMinionBehaviour : MonoBehaviour {
    private int player = 0;
    public int Player {
        set {
            player = value;
            Debug.Log(player);
        }
        get {
            return player;
        }
    }
}

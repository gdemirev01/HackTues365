using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EStatic {
    public static Dictionary<int, Color> playerColors;

    static EStatic()
    {
        // GAIA DONT USE PLAYER 0
        playerColors.Add(0, Color.gray);

        playerColors.Add(1, Color.blue);
        playerColors.Add(2, Color.red);
        playerColors.Add(3, Color.green);
        playerColors.Add(4, Color.magenta);
        playerColors.Add(5, Color.yellow);
        playerColors.Add(6, Color.cyan);
    }
}

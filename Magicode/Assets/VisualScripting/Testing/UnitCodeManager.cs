using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCodeManager : MonoBehaviour
{
    private List<Unit> units;
    

    public Unit GetUnitIndex(int index)
    {
        if(index < 0 || index >= units.Count)
        {
            return null;
        }
        return units[index];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageListWIP : MonoBehaviour
{
    [SerializeField]
    List<Unit> units;

    public List<Unit> GetUnits()
    {
        return units;
    }
}

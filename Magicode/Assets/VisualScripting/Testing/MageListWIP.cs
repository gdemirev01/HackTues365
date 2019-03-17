using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageListWIP : MonoBehaviour
{
    [SerializeField]
    List<BaseMinionBehaviour> units;

    public List<BaseMinionBehaviour> GetUnits()
    {
        return units;
    }
}
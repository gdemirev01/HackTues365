using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private UnitUI UIPrefab;

    List<BaseMinionBehaviour> units;

    public static UnitView instance;

    void Start()
    {
        units = FindObjectOfType<MageListWIP>().GetUnits();
        foreach(BaseMinionBehaviour unit in units)
        {
            UnitUI ui = Instantiate<UnitUI>(UIPrefab, content.transform);
            ui.SetName(unit.name);
            ui.SetUnit(unit);
        }
        instance = this;
    }
}

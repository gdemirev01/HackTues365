using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text nameText;

    [SerializeField] // TODO: remove
    private Unit unit;

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public void SetUnitAsActive()
    {
       VisualScriptingManager.instance.SetSelectedUnit(unit);
    }

    public void DublicateActiveScriptToUnit()
    {
        VisualScriptingManager.instance.SetActiveScriptToUnit(unit);
    }

}

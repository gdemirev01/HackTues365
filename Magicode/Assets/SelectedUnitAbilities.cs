using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectedUnitAbilities : MonoBehaviour
{
    [SerializeField]
    private List<SpellIcon> spellIcons;

    public static SelectedUnitAbilities instance;

    void Start()
    {
        instance = this;
        spellIcons = GetComponentsInChildren<SpellIcon>().ToList();
    }
    

    void Update()
    {

    }

    public void SetSpells()
    {

    }
}

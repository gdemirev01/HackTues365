using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{
    public abstract void Activate(params object [] vars);

    [SerializeField]
    private new string name;

    [SerializeField]
    private string description;

    [SerializeField]
    private List<VariableType> expectedTypes;

    protected Spell spell;

    private void Start()
    {
        spell = GetComponentInParent<Spell>();
    }
    

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public List<VariableType> GetExpectedTypes()
    {
        return expectedTypes;
    }
}

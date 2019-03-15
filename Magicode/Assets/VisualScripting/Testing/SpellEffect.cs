using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{
    public abstract void Activate(params object [] vars);
    public abstract VariableType[] GetExpectedVariableTypes();

    [SerializeField]
    private new string name;

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
}

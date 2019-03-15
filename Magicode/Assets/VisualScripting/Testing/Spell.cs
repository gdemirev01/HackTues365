using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spell : MonoBehaviour
{
    [SerializeField]
    private float cooldown;

    [SerializeField]
    private int manaCost;

    [SerializeField]
    private Dictionary<string, SpellEffect> spellBook = new Dictionary<string, SpellEffect>();

    private void Start()
    {
        List<SpellEffect> effects = GetComponentsInChildren<SpellEffect>().ToList();
        foreach(SpellEffect effect in effects)
        {
            Debug.Log("Add spell named " + effect.GetName(), effect);
            spellBook.Add(effect.GetName(), effect);
        }
    }

    public void ActivateEffect(string effect_name, params object [] args)
    {
        SpellEffect effect;
        if(spellBook.TryGetValue(effect_name, out effect))
        {
            effect.Activate(args);
        }
        else
        {
            Debug.LogError("No spell named " + effect_name);
        }
    }
}

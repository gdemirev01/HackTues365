using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spell : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 10f;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private int manaCostPerSpell;

    [SerializeField]
    private Dictionary<string, SpellEffect> spellBook = new Dictionary<string, SpellEffect>();

    private BaseMinionBehaviour minion;

    private void Start()
    {
        List<SpellEffect> effects = GetComponentsInChildren<SpellEffect>().ToList();
        foreach(SpellEffect effect in effects)
        {
            Debug.Log("Add spell named " + effect.GetName(), effect);
            spellBook.Add(effect.GetName(), effect);
        }
    }

    public void SetMinion(BaseMinionBehaviour minion)
    {
        Debug.Log("Set minion", minion);
        this.minion = minion;
    }

    public void ActivateEffect(string effect_name, params object [] args)
    {
        SpellEffect effect;
        if(spellBook.TryGetValue(effect_name, out effect))
        {
            Debug.Log("Activate spell effect " + effect_name);
            effect.Activate(args);
            float manaCost = effect.GetManaCost(args);
            if(minion)
                minion.RemoveMana(manaCost);
            if(minion && !minion.HasMana())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("No spell named " + effect_name);
        }
    }
}

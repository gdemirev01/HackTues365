using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spell : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 3f;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private int manaCostPerSpell;

    [SerializeField]
    private Dictionary<string, SpellEffect> spellBook = new Dictionary<string, SpellEffect>();

    [SerializeField]
    private BaseMinionBehaviour minion;

    private void Start()
    {
        StartCoroutine(destroyAfterLifetime());
        List<SpellEffect> effects = GetComponentsInChildren<SpellEffect>().ToList();
        foreach(SpellEffect effect in effects)
        {
            spellBook.Add(effect.GetName(), effect);
        }
    }

    public void SetMinion(BaseMinionBehaviour minion)
    {
        this.minion = minion;
    }

    public BaseMinionBehaviour GetParent()
    {
        return minion;
    }

    public void ActivateEffect(string effect_name, params object [] args)
    {
        SpellEffect effect;
        if(spellBook.TryGetValue(effect_name, out effect))
        {
            effect.Activate(args);
            float manaCost = effect.GetManaCost(args);
            if (minion)
            {
                minion.RemoveMana(manaCost);
            }
            else
            {
                Debug.Log("no minion to remove mana to");
            }
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
    

    IEnumerator destroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

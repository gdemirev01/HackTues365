using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellBlockGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject spellBlockContainer;

    [SerializeField]
    private FunctionCodeBlock prefabFunctionBlock;

    public static SpellBlockGenerator instance;

    private void Start()
    {
        instance = this;
    }

    public void GenerateSpellsForUnit(Unit unit, int spellIndex)
    {
        List<GameObject> children = new List<GameObject>();
        for(int i = 0; i < spellBlockContainer.transform.childCount; i++)
        {
            children.Add(spellBlockContainer.transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < children.Count; i++)
        {
            Destroy(children[i]);
        }

        List<SpellEffect> effects = unit.GetSpellObject(0).GetComponents<SpellEffect>().ToList();
        foreach(SpellEffect effect in effects)
        {
            Debug.Log("Add block for effect: " + effect.GetName());
            FunctionCodeBlock block = Instantiate(prefabFunctionBlock, spellBlockContainer.transform);
            block.SetTitle(effect.GetName());
            block.SetDescription(effect.GetDescription());
            block.SetParameters(effect.GetExpectedTypes());
        }
    }
}

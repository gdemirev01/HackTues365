using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    public int spell;
    public GameObject playerCamera;

    private void Start()
    {
        int.TryParse(transform.name.Substring(transform.name.LastIndexOf(" ")), out spell);
    }

    public void SetSpell(string spellString)
    {
        var selector = GameObject.FindGameObjectWithTag("4 Spell Selector");
        selector.SetActive(true);
        string[] currentSpells = playerCamera.GetComponent<BaseCameraBehaviour>().spellsEquipped;
        int.TryParse(selector.transform.Find("InputField").Find("Text").GetComponent<Text>().text, out spell);
        transform.Find("UseSpell").gameObject.SetActive(true);
        transform.Find("AddSpell").gameObject.SetActive(false);
        transform.Find("UseSpell").Find("Text").GetComponent<Text>().text += " " + spell;
    }

    public void UseSpell()
    {
        playerCamera.GetComponent<BaseCameraBehaviour>().currentSpell = spell;
    }
}

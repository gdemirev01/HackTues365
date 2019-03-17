using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleInfoPanel : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text name;

    [SerializeField]
    Slider healthSlider;

    [SerializeField]
    Slider manaSlider;

    private BaseMinionBehaviour minion;

    private void Update()
    {
        if (minion)
        {
            healthSlider.value = minion.health;
            manaSlider.value = minion.mana;
        }
    }

    public void SetInfo(BaseMinionBehaviour minion)
    {
        name.text = minion.name;
        this.minion = minion;
    }
}

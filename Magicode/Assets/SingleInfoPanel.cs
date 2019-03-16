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
    Slider slider;

    private MinionInfo minion;

    private void Update()
    {
        slider.value = minion.health;
    }

    public void SetInfo(MinionInfo minion)
    {
        name.text = minion.name;
        this.minion = minion;
    }
}

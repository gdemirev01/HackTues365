using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private float time = 2f;



    public void Show()
    {
        StartCoroutine(popup());
    }

    IEnumerator popup()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}

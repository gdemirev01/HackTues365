using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSpellScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Activate());
    }


    public IEnumerator Activate()
    {
        while(true)
        {
            Debug.Log("Call effect");
            GetComponent<Spell>().ActivateEffect("move_forward", 5, new Vector3(0, 1, 1));
            yield return new WaitForEndOfFrame();
        }
    }
}

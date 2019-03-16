using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTestController : MonoBehaviour
{
    bool hasCollided = false;
    Unit collidedUnit;

    private void Start()
    {
        Debug.Log("Start spell test");
        StartCoroutine(CodeStuffs());
    }

    IEnumerator CodeStuffs()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            GetComponent<Spell>().ActivateEffect("move_forward");
            

            yield return new WaitForEndOfFrame();
            if (hasCollided)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        if (other.GetComponent<Unit>())
        {
            Debug.Log(name + " hit unit!", other.gameObject);
            collidedUnit = other.GetComponent<Unit>();
            hasCollided = true;
        }
    }
}

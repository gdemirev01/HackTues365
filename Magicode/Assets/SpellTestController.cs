using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTestController : MonoBehaviour
{
    bool hasCollided = false;
    BaseMinionBehaviour collidedUnit;

    private void Start()
    {
        StartCoroutine(CodeStuffs());
    }

    IEnumerator CodeStuffs()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            GetComponent<Spell>().ActivateEffect("move_forward", 5f);
            

            yield return new WaitForEndOfFrame();
            if (hasCollided)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseMinionBehaviour>())
        {
            Debug.Log(name + " hit unit!", other.gameObject);
            collidedUnit = other.GetComponent<BaseMinionBehaviour>();
            hasCollided = true;
        }
    }
}

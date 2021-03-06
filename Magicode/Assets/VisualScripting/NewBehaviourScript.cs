﻿using System.Collections;
using UnityEngine;

public class CodeController : MonoBehaviour
{
    bool hasCollided = false;
    Unit collidedUnit;

    private void Start()
    {

        StartCoroutine(CodeStuffs());
    }

    IEnumerator CodeStuffs()
    {
        while (true)
        {
            GetComponent<Spell>().ActivateEffect("move_forward");
            GetComponent<Spell>().ActivateEffect("move_sine");

            yield return new WaitForEndOfFrame();
            if (hasCollided)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Unit>())
        {
            collidedUnit = other.GetComponent<Unit>();
            hasCollided = true;
        }
    }


}
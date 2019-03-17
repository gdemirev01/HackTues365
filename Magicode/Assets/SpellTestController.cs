﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTestController : MonoBehaviour
{
    float Damage = 5;
    float Speed = 5;
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
            yield return new WaitForEndOfFrame();
            if (hasCollided)
            {
                GetComponent<Spell>().ActivateEffect("deal_aoe", 100, 5);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<BaseMinionBehaviour>() &&
            other.GetComponent<BaseMinionBehaviour>() != GetComponent<Spell>().GetParent())
        {
            Debug.Log(name + " hit unit!", other);
            Debug.Log("parent ", GetComponent<Spell>().GetParent());
            collidedUnit = other.GetComponent<BaseMinionBehaviour>();
            collidedUnit.health -= Damage;
            hasCollided = true;
        }
    }


}
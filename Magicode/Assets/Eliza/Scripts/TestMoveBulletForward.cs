﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Mirror;
using System;

public class TestMoveBulletForward : NetworkBehaviour
{
    public float damage = 20;
    public GameObject minionCaster;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 5;
    }

    void OnTriggerEnter(Collider other)
    {
        try
        {
            if(minionCaster != other.gameObject)
            {
                DealDamage(other.gameObject.GetComponent<BaseMinionBehaviour>());
            }
        }
        catch (Exception e)
        {
            Debug.Log(other);
        }
    }

    void DealDamage(BaseMinionBehaviour other)
    {
            other.GetType().GetField("health").SetValue(other,
                (float)other.GetType().GetField("health").GetValue(other) - damage);
        Debug.Log(other.GetType().GetField("health").GetValue(other));
    }
}

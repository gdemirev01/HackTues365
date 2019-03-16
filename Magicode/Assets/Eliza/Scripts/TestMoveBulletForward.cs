using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveBulletForward : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 5;
    }
}

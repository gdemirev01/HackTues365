using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        var slider = transform.Find("Slider").GetComponent<Slider>();
        slider.value = transform.parent.GetComponent<MinionInfo>().health;
        slider.direction = Slider.Direction.RightToLeft;
    }
}

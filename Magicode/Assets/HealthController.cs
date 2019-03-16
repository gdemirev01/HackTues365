using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("Server");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera.transform);
        var slider = transform.Find("Slider").GetComponent<Slider>();
        slider.value = transform.parent.GetComponent<BaseMinionBehaviour>().health;
        slider.direction = Slider.Direction.RightToLeft;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapConntroller : MonoBehaviour
{
    public List<GameObject> minions = new List<GameObject>();
    public List<GameObject> points = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform point in transform)
        {
            if(point.name != "Image")
                points.Add(point.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach(GameObject minion in minions)
        {
            var pos = minion.transform.position;
            Vector2 des = new Vector2(pos.x * (600 / 150), pos.z * (600 / 150));
            points[i].GetComponent<RectTransform>().anchoredPosition = des;
            i++;
        }
    }
}

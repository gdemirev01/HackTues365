using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour
{
    public List<GameObject> selectedMinions = new List<GameObject>();
    private Vector3 startDragboxPos;
    private Vector3 endDragboxPos;
    //public GameObject selector;
    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;

    private bool isDown = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickSelect();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DragSelect();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Move();
        }
    }

    

    void ClickSelect()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (!isDown)
            {
                orgBoxPos = Input.mousePosition;
                isDown = true;
            }
            if (hit.transform.GetComponent<BaseMinionBehaviour>() != null)
            {
                selectedMinions.Add(hit.transform.gameObject);
            }
            startDragboxPos = hit.point;
            endBoxPos = Input.mousePosition;
        }
    }

    void DragSelect()
    {
        isDown = false;
        orgBoxPos = Vector2.zero;
        endBoxPos = Vector2.zero;
        foreach (var minion in selectedMinions)
        {
            minion.transform.Find("Sphere").GetComponent<MeshRenderer>().enabled = false;
        }
        selectedMinions.Clear();
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            endDragboxPos = hit.point;
            var x = Mathf.Abs(startDragboxPos.x - endDragboxPos.x);
            var z = Mathf.Abs(startDragboxPos.z - endDragboxPos.z);
            var List = Physics.OverlapBox((startDragboxPos + endDragboxPos) / 2, new Vector3(x / 2f, 100, z / 2f));
            foreach (Collider c in List)
            {
                if (c.transform.GetComponent<BaseMinionBehaviour>() != null)
                {
                    selectedMinions.Add(c.gameObject);
                    c.transform.Find("Sphere").GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }

    void Move()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (selectedMinions.Count != 0)
            {
                for (int i = -selectedMinions.Count / 2, k = 0; k < selectedMinions.Count; i++, k++)
                {
                    GetComponent<BaseCameraBehaviourOld>().CmdMoveMinion(selectedMinions[k], hit.point + new Vector3((i * 2.5f) % 15, 0, i / 5));
                }
            }
        }
    }

    void OnGUI()
    {
        if (isDown)
        {
            float width = orgBoxPos.x - Input.mousePosition.x;
            float height = (Screen.height - orgBoxPos.y) - (Screen.height - Input.mousePosition.y);
            float x = orgBoxPos.x;
            float y = Screen.height - orgBoxPos.y;

            GUIStyle style = new GUIStyle();
            GUI.Box(new Rect(x, y, -width, -height), "");
        }
    }

    //Debug
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
    //    if (m_Started)
    //    {
    //        var x = Mathf.Abs(startDragboxPos.x - endDragboxPos.x);
    //        var z = Mathf.Abs(startDragboxPos.z - endDragboxPos.z);
    //        Gizmos.DrawWireCube((startDragboxPos + endDragboxPos) / 2, new Vector3(x / 1.2f, 100, z / 1.2f));

    //    }
    //    //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
    //}
}

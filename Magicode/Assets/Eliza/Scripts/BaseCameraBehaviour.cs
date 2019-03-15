using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Mirror;
using UnityEngine.UI;

public class BaseCameraBehaviour : NetworkBehaviour { 
    public float scrollSpeed = 5;
    public float distanceY = 10;
    public float minDistanceY = 5;
    public float maxDistanceY = 15;
    public List<GameObject> selectedMinions = new List<GameObject>();
    public GameObject minionPrefab;
    private Vector3 startDragboxPos;
    private Vector3 endDragboxPos;  
    public GameObject selector;
    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;
    public Texture TextureForRect;
    private bool isDown = false;
    bool m_Started;
    [SyncVar] public int playerNumber;
    [SyncVar] public int lastPlayerNumber = 1;
    public Text logger;

    void Start()
    {
        logger = GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        if(isLocalPlayer)
        {
            gameObject.GetComponent<Camera>().enabled = true;
        }
        if(isServer)
        {
            playerNumber = lastPlayerNumber;
            lastPlayerNumber++;
        }
        m_Started = true;
    }

    public override void OnStartLocalPlayer()
    {
        CmdGenerateMinions(new Vector3(transform.position.x, 0, transform.position.z));
        if(isServer)
        {
            transform.tag = "Server";
        }
    }

    [Command]
    void CmdAssignPlayer()
    {

    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        logger.text = lastPlayerNumber.ToString();
        if (Input.GetMouseButtonDown(0))
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
        else if (Input.GetMouseButtonUp(0))
        {
            isDown = false;
            orgBoxPos = Vector2.zero;
            endBoxPos = Vector2.zero;
            foreach(var minion in selectedMinions)
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

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (selectedMinions.Count != 0)
                {
                    for (int i = -selectedMinions.Count / 2, k = 0; k < selectedMinions.Count; i++, k++)
                    {
                        CmdMoveMinion(selectedMinions[k], hit.point + new Vector3((i * 2.5f) % 15, 0, i / 5));                    
                    }
                }
            }
        }
    }

    [Command]
    private void CmdMoveMinion(GameObject minion, Vector3 destination)
    {
        if(!isServer)
        {
            return;
        }
        minion.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(destination);
    }

    [Command]
    void CmdGenerateMinions(Vector3 position)
    {
        if (isServer)
        {
            List<Vector3> positions = new List<Vector3>();
            for (int i = -1; i <= 1; i++)
            {
                positions.Add(new Vector3(position.x - 3, 2, position.z + i * 3));
                positions.Add(new Vector3(position.x + 3, 2, position.z + i * 3));
            }
            foreach (Vector3 pos in positions)
            {
                var spawned = Instantiate(minionPrefab, pos, Quaternion.identity);
                NetworkServer.Spawn(spawned);
                spawned.GetComponent<BaseMinionBehaviour>().Player = lastPlayerNumber;
                Color color;
                EStatic.playerColors.TryGetValue(lastPlayerNumber, out color);
                spawned.transform.Find("Mage").Find("mage_mesh").Find("Mage").
                    GetComponent<SkinnedMeshRenderer>().materials.ElementAt(1).color = color;
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
    
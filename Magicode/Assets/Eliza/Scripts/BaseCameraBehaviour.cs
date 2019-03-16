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
    bool hasAssigned = false;
    public Material outlineMaterial;

    void ShootSpells(string assemblyName, string className)
    {
        foreach(var minion in selectedMinions)
        {
            CmdShoot(minion, assemblyName, className);
        }
    }

    void Start()
    {
        if(isLocalPlayer)
        {
            gameObject.GetComponent<Camera>().enabled = true;
        }
        m_Started = true;
    }

    public List<GameObject> GetSelected()
    {
        return selectedMinions;
    }

    public override void OnStartLocalPlayer()
    {
        CmdGenerateMinions(new Vector3(transform.position.x, 0, transform.position.z));
        if(isServer)
        {
            transform.tag = "Server";
        }
        StartCoroutine("AssignPlayers");
    }

    // this needs to be here because only LocalPlayerAuth can send [Cmd]
    [Command]
    public void CmdShoot(GameObject minion, string assemblyName, string behaviourName)
    {
        var spawned = Instantiate(minion.GetComponent<BaseMinionBehaviour>().bulletPrefab,
            minion.transform.position + new Vector3(0, 1, 0), minion.transform.rotation);
        BaseCompiler.LoadAssembly(spawned, assemblyName, behaviourName);
        NetworkServer.Spawn(spawned);
    }

    // don't bully me please... it works... its bad but it works...
    /*
     * gonna do a better implementation if there is any time left
     * found the issue, basically i was trying to access the minions while they werent initialized
     * so now i rescan for them near the camera every 0.X seconds until i find them and then init
     * */
    IEnumerator AssignPlayers()
    {
        while(!hasAssigned)
        {
            var list = Physics.OverlapBox(transform.position, new Vector3(6, 20, 6));
            if (list.Length > 6)
            {
                int i = 1;
                foreach (var obj in list)
                {
                    if (obj.GetComponent<BaseMinionBehaviour>() != null)
                    {
                        obj.transform.Find("Mage").Find("mage_mesh").Find("Mage").GetComponent<SkinnedMeshRenderer>().
                            materials.ElementAt(1).color = Color.blue;
                        obj.GetComponent<BaseMinionBehaviour>().isAllied = true;
                        obj.name = "Minion " + i;
                        GameObject.Find("MinionsInfo").transform.Find("MiniMap").GetComponent<MiniMapConntroller>().minions.Add(obj.gameObject);
                        GetComponent<GameStateManager>().minions.Add(obj.gameObject);
                        i++;
                    }
                }
                hasAssigned = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        if(Input.GetButtonDown("Jump"))
        {
            ShootSpells("test.dll", "SpellTestController");
        }
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
                    if(hit.transform.GetComponent<BaseMinionBehaviour>().isAllied == true)
                    {
                        selectedMinions.Add(hit.transform.gameObject);
                    }   
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
            foreach (GameObject obj in selectedMinions)
            {
                var rend = obj.transform.Find("Mage").transform.Find("mage_mesh").transform.Find("Mage").GetComponent<SkinnedMeshRenderer>();
                foreach (Material mat in rend.materials)
                {
                    mat.DisableKeyword("_EMISSION");
                }
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
                        if (c.transform.GetComponent<BaseMinionBehaviour>().isAllied == true)
                        {
                            selectedMinions.Add(c.gameObject);
                            var rend = c.transform.Find("Mage").transform.Find("mage_mesh").transform.Find("Mage").GetComponent<SkinnedMeshRenderer>();
                            foreach(Material mat in rend.materials) {
                                mat.SetColor("_EmissionColor", mat.color);
                                mat.EnableKeyword("_EMISSION");
                            }
                        }
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
        var minionsInfo = GameObject.Find("MinionsInfo");
        minionsInfo.GetComponent<MinionsInGameUi>().minions = selectedMinions;
        List<BaseMinionBehaviour> selectedMinionsInfo = new List<BaseMinionBehaviour>();
        foreach(GameObject minion in selectedMinions)
        {
            selectedMinionsInfo.Add(minion.GetComponent<BaseMinionBehaviour>());
        }
        FindObjectOfType<MinionPanelInfo>().SetMinions(selectedMinionsInfo);
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
            int minionNumber = 1;
            foreach (Vector3 pos in positions)
            {
                var spawned = Instantiate(minionPrefab, pos, Quaternion.identity);
                spawned.name = "Minion " + minionNumber;
                NetworkServer.Spawn(spawned);
                minionNumber++;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
        {
            var x = Mathf.Abs(startDragboxPos.x - endDragboxPos.x);
            var z = Mathf.Abs(startDragboxPos.z - endDragboxPos.z);
            Gizmos.DrawWireCube((startDragboxPos + endDragboxPos) / 2, new Vector3(x / 1.2f, 100, z / 1.2f));

        }
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
    }


    private void AddOutlineMaterial(SkinnedMeshRenderer rend)
    {
        if (rend != null)
        {
            Debug.Log(rend.gameObject.name);

            Material[] mats = rend.materials;
            List<Material> materials = mats.ToList();
            materials.Add(outlineMaterial);

            rend.materials = materials.ToArray();
        }
    }
}
    
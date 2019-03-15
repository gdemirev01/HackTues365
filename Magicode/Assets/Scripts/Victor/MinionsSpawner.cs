using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MinionsSpawner : NetworkBehaviour
{

    public GameObject minionPrefab;
    bool m_Started;

    public override void OnStartLocalPlayer()
    {
        Debug.Log("Spawn");
        CmdGenerateMinions(new Vector3(transform.position.x, 0, transform.position.z));
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Local");
            gameObject.AddComponent<Camera>();
        }
        m_Started = true;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            Debug.Log(isServer);
            return;
        }
    }

    [Command]
    void CmdGenerateMinions(Vector3 position)
    {
        if (isServer)
        {
            List<Vector3> positions = new List<Vector3>();
            List<GameObject> minions = new List<GameObject>();
            for (int i = -1; i <= 1; i++)
            {
                positions.Add(new Vector3(position.x - 3, 2, position.z + i * 3));
                positions.Add(new Vector3(position.x + 3, 2, position.z + i * 3));
            }
            foreach (Vector3 pos in positions)
            {
                var spawned = Instantiate(minionPrefab, pos, Quaternion.identity);
                NetworkServer.Spawn(spawned);
                minions.Add(spawned);
            }
        }
    }
}

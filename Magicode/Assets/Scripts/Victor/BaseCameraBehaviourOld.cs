using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Mirror;

public class BaseCameraBehaviourOld : NetworkBehaviour {

    bool m_Started;
    void Start()
    {
        if(isLocalPlayer)
        {
            gameObject.AddComponent<Camera>();
        }
        m_Started = true;
    }


    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }

    [Command]
    public void CmdMoveMinion(GameObject minion, Vector3 destination)
    {
        if(!isServer)
        {
            return;
        }
        minion.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(destination);
    }
}
    
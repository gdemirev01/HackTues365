using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class GameStateManager : NetworkBehaviour
{
    public List<GameObject> minions = new List<GameObject>();
    // Update is called once per frame
    void Update()
    {
        if(BaseMinionBehaviour.minionsCounter <= 0)
        {
            Debug.Log("bye");
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class GameStateManager : NetworkBehaviour
{
    public List<GameObject> minions = new List<GameObject>();

    private bool hasLost = false;

    void Update()
    {
        if(hasLost)
        {
            return;
        }
        Debug.Log("Minion count: " + BaseMinionBehaviour.minionsCounter);
        if(BaseMinionBehaviour.minionsCounter <= 0)
        {
            Debug.Log("Has lost!");
            hasLost = true;
            StartCoroutine(loadMenu());
        }
    }

    IEnumerator loadMenu()
    {
        PopupManager.instance.ShowError();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

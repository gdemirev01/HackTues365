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
            int loseCount = 0;
            int winCount = 0;
            foreach(BaseMinionBehaviour behaviour in FindObjectsOfType<BaseMinionBehaviour>())
            {
                if(behaviour == null)
                {
                    continue;
                }
                if(behaviour.isAllied)
                {
                    loseCount++;
                }
                else
                {
                    winCount++;
                }
            }
            if (loseCount == 0 || winCount == 0) {
                if (loseCount == 0)
                {
                    PopupManager.instance.ShowError();
                }

                if (winCount == 0)
                {
                    PopupManager.instance.ShowNotice();
                }
                hasLost = true;
                StartCoroutine(loadMenu());
            }
        }
    }

    IEnumerator loadMenu()
    {
        
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

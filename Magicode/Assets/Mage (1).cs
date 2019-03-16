using System.Collections;
using UnityEngine;

public class CodeStuff : MonoBehaviour
{
	bool hasCollided = false;
	Unit collidedUnit;

	private void Start()
    	{
        		StartCoroutine(CodeStuffs());
    	}

	IEnumerator CodeStuffs() {
		while(true) {
GetComponent<Spell>().ActivateEffect("move_forward");
	
			yield return new WaitForEndOfFrame();
			if(hasCollided) {
				Destroy(gameObject);
			}
		}
	}
	private void OnTriggerEnter(Collider other)
    	{		if(other.GetComponent<Unit>())
        		{
            			collidedUnit = other.GetComponent<Unit>();
			hasCollided = true;
        		}
    	}
	
	
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class DialougeTrigger : MonoBehaviour
{
	public string name;
    public Dialouge[] dialougeList;
	public bool hasBeenSpoken = false;
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(dialougeList[0].objective.Contains("Destroy all Generators"))
			{
				FindObjectOfType<CountGenerator>().addObjective();
			}

			if(dialougeList[0].objective.Contains("Treasure Chest"))
			{
				FindObjectOfType<treasure>().hasFoundPower = true;
			}

			FindObjectOfType<DialogueManager>().Display(this);
		}		
	}
}

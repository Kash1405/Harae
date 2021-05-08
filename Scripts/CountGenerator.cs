using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountGenerator : MonoBehaviour
{
	public int numberOfGenerators;
	public Text objectiveUI;
	
    // Start is called before the first frame update
    void Start()
    {
        numberOfGenerators = 5;
    }

    public void generatorDestroyed()
	{
		numberOfGenerators -= 1;
		if(numberOfGenerators==0)
		{
			objectiveUI.text = "Completed: Destroy All Generators.";
			objectiveUI.color = Color.green;
		}
		objectiveUI.text = "Destroy All Generators.\nRemaining: "+numberOfGenerators;
	}
	
	public void addObjective()
	{
		objectiveUI.text = "Destroy All Generators.\nRemaining: "+numberOfGenerators;
	}
}

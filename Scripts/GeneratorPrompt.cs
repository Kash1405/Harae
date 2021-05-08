using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GeneratorPrompt : MonoBehaviour
{
	public Text prompt;
	public Transform target;
	public float takeDamageRadius = 5f;
	public Transform[] generatorPanelsArray = new Transform[5];
	List<Transform> generatorPanels = new List<Transform>();
	public GameObject explosion;
	
	GameObject nearGenerator;
	Transform nearPanel;
	
    // Start is called before the first frame update
    void Start()
    {
		foreach(Transform panel in generatorPanelsArray)
		{
			generatorPanels.Add(panel);
		}
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
		//Just more than the takeDamageRadius
		bool inRange = false;
		
        foreach(Transform panel in generatorPanels)
		{
			float distance = Vector3.Distance(target.position, panel.position);
			if(distance <= takeDamageRadius)
			{
				inRange = true;
				nearGenerator = panel.parent.gameObject;
				nearPanel = panel;
			}
		}
		
		if(inRange)
		{
			prompt.text = "Press 'E' to destroy the Generator";
			if(Input.GetKey("e"))
			{
				generatorPanels.Remove(nearPanel);
				Instantiate(explosion, nearGenerator.transform.position, Quaternion.identity);
				FindObjectOfType<CountGenerator>().generatorDestroyed();
				nearGenerator.GetComponent<GeneratorHealth>().takeDamage();
				nearPanel = null;
				nearGenerator=null;
			}
		}
		else{
			prompt.text="";
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeneratorHealth : MonoBehaviour
{
	public float health;
	
	public Text prompt;
	
	public Transform target;
	public Transform panel;
	public float damageTaken = 100f;
	public float takeDamageRadius = 5f;
	public float timeSinceDamageTaken;
	public float damageTakenCooldown;
	
	public GameObject explosion;
	
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
		target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
	//	if(health<=0)return;
     //   CheckDamage(Time.deltaTime);
    //}
	
	void CheckDamage(float timeElapsed)
	{		
		float distance = Vector3.Distance(target.position, panel.position);
		Debug.Log(distance+"\tvs\t"+takeDamageRadius);
		if (distance <= takeDamageRadius)
		{
			Debug.Log("wtf");
			prompt.text = "Press 'E' to destroy the Generator";
			if(Input.GetKey("e"))
			{
				takeDamage();
				timeSinceDamageTaken = 0;
			}
			else{}
		}
		else{
			
			prompt.text="";
		}
	}
	
	public void takeDamage()
	{
		
		prompt.text = "";
		FindObjectOfType<AudioManager>().Play("boom");
		health -= damageTaken;
		if(health <= 0)
		{
			Debug.Log("Explosion");
			
			//Explosion Effect
			
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100;
	public float currentHealth = 100;
	float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<HealthBar>().SetMaxHealth((int)(health));
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth!=health)
		{
			currentHealth = Mathf.Lerp(currentHealth, health, t);
			t+=Time.deltaTime;
			FindObjectOfType<HealthBar>().SetHealth((int)(currentHealth));
		}
    }
	
	public void takeDamage(float damageValue)
	{
		health -= damageValue;
		t = 0;
		
		Debug.Log("Player health: " + health);
		if (health <= 0)
		{
			Debug.Log("Player Died");
		}
	}
}

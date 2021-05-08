using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class lookAtEnemy : MonoBehaviour
{ 
	//public CinemachineVirtualCamera	cam;
	GameObject[] enemies;
	bool toggle = false;
	public Transform closestEnemy;
	
	public Transform whereWeWantToLook;
	
	public GameObject player; 
	
	public float turnSmoothTime = 0.1f;
	private float turnSmoothVelocity;
	
    // Start is called before the first frame update
    void Start()
    {
		
        
		gameObject.GetComponent<CinemachineFreeLook>().LookAt = whereWeWantToLook;
    }

    // Update is called once per frame
    void Update()
    {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if(enemies==null)return;
        bool tPressed = Input.GetKeyDown("t");
		
		if(tPressed)
		{
			toggle=!toggle;
		}
		
		if(toggle)
		{
			closestEnemy = getClosestEnemy();
			gameObject.GetComponent<CinemachineFreeLook>().LookAt = closestEnemy.transform;
			
			Vector3 direction = closestEnemy.transform.position - player.transform.position;
			
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;// + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
			
		}
		else{
			gameObject.GetComponent<CinemachineFreeLook>().LookAt = whereWeWantToLook;
		}
    }
	
	public Transform getClosestEnemy()
	{
		float closestDistance = Mathf.Infinity;
		Transform enemyTranform = null;
		
		foreach(GameObject enemy in enemies)
		{
			float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
			if(currentDistance < closestDistance)
			{
				closestDistance = currentDistance;
				enemyTranform = enemy.transform;
			}
		}
		return enemyTranform;
	}
}

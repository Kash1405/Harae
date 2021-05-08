using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	public Transform target;
	public float lookRadius = 15f;
	public float stopRadius = 2.5f;
	public float takeDamageRadius = 4.5f;
	
	public float health = 100f;
	public float damageTakenFromSwordAttack = 40;
	public float damageTakenCooldown = 1;
	private float timeSinceDamageTaken = 0;
	
	public float attackRadius = 4.5f;
	public float damageDealt = 20;
	public float attackCooldown;
	private float timeSinceAttacked = 0f;
	
    public CharacterController controller;
	public Transform groundCheck;
	public float groundDistance;
	public LayerMask groundMask;
	
	//public Animator animator;
	
	public float playerSpeed = 5.2f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
	public float turnSmoothTime = 0.1f;
	//public Transform cam;
	
    private Vector3 playerVelocity;
    private bool isGrounded;
	private float turnSmoothVelocity;
	
	private int jumpCount = 0;
	private int jumpLimit = 3;
	
	void Awake()
	{
		attackCooldown = Random.Range(1.0f,3.0f);
	}
	void Start()
	{
		target = PlayerManager.instance.player.transform;
	}
	
    void Update()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		
		if(isGrounded && playerVelocity.y < 0)
		{
			playerVelocity.y = -2f;
		}
		
        //float horizontal = Input.GetAxisRaw("Horizontal");
		//float vertical = Input.GetAxisRaw("Vertical");
		
		CheckAttack(Time.deltaTime);
		CheckDamage(Time.deltaTime);

		Vector3 direction = GetMove().normalized; //new Vector3(horizontal, 0f, vertical).normalized;
		
		if(direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			
			float speed = playerSpeed;
			//if(animator.GetBool("isRunning"))
			//{
			//	speed *= 2.4f;
			//}
			controller.Move(moveDir.normalized * speed * Time.deltaTime);
		}
		
		//if(Input.GetButtonDown("Jump") && (isGrounded || jumpCount < jumpLimit))
		//{
		//	playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityValue);
		//	animator.SetBool("isJumping",true);
		//	jumpCount += 1;
		//	Debug.Log("Jumped");
		//}
		
		//if(isGrounded) jumpCount = 0;
		
		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime * 0.5f);
    }

    Vector3 GetMove()
    {
        float distance = Vector3.Distance(target.position, transform.position);
		if(distance <= lookRadius && distance > stopRadius)
		{
			return target.position - transform.position;
		}
		else
		{
			return new Vector3(0,0,0);
		}
    }
	
	void CheckDamage(float timeElapsed)
	{
		if(timeSinceDamageTaken < damageTakenCooldown)
		{
			timeSinceDamageTaken += timeElapsed;
			return;
		}
		
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance <= takeDamageRadius && Input.GetMouseButtonDown(0))
		{
			takeDamage();
			timeSinceDamageTaken = 0;
		}
	}
	
	void CheckAttack(float timeElapsed)
	{
		if(timeSinceAttacked < attackCooldown)
		{
			timeSinceAttacked += timeElapsed;
			return;
		}
		
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance <= attackRadius)
		{
			PlayerManager.instance.player.GetComponent<PlayerHealth>().takeDamage(damageDealt);
			timeSinceAttacked = 0;
		}
	}
	
	public void takeDamage()
	{
		health -= damageTakenFromSwordAttack;
		Debug.Log("health: " + health);
		if (health <= 0)
		{
			Debug.Log("Enemy died");
			Destroy(this.gameObject);
		}
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}

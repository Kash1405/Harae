using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
	Animator animator;
	bool isIdle = true;
	bool isWalking;
	bool isRunning;
	bool isAttacking;
	bool isJumping;
	
	public Rigidbody powerAttack;
	public float powerAttackSpeed = 20f;
	public float dodgeCoolDown = 4f;
	public float dodgeTimeElapsed = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
		isRunning = animator.GetBool("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
		isIdle = animator.GetBool("isIdle");
		isWalking = animator.GetBool("isWalking");
		isRunning = animator.GetBool("isRunning");
		isAttacking = animator.GetBool("isAttacking");
		isJumping = animator.GetBool("isJumping");
		
		animator.SetBool("isAttacking", false);
		animator.SetBool("isJumping", false);
		
		bool wPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
		bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
		bool mouseClicked = Input.GetMouseButtonDown(0);
		bool rightMouseClicked = Input.GetMouseButtonDown(1);
		bool qPressed = Input.GetKey("q");
		bool spacePressed = Input.GetButtonDown("Jump");
		
		if(!isAttacking && (mouseClicked || rightMouseClicked))
		{
			animator.SetBool("isAttacking",true);
			if(rightMouseClicked && FindObjectOfType<treasure>().hasFoundPower)
			{
				Rigidbody p =Instantiate(powerAttack, transform.position, Quaternion.identity);
				p.velocity = transform.forward * powerAttackSpeed;
			}
		}
		
		
		//if (!isAttacking && mouseClicked)
		//{
		//	animator.SetBool("isAttacking", true);
		//}
		
		//if (!isJumping && spacePressed)
		//{
		//	animator.SetBool("isJumping", true);
		//}
		
		if (!isWalking && (wPressed || qPressed) && !shiftPressed)
		{
			animator.SetBool("isWalking",true);
			animator.SetBool("isIdle",false);
			
			//FindObjectOfType<AudioManager>().Play("Walking");
			
			if(qPressed)
			{
				if(dodgeTimeElapsed < dodgeCoolDown)
				{
					FindObjectOfType<AudioManager>().Play("Dodge");
					dodgeTimeElapsed = 0;
				}
				else{
					dodgeTimeElapsed += Time.deltaTime;
				}
			}
			else{
				FindObjectOfType<AudioManager>().Play("Walking");
			}
		}
		else if (isWalking && !wPressed)
		{
			animator.SetBool("isWalking",false);
			animator.SetBool("isIdle",true);
			FindObjectOfType<AudioManager>().Stop("Walking");
		}
		
        if (!isRunning && wPressed && shiftPressed)
		{
			animator.SetBool("isRunning",true);
			animator.SetBool("isWalking",false);
			animator.SetBool("isIdle",false);
			
			FindObjectOfType<AudioManager>().Play("Running");
			FindObjectOfType<AudioManager>().Stop("Walking");
		}
		else if (isRunning && !shiftPressed)
		{
			animator.SetBool("isRunning",false);
			animator.SetBool("isWalking",true);
			
			FindObjectOfType<AudioManager>().Play("Walking");
			FindObjectOfType<AudioManager>().Stop("Running");			
			
			
		}
		else if (isRunning && !wPressed)
		{
			animator.SetBool("isRunning",false);
			animator.SetBool("isIdle",true);

			FindObjectOfType<AudioManager>().Stop("Running");
		}
		
		
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;
	public Transform groundCheck;
	public float groundDistance;
	public LayerMask groundMask;
	
	public Animator animator;
	
	public float playerSpeed = 12.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
	public float turnSmoothTime = 0.1f;
	public Transform cam;
	
    private Vector3 playerVelocity;
    private bool isGrounded;
	private float turnSmoothVelocity;
	
	private int jumpCount = 0;
	private int jumpLimit = 3;
	
    public float dodgeCoolDown = 4f;
	public float dodgeTimeElapsed = 0f;
	public float dodgeSpeed = 10;
	public GameObject smoke;

    private void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
        //controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		
		if(isGrounded && playerVelocity.y < 0)
		{
			playerVelocity.y = -2f;
		}
		
        float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		
		bool qPressed = Input.GetKey("q");
		
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
		
		if(qPressed)
		{
			if(dodgeTimeElapsed > dodgeCoolDown)
			{
				Vector3 moveDir = Quaternion.Euler(0f, cam.eulerAngles.y , 0f) * Vector3.forward * -1;
				Instantiate(smoke, transform.position, transform.rotation);
				controller.Move(moveDir.normalized * dodgeSpeed);
				dodgeTimeElapsed = 0;
			}
		}
		else
		{
				dodgeTimeElapsed += Time.deltaTime;
		}
		
		if(direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			
			float speed = playerSpeed;
			if(animator.GetBool("isRunning"))
			{
				speed *= 2.4f;
				
			}
			controller.Move(moveDir.normalized * speed * Time.deltaTime);
		}
		
		if(Input.GetButtonDown("Jump") && (isGrounded || jumpCount < jumpLimit))
		{
			playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityValue);
			animator.SetBool("isJumping",true);
			jumpCount += 1;
			Debug.Log("Jumped");
		}
		
		if(isGrounded) jumpCount = 0;
		
		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime * 0.5f);
    }
}

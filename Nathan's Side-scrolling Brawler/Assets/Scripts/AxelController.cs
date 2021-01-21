using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxelController : MonoBehaviour
{
	public float horizontalSpeed;
	public float verticalSpeed;

	private Rigidbody2D rigidBody2D;
	private Animator animator;
	
	private float horizontalInput;
	private float verticalInput;
	private float lookDirection;
	private bool acceptingInput;
	
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		lookDirection = 1.0f;
		acceptingInput = true;
    }

    // Update is called once per frame
    void Update()
    {
		if(acceptingInput)
		{
        	horizontalInput = Input.GetAxis("Horizontal");
			verticalInput = Input.GetAxis("Vertical");
		
			if(horizontalInput > 0)
				lookDirection = 1.0f;
			if(horizontalInput < 0)
				lookDirection = -1.0f;

			animator.SetFloat("Look Direction", lookDirection);
			animator.SetBool("Moving", !Mathf.Approximately(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput), 0f));

			if(Input.GetKeyDown(KeyCode.Z))
			{
				animator.SetTrigger("Punch");
				acceptingInput = false;
			}
		}
	}
	
	void FixedUpdate()
	{
		Vector2 newPosition = rigidBody2D.position;
		newPosition.x += horizontalInput * horizontalSpeed * Time.deltaTime;
		newPosition.y += verticalInput * verticalSpeed * Time.deltaTime;

		rigidBody2D.MovePosition(newPosition);
	}

	public void SetAcceptingInput(bool acceptingInput)
	{
		this.acceptingInput = acceptingInput;
	}

	public void OverrideInput(float horizontalInput, float verticalInput)
	{
		this.horizontalInput = horizontalInput;
		this.verticalInput = verticalInput;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.tag;

		if(tag.Equals("EnemyHitBox"))
		{
			
		}
	}
}
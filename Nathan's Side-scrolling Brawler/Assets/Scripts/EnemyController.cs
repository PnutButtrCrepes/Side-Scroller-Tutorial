using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject axel;

	public float horizontalSpeed;
	public float verticalSpeed;
    public float horizontalPlayerPadding;
    public float verticalPlayerPadding;

	private EnemyBehavior enemyBehavior;
	private Vector2 target;
	private bool surroundPositionSecured;

	private Rigidbody2D rigidBody2D;
	private Animator animator;
	
	private float horizontalMovement;
	private float verticalMovement;
	private float lookDirection;
	
    // Start is called before the first frame update
    void Start()
    {
		enemyBehavior = EnemyBehavior.SURROUND;
		surroundPositionSecured = false;
        rigidBody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		lookDirection = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 directionFromPlayer = rigidBody2D.position - axel.GetComponent<Rigidbody2D>().position;
		float angle = Mathf.Atan2(directionFromPlayer.y, directionFromPlayer.x) * Mathf.Rad2Deg;
		float angle360 = (angle < 0) ? (360 + angle) : angle;

		switch(enemyBehavior)
		{
			case EnemyBehavior.RUSH:
				target = axel.GetComponent<Rigidbody2D>().position;
				break;
			case EnemyBehavior.SURROUND:
				if(surroundPositionSecured)
					break;
				//float angleIncrement = (360f)/EnemyManager.surroundingEnemies;
				//float closestAngle = FindClosestMultiple(angle360, angleIncrement);
				//int index = Mathf.RoundToInt(closestAngle / angleIncrement);
				//while(EnemyManager.isSurroundPositionOccupied(index))
				//{
					//index++;
				//}
				//EnemyManager.isSurroundPositionOccupied(index) = true;
				target = axel.GetComponent<Rigidbody2D>().position + Random.insideUnitCircle.normalized * 2;
				surroundPositionSecured = true;
				break;
			case EnemyBehavior.DEFEND:
				target = rigidBody2D.position - axel.GetComponent<Rigidbody2D>().position;
				target.Normalize();
				target = target * (horizontalPlayerPadding * 2);
				break;
			case EnemyBehavior.IDLE_STILL:
				target = rigidBody2D.position;
				break;
			case EnemyBehavior.IDLE_RANDOM:
				target = rigidBody2D.position + Random.insideUnitCircle * 5;
				break;
			default:
				break;
		}

        if(target.x + 1 < rigidBody2D.position.x)
            horizontalMovement = -1f;
        else if(target.x - 1 > rigidBody2D.position.x)
            horizontalMovement = 1f;
        else
            horizontalMovement = 0f;

        if(target.y + 1 < rigidBody2D.position.y)
            verticalMovement = -1f;
        else if(target.y - 1 > rigidBody2D.position.y)
            verticalMovement = 1f;
        else
            verticalMovement = 0f;

		if(horizontalMovement > 0f)
			lookDirection = 1.0f;
		if(horizontalMovement < 0f)
			lookDirection = -1.0f;

		animator.SetFloat("Look Direction", lookDirection);
		animator.SetBool("Moving", !Mathf.Approximately(Mathf.Abs(horizontalMovement) + Mathf.Abs(verticalMovement), 0f));

		switch(enemyBehavior)
		{
			case EnemyBehavior.RUSH:
				target = axel.GetComponent<Rigidbody2D>().position;
				break;
			case EnemyBehavior.SURROUND:
				break;
			case EnemyBehavior.DEFEND:
				if((rigidBody2D.position - axel.GetComponent<Rigidbody2D>().position).magnitude < horizontalPlayerPadding)
				{
					enemyBehavior = EnemyBehavior.RUSH;
				}
				break;
			case EnemyBehavior.IDLE_STILL:
				target = rigidBody2D.position;
				break;
			case EnemyBehavior.IDLE_RANDOM:
				target = rigidBody2D.position + Random.insideUnitCircle;
				break;
			default:
				break;
		}
	}
	
	void FixedUpdate()
	{
		Vector2 newPosition = rigidBody2D.position;
		newPosition.x += horizontalMovement * horizontalSpeed * Time.deltaTime;
		newPosition.y += verticalMovement * verticalSpeed * Time.deltaTime;

		rigidBody2D.MovePosition(newPosition);
	}

	float FindClosestMultiple(float value, float a)
	{
		float quotient = value / a;
		float lower = Mathf.FloorToInt(quotient) * a;
		float higher = lower + a;
		return FindCloserFloat(value, lower, higher);
	}

	float FindCloserFloat(float value, float a, float b)
	{
		if(Mathf.Abs(value - a) > Mathf.Abs(value - b))
			return b;
		else
			return a;
	}

    void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.tag;

		switch(enemyBehavior)
		{
			case EnemyBehavior.RUSH:
				
				break;
			case EnemyBehavior.SURROUND:
				break;
			case EnemyBehavior.DEFEND:
				break;
			case EnemyBehavior.IDLE_STILL:
				break;
			case EnemyBehavior.IDLE_RANDOM:
				break;
			default:
				break;
		}

		if(tag.Equals("PlayerHitBox"))
		{
			animator.SetTrigger("Hit");
		}
	}
}

public enum EnemyBehavior
{
	RUSH,
	SURROUND,
	DEFEND,
	IDLE_STILL,
	IDLE_RANDOM,
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
	public LayerMask raycastMask;
	public float rayCastLength;
	public float attackDistance;
	public float moveSpeed;
	public float timer;//攻擊間隙
	public int health = 100;
	public Transform rightLimit;
	public Transform leftLimit;
	#endregion

	#region Private Variables
	private RaycastHit2D hit;
	private Transform target;
	private Animator anim;
	private float distance;//與玩家間的距離
	private bool attackMode;
	private bool inRange;//玩家是否在範圍內
	private bool cooling;//是不是在準備下一次攻擊中
	private float intTimer;
	#endregion 

	//public GameObject deathEffect;
	private void Awake()
    {
		SelectTarget();
		intTimer = timer;
		anim = GetComponent<Animator>();
    }
	private void Update()
    {
        if (!attackMode)
        {
			Move();
        }

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
			SelectTarget();
        }

        if (inRange)
        {
			hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
			RaycastDebugger();
        }

        //當玩家被偵測
        if (hit.collider != null)
        {
			EnemyLogic();
        }
		else if (hit.collider == null)
        {
			inRange = false;
        }

		if(inRange == false)
        {
			StopAttack();
        }
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
			target = trig.transform;
			inRange = true;
			Flip();
        }
    }
	void EnemyLogic()
    {
		distance = Vector2.Distance(transform.position, target.position);

		if (distance > attackDistance)
		{
			StopAttack();
		}
		else if (attackDistance >= distance && cooling == false)
        {
			Attack();
        }

        if (cooling)
        {
			anim.SetBool("Attack", false);
        }
    }
	void Move()
    {
		anim.SetBool("canWalk", true);
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Skel_attack"))
        {
			Vector2 targetPosition = new Vector2(target.position.x, target.transform.position.y);

			transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
		}
    }
	void Attack()
    {
		timer = intTimer;
		attackMode = true;

		anim.SetBool("canWalk", false);
		anim.SetBool("Attack", true);
    }
	void StopAttack()
    {
		cooling = false;
		attackMode = false;
		anim.SetBool("Attack", false);
    }
	void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
			Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
		else if (attackDistance > distance)
        {
			Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }
	public void TriggerCooling()
    {
		cooling = true;
    }
	private bool InsideofLimits()
    {
		return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
	private void SelectTarget()
	{
		float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
		float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

		if (distanceToLeft > distanceToRight)
		{
			target = leftLimit;
		}
		else
		{
			target = rightLimit;
		}

		Flip();
	}
	private void Flip()
    {
		Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
			rotation.y = 180f;
        }
        else
        {
			rotation.y = 0f;
        }
    }
	public void TakeDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}

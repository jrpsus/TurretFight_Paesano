using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{

    //This is the component that controls the enemy's movement

    //The animator controlling the enemy animations
    private Animator animator;
    private Rigidbody2D rb;
    public GameObject powerup;
    public float moveSpeed;
    public float projectileSpeed = 10f;
    public float health;
    public GameObject particle;
    public LevelManagerScript lvl;
    public Transform firePoint;
    public GameObject projectile;

    public enum State { Idle = 0, Patrolling = 1, Attacking = 2, Searching = 3 };
    //The current state of the enemy, which determines their behavior at that time
    private State currentState = State.Idle;

    [Header("Idle")]
    public float idleTime = 1;
    private float idleTimeCounter = 0;

    [Header("Patrol")]
    private Vector2 patrollingDestination;
    public float minDestinationDistance;
    public float patrolRange = 10;

    [Header("Searching")]
    private Vector2 searchingDestination;
    public Vector2 targetpos;

    [Header("Combat")]
    //The transform this enemy is gonna chase
    public Transform target;
    //How many seconds should pass between each nav update
    public float updateFrequency = 0.1f;
    //The counter keeping track of time since the last nav update
    private float updateCounter = 0;
    public float shootFrequency;
    private float shootCounter = 0;
    //
    public float minCombatDistance = 4;


    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        lvl = GameObject.Find("LevelManager").GetComponent<LevelManagerScript>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Update()
    {
        animator.SetInteger("State", (int)currentState);

        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrolling:
                Patrolling();
                break;
            case State.Attacking:
                Attacking();
                break;
            case State.Searching:
                Searching();
                break;
            default:
                break;
        }

    }

    void Idle()
    {
        if (idleTimeCounter >= idleTime)
        {
            currentState = State.Patrolling;
            patrollingDestination = this.transform.position +
            new Vector3(
                Random.Range(-patrolRange, patrolRange),
                Random.Range(-patrolRange, patrolRange), 0);
            LookAtPlayer();
            idleTimeCounter = 0;
        }
        else
        {
            idleTimeCounter += Time.deltaTime;
        }

        if (Vector3.Distance(target.position, this.transform.position) <= minCombatDistance)
        {
            currentState = State.Attacking;
        }
    }

    void Patrolling()
    {
        rb.AddForce(this.transform.up * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        LookAtPlayer();
        if (Vector3.Distance(target.position, this.transform.position)
            <= minCombatDistance)
        {
            currentState = State.Attacking;
        }
        else if (Vector3.Distance(patrollingDestination, this.transform.position)
            <= minDestinationDistance)
        {
            currentState = State.Idle;
        }
    }

    void Attacking()
    {
        if (updateCounter >= updateFrequency)
        {
            updateCounter = 0;
            if (Vector3.Distance(target.position, this.transform.position) >= minCombatDistance)
            {
                currentState = State.Patrolling;
            }
        }
        else
        {
            updateCounter += Time.deltaTime;
        }
        if (shootCounter >= shootFrequency)
        {
            shootCounter = 0;
            Shoot();
        }
        else
        {
            LookAtPlayer();
            shootCounter += Time.deltaTime;
        }
    }

    void Searching()
    {
        if (Vector3.Distance(target.position, this.transform.position) <= minCombatDistance)
        {
            currentState = State.Attacking;
        }
    }
    void LookAtPlayer()
    {
        targetpos = target.position;
        Vector2 lookDir = targetpos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb2 = bullet.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health -= 1f;
            if (health <= 0)
            {
                lvl.remaining -= 1;
                if (lvl.remaining == 0 && lvl.level >= 4)
                {
                    Instantiate(powerup, this.transform.position, Quaternion.identity);
                }
                Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}

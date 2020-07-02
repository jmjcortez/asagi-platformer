using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public float moveSpeed, moveTime, waitTime;
    public float health;
    private float moveCount, waitCount;
    [Range(0, 100)] public float chanceToDrop;

    public Transform leftPoint, rightPoint;

    private bool movingRight;


    private Rigidbody2D enemyRigidBody;

    public SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject deathEffect;
    public GameObject loot;

    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength; // sight distance
    public float attackDistance;
    public float timer; // cooldown for attacks
    
    public GameObject target;
    public bool inRange;

    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private float distance;

    private bool attackMode;
    private bool cooling;
    private float intTimer;
    #endregion

    private void Awake() {
        instance = this;

        intTimer = timer;
    }

    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        leftPoint.parent = null;    // we dont want left points to be relative to the enemy after start
        rightPoint.parent = null;   

        movingRight = true;

        moveCount = moveTime;
    }

    void Update()
    {
        // Move();
        if (inRange) {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
            RaycastDebugger();
        }

        //when player is detected
        if (hit.collider != null) {
            EnemyLogic();
        }
        else if (hit.collider == null) {
            StopAttack();
            animator.SetBool("isMoving", false);
        }
    }

    void EnemyLogic() {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance) {
            Move2();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false) {
            Attack();
        }

        if (cooling) {
            Cooldown();
            animator.SetBool("isAttacking", false);
        }
    }  

    void Move2() {
        animator.SetBool("isMoving", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BearAttack")) {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack() {
        timer = intTimer;   //Reset Timer when player enter attack range
        attackMode = true;  //to check if enemy can still attack

        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
    }

    void StopAttack() {
        cooling = false;
        attackMode = false;
        animator.SetBool("isAttacking", false);
    }

    public void TakeDamage (float damage) {
        health -= damage;

        if (health <= 0) {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Die();
        }
    }

    private void Move() {
                if (moveCount > 0) {
            moveCount -= Time.deltaTime;

            if (movingRight) {
            enemyRigidBody.velocity = new Vector2(moveSpeed, enemyRigidBody.velocity.y);
            spriteRenderer.flipX = true;

                if (transform.position.x > rightPoint.position.x) {
                    movingRight = false;
                }
            }
            else {
                enemyRigidBody.velocity = new Vector2(-moveSpeed, enemyRigidBody.velocity.y);
                spriteRenderer.flipX = false;

                if (transform.position.x < leftPoint.position.x) {
                    movingRight = true;
                }
            }
        
            if(moveCount <= 0) {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

            animator.SetBool("isMoving", true);
        } else if (waitCount > 0) {
             waitCount -= Time.deltaTime;
             enemyRigidBody.velocity = new Vector2(0f, enemyRigidBody.velocity.y);
        
            if (waitCount <= 0) {
                moveCount = Random.Range(moveTime * 0.75f, moveTime * 1.25f);

            }
            animator.SetBool("isMoving", false);

        } 
    }

    private void Die() {
        float dropSelect = Random.Range(0, 100f);

        if (dropSelect <= Random.Range(0, 100f)) {
            Instantiate(loot, transform.position, transform.rotation);
        }

        AudioManager.instance.PlaySFX(3);

        Destroy(gameObject);
    }

    // void OnTriggerEnter2D(Collider2D trig) {
    //     if(trig.gameObject.tag == "Player") {
    //         target = trig.gameObject;
    //         inRange = true;
    //     }
    // }

    void RaycastDebugger() {
        if(distance > attackDistance) {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if(attackDistance > distance) {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling() {
        cooling = true;
        Debug.Log("triggered");
    }

    void Cooldown() {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode) {
            cooling = false;
            timer = intTimer;
        }
    }
}

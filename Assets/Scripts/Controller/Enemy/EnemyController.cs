using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed, moveTime, waitTime;
    private float moveCount, waitCount;

    public Transform leftPoint, rightPoint;

    private bool movingRight;


    private Rigidbody2D enemyRigidBody;

    public SpriteRenderer spriteRenderer;
    private Animator animator;

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
}

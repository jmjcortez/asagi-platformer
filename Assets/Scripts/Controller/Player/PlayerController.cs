using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D asagiRigidBody;
    public LayerMask ground;
    public Transform groundCheckPoint;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
        
    private bool isGrounded;
    private float knockBackCounter;
    public bool canDoubleJump;
    public float moveSpeed, jumpPower;
    public float knockBackLength, knockBackForce;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter <= 0) {
            Animate();
            Move();
            CheckJump();     
            CheckAttack();
            CheckUseTabs();
        } else {
            knockBackCounter -= Time.deltaTime;
        }
    }

    private void Move() {
        asagiRigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), asagiRigidBody.velocity.y);

    }

    private void Animate() {
        if (asagiRigidBody.velocity.x > 0.9999f) {
            spriteRenderer.flipX = true;
        }
        else if (asagiRigidBody.velocity.x < -0.99999f) {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("moveSpeed", Mathf.Abs(asagiRigidBody.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }

    private void CheckJump() {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, ground);
    
        if (Input.GetButtonDown("Jump")) {
            if (isGrounded) {
                canDoubleJump = true;
                Jump();
            }
            else {
                if (canDoubleJump) {
                    canDoubleJump = false;
                    Jump();
                }
            }
        }
    }

    private void Jump() {
        asagiRigidBody.velocity = new Vector2(asagiRigidBody.velocity.x, jumpPower);
    }

    private void CheckAttack() {
        if (Input.GetMouseButtonDown(0)) {
            animator.SetBool("isFiring", true);
            // asagiRigidBody.velocity = Vector3.zero;            
        }
        if (Input.GetMouseButtonUp(0)) {
            animator.SetBool("isFiring", false);
        }

        if (Input.GetMouseButtonDown(1)) {
            animator.SetBool("isFiringCanon", true);
            // asagiRigidBody.velocity = Vector3.zero;            
        }
        if (Input.GetMouseButtonUp(1)) {
            animator.SetBool("isFiringCanon", false);
        }
    }

    public void KnockBack() {
        knockBackCounter = knockBackLength;
        
        if (!spriteRenderer.flipX) {
            asagiRigidBody.velocity = new Vector2(knockBackForce, knockBackForce);
            // asagiRigidBody.velocity = new Vector2(0f, knockBackForce);

        }
        else {
            asagiRigidBody.velocity = new Vector2(-knockBackForce, knockBackForce);
        }

        animator.SetTrigger("isHurt"); 
    }

    public void CheckUseTabs() {
        if (Input.GetButtonDown("Fire2")) {
            LevelManager.instance.ActivateTab();
        }
    }
}

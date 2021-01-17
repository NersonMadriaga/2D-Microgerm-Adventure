using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyController : MonoBehaviour
{
    public bool isPatrol;
    public float walkSpeed;
    public bool isTurn;
    public float distance;
    private bool attackMode;
    public float attackRange = 0.5f;

    public Rigidbody2D rb;
    private Animator anim;
    public Transform groundCheckPos;
    public Transform target;
    public LayerMask groundLayer;
    public LayerMask playerMask;
    private Collider2D bodyCollider;
    public Transform attackPoint;

    public int damage;

    private bool walk;

    private float attackTime = 0.5f;
    private float timeSinceAttack;

    private void Start()
    {
        isPatrol = true;
        attackMode = false;
        bodyCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        bool walk = HasParameter("isWalking", anim);
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;

        RaycastHit2D playerCheck = Physics2D.Raycast(target.position, target.right, distance, playerMask);

        if(playerCheck.collider == true)
        {
            Debug.DrawRay(target.position, target.right * distance, Color.red);
            if(Vector2.Distance(target.position, playerCheck.transform.position) < distance)
            {
                attackMode = true;
               
            }
        } else
        {
            attackMode = false;
            
        }

        if (attackMode && timeSinceAttack >= 0.75f)
        {
            Attack();
        }

        if (attackMode)
        {
            anim.SetBool("isAttacking", true);
        } else
        {
            anim.SetBool("isAttacking", false);
            Patrol();
        }
    }

    private bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }


    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerMask);
        // damage enemies

        foreach (Collider2D enemy in hit)
        {
            Debug.Log("we hit " + enemy.transform.parent.name);
            enemy.transform.parent.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage);
        }

        timeSinceAttack = 0f;
    }

    void Patrol()
    {
        if (isTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        

        

        if (walk)
        {
            anim.SetBool("isWalking", true);
        }
    }

    private void FixedUpdate()
    {

        if (isPatrol)
        {
            isTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Flip()
    {
        isPatrol = false;
        transform.Rotate(0f, 180f, 0);
        walkSpeed *= -1;
        isPatrol = true;
    }


}

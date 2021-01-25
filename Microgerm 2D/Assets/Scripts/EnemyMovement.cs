using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private bool isWall;
    private bool isInAttackRange;
    private bool isAttackMode;
    private bool isGrounded;
    private bool isMovingRight;

    private float timeSinceAttack;

    private Vector2 direction;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float distance;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeToMove;

    [SerializeField] private int damage;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform targetCheck;

    [SerializeField] private LayerMask terrain;
    [SerializeField] private LayerMask playerMask;
    private void Awake()
    {

        isAttackMode = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceAttack += Time.deltaTime;
        isGrounded = IsGrounded();
        isWall = IsWall();
        isInAttackRange = InAttackRange();

        DirectionCheck();
        CanAttack();

        anim.SetBool("isAttacking", isAttackMode);
    }

    void FixedUpdate()
    {

        if (isAttackMode || gameObject.GetComponent<Enemy>().IsDead)
        {
            EnemyStop();

            if(timeSinceAttack >= 0.85f && gameObject.GetComponent<Enemy>().IsDead == false)
            {
                Attack();
            }
            
        } else
        {
            EnemyMove();
        }

        
    }

    void LateUpdate()
    {
        if (isGrounded == false || isWall == true)
        {
            Flip();
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

    private void Attack()
    {

        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(targetCheck.position, attackRange, playerMask);
        // damage enemies

        foreach (Collider2D enemy in hitenemies)
        {
            enemy.transform.parent.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage);
        }

        timeSinceAttack = 0f;
    }

    private void CanAttack()
    {
        if (isInAttackRange)
        {
            isAttackMode = true;
        } else
        {
            isAttackMode = false;
        }
    }

    private bool InAttackRange()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(targetCheck.position, direction, attackRange, playerMask);
        Debug.DrawRay(targetCheck.position, direction * attackRange, GetRaycastColor(raycastHit));
        return raycastHit.collider != null;
    }

    private bool IsWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundCheck.position, direction, distance, terrain);
        Debug.DrawRay(groundCheck.position, direction * distance, GetRaycastColor(raycastHit));
        return raycastHit.collider != null;
    }

    private bool IsGrounded()
    {
        float extraHeight = 1.5f;
        RaycastHit2D raycastHit = Physics2D.Raycast(groundCheck.position, Vector2.down, extraHeight, terrain);
        Debug.DrawRay(groundCheck.position, Vector2.down * extraHeight, GetRaycastColor(raycastHit),Time.deltaTime);
        return raycastHit.collider != null;
    }

    private void DirectionCheck()
    {
        if(movementSpeed > 0f)
        {
            isMovingRight = true;
        } else
        {
            isMovingRight = false;
        }

        direction = isMovingRight ? Vector2.right : Vector2.left;
    }
    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        movementSpeed *= -1f;
    }

    private void EnemyMove()
    {
        rb.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, rb.velocity.y);

        if(HasParameter("isWalking", anim))
        {
            anim.SetBool("isWalking",true);
        }
    }

    private void EnemyStop()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);

        if (HasParameter("isWalking", anim))
        {
            anim.SetBool("isWalking", false);
        }

    }

    private Color GetRaycastColor(RaycastHit2D raycast)
    {
        return raycast.collider ? Color.green : Color.red;
    }
}

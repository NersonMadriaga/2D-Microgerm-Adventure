using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private float timeSinceAttack = 0f;
    private float attackTime;
    private int currentAttack = 0;
    private bool attackMode = false;

    [SerializeField] private bool dev;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int damage;

    public bool AttackMode
    {
        get
        {
            return attackMode;
        }

        set
        {
            attackMode = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

        if ( dev && Input.GetMouseButtonDown(0) && timeSinceAttack > 0.25f)
        {
            Attack();
        }

        if (attackMode && timeSinceAttack >= attackTime)
        {
            attackMode = false;
        }
    }

    public void Attack()
    {
        currentAttack++;
        attackMode = true;
        if (currentAttack > 3)
        {
            currentAttack = 1;
        }

        if (timeSinceAttack > 1.0f)
        {
            currentAttack = 1;
        }
        // play attack animation
        anim.SetTrigger("Attack_"+currentAttack);
        FindObjectOfType<AudioManager>().Play("Attack");


        timeSinceAttack = 0.0f;
        // detect enemies in rangw of attack
        
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // damage enemies

        foreach(Collider2D enemy in hitenemies)
        {
            Debug.Log("we hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        
    }

}

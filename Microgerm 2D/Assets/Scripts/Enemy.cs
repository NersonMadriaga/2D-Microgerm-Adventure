using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool isBoss;
    [SerializeField] private bool dev = false;
    [SerializeField] private int maxHealth;
    [SerializeField] private Health healthBar;
    

    private bool isDead;
    private int currentHealth;
    private Animator anim;

 

    public bool IsDead
    {
        get { return isDead; }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
        isDead = false;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    private void HealthCheck()
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance.RegenHealth();
            isDead = true;
            if (HasParameter("isDead", anim))
            {
                anim.SetTrigger("isDead");
            }

            if (isBoss)
            {
                GameManager.Instance.IncreaseLevel();
            }

            StartCoroutine(ClearEnemy());
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

    IEnumerator ClearEnemy()
    {
        

        if (HasParameter("isDead",anim))
        {
            yield return new WaitForSeconds(1.5f);
        } else
        {
            yield return new WaitForSeconds(0.3f);
        }
        
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        HealthCheck();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool isBoss;
    [SerializeField] private bool dev = false;
    [SerializeField] private int maxHealth;
    [SerializeField] private Health healthBar;
    [SerializeField] private GameObject loot;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private int itemIndex;

    private bool isDead;
    private bool isAlreadyDropItem;
    private int currentHealth;
    private Animator anim;

    public int ItemIndex
    {
        get
        {
            return itemIndex;
        }
    }

    public bool IsDead
    {
        get { return isDead; }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
        isDead = false;
        isAlreadyDropItem = false;

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
                GameManager.Instance.OpenConfirmation();
                //GameManager.Instance.IncreaseLevel();
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
        if (isAlreadyDropItem == false)
        {
            DropItem();
            isAlreadyDropItem = true;
        }
        
        if (HasParameter("isDead",anim))
        {
            yield return new WaitForSeconds(1.5f);
        } else
        {
            yield return new WaitForSeconds(0.3f);
        }
        
        Destroy(gameObject);
    }

    private void DropItem()
    {
        Instantiate(loot, dropPoint.position, Quaternion.identity);
        //loot.transform.position = new Vector3(dropPoint.position.x,dropPoint.position.y,dropPoint.position.z);
        //loot.transform.rotation = Quaternion.identity;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        HealthCheck();
    }
}

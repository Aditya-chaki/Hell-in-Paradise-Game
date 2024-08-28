using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    Animator myAnimator;
    public int maxHealth = 500;
    public int currentHealth;
    bool isAlive = true;
    [SerializeField] private float delayBeforeLoading = 3f;
    [SerializeField] private float timeElapsed;
    public HealthBar healthBar;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    void Update()
    {
        if (!isAlive)
        {
            timeElapsed += Time.deltaTime;
        }

        if (timeElapsed > delayBeforeLoading && !isAlive)
        {
            SceneManager.LoadScene(6);
        }
    }





    public void TakeDamage(int damage)
    {


        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        myAnimator.SetTrigger("dead");

    }

}

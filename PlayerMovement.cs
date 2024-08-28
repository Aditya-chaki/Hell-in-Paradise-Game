using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    private bool isAttacking;
    bool isAlive = true;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    [SerializeField]private float delayBeforeLoading = 3f;
    [SerializeField]private float timeElapsed;




    void Start()
    {

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);





    }


    void Update()
    {
        if (isAlive)
        {
            Run();
            die();
            FlipSprite();
            attack();
            attack1();
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);


            if (myRigidbody.velocity.y == 0 && !myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {

                myAnimator.SetBool("isFalling", true);

            }
            if (myRigidbody.velocity.y == 0 || !myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {

                myAnimator.SetBool("isJumping", false);

            }
            if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                TakeDamage(2);
            }
            if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
            {
                TakeDamage(100);

            }

        }
        else{
            timeElapsed += Time.deltaTime;

        }

        if (timeElapsed > delayBeforeLoading && !isAlive)
        {
            SceneManager.LoadScene(5);
        }

      








    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);

        }
    }

    void OnJump(InputValue value)

    {

        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {

            return;
        }

        if (value.isPressed)
        {
            // do stuff

            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            myAnimator.SetBool("isJumping", true);




        }


    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }

    void attack()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            myAnimator.SetBool("isAttacking", true);


        }
        if (Input.GetButtonUp("Fire3"))
        {
            myAnimator.SetBool("isAttacking", false);
        }

    }
    void attack1()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            myAnimator.SetBool("isAttacking1", true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            myAnimator.SetBool("isAttacking1", false);
        }

    }
    void die()
    {
        if(currentHealth<=2){
           
        }
        if (currentHealth <= 0)
        {

            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            
        

        }

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

}
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    Animator animator;
    CapsuleCollider playerCollider;

    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    public float attackCooldown;
    private bool isAttacking = false;
    private float currentCooldown;

    //public TextMeshProUGUI countText;
    private int count;

    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;
    public string inputCtrl;

    public Vector3 jumpSpeed;

    public bool isDead = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();

        count = 0;

        /*SetCountText();
        winTextObject.SetActive(false);*/
    }

    bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.35f);
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);

                if (!isAttacking)
                {
                    animator.Play("Female Sword Walk");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }

            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animator.Play("Female Sword Sprint");
            }

            if (Input.GetKey(inputBack))
            {
                transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);

                if (!isAttacking)
                {
                    animator.Play("Female Sword Walk");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }

            if (Input.GetKey(inputLeft))
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKey(inputRight))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }

            if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))
            {
                if (!isAttacking)
                {
                    animator.Play("Female Sword Stance");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }

            if (Input.GetKeyDown(inputCtrl))
            {
                animator.Play("Female Sword Roll");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpSpeed.y;

                gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
                animator.Play("Female Jump Up");
                /*animator.Play("Female Fall");
                animator.Play("Female Land");*/
            }
        }

        /*if (inputCtrl.GetKeyDown(KeyCode.Mouse0))
        {
        }*/

        if (isAttacking)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentCooldown <= 0)
        {
            currentCooldown = attackCooldown;
            isAttacking = false;
        }
    }
    public void Attack()
    {
        isAttacking = true;
        animator.Play("Female Sword Attack 1");
    }

    /*void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            //SetCountText();
        }
    }
}
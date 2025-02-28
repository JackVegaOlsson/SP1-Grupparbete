using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float bounciness = 100;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;

    [SerializeField] private int damageGiven = 1;

    private SpriteRenderer rend;
    private Animator anim;

    private bool canMove = true;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed < 0)
        {
            rend.flipX = false;
        }

        if (moveSpeed > 0)
        {
            rend.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);


            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(knockbackForce, upwardForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(-knockbackForce, upwardForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounciness));
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;

            Destroy(gameObject, 0.7f);
        }
    }
}

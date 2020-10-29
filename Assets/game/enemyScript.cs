using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour, IEntityTakeDamage
{
    Rigidbody2D rb;
    Collider2D col;
    Collider2D playerCol;

    public float life = 100.0f;


    Vector3 dir;
    float speed = 2.5f;
    bool isWalking = false;

    float sightDistance = 7.0f;
    float attackDamage = 10.0f;
    float attackKnockback = 1.0f;

    float waitForMovement = 0.0f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerCol = GameObject.Find("player").GetComponent<Collider2D>();
    }

    void Update() {
        if (waitForMovement > 0.0f)
            waitForMovement -= Time.deltaTime;

        isWalking = false;
        if (Vector2.Distance(playerCol.transform.position, transform.position) <= sightDistance && waitForMovement <= 0.0f) {
            isWalking = true;
            
            dir = (playerCol.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider == playerCol) {
            IEntityTakeDamage script = playerCol.GetComponent<IEntityTakeDamage>();
            script.takeDamage(attackDamage, attackKnockback, dir);

            waitForMovement = 0.7f;
        }
    }

    public void takeDamage(float damage, float knockback, Vector3 dealerDir) {
        life -= damage;
        waitForMovement = 0.5f;
        
        transform.position += dealerDir * knockback;
    }
}

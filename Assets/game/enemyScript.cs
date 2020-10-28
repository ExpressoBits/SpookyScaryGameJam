using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour, IEntityTakeDamage
{
    float life = 100.0f;
    float knockback = 0.0f;
    int knockbackDir = 1;

    void Start() {
        
    }

    void Update() {
        
    }

    public void takeDamage(float damage, float knock, int dealerDir) {
        life -= damage;
        knockback = knock;
        knockbackDir = dealerDir;
    }
}

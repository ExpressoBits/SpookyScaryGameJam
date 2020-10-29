using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour, IEntityTakeDamage
{
    Rigidbody2D rb;

    [SerializeField]
    GameObject lifebarUI;

    [SerializeField]
    GameObject attackArea;

    public float life = 100.0f;
    float lifeDisplay = 100.0f;

    //Direção do jogador
    float speed = 5.0f;
    bool isWalking = false;
    public Vector3 dir = new Vector3(1, 0, 0);

    void Start() {
       rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //Barra de vida
        lifeDisplay = Mathf.Lerp(lifeDisplay, life, 15.0f * Time.deltaTime);
        lifebarUI.transform.localScale = new Vector3(lifeDisplay/100.0f, 1, 1);
        
        //Movimentação
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector3 pos = new Vector3(inputX, inputY, 0).normalized;

        isWalking = (inputX != 0 && inputY != 0);
        transform.position += pos * speed * Time.deltaTime;

        //Direção do jogador
        if (inputX != 0)
            dir.Set(inputX, 0, 0);
        
        else if (inputY != 0)
            dir.Set(0, inputY, 0);
        

        //Camera seguindo o player
        Vector3 camPos = new Vector3(rb.position.x, rb.position.y, -10);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camPos, Time.deltaTime * 5);
    }

    public void takeDamage(float damage, float knockback, Vector3 dealerDir) {
        life -= damage;
        
        transform.position += dealerDir * knockback;
    }
}

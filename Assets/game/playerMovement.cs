using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEntityTakeDamage {
    void takeDamage(float damage, float knock, int dealerDir);
}

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
    public int dirX = 1;

    void Start() {
       rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //Barra de vida
        Vector3 newScale = lifebarUI.transform.localScale;
        newScale.x = lifeDisplay/100.0f;
        lifebarUI.transform.localScale = newScale;

        lifeDisplay = Mathf.Lerp(lifeDisplay, life, 15.0f * Time.deltaTime);

        if (Input.GetKeyDown("q"))
            life -= 10.0f;

        if (Input.GetKeyDown("right"))
            dirX = 1;

        if (Input.GetKeyDown("left"))
            dirX = -1;

        attackArea.transform.localPosition = new Vector3(0.13f * dirX, 0, 0);
    }

    void FixedUpdate() {
        Vector2 pos = new Vector2();
        pos.x = 5.0f * Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        pos.y = 5.0f * Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + pos);

        //Camera seguindo o player
        Vector3 camPos = new Vector3(rb.position.x, rb.position.y, -10);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camPos, Time.fixedDeltaTime * 5);
    }

    public void takeDamage(float damage, float knock, int dealerDir) {

    }
}

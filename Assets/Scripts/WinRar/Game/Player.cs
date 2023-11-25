using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float crouchSpeed;
    private bool isOnPlatform;
    private InputSystem inputSystem;
    private Rigidbody2D rb;

    void Start()
    {
        isOnPlatform = false;
        inputSystem = GetComponent<InputSystem>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isOnPlatform)
        {
            MoveCharacter();
        }
        if (transform.position.y < -10)
        {
            TeleportPlayerToTop();
        }
    }

    void MoveCharacter()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (inputSystem.Jump && isOnPlatform)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (inputSystem.Crouch)
        {
            speed = crouchSpeed;
        }
        
    }

    void TeleportPlayerToTop()
    {
        // Устанавливаем позицию игрока на верхнюю границу экрана
        transform.position = new Vector2(transform.position.x, 10);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
        }
    }
}
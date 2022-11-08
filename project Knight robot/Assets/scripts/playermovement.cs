using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float jumpSpeed = 4f;
    public float groundRadiusCheck;
    public LayerMask layers;

    Rigidbody2D rigidbody;
    float moveInput;
    bool jumpInput = false;
    public bool faceLeft = false;
    SpriteRenderer characterSprite;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        characterSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButton("Jump");

        //Checks to see what direction we are moving in.
        //Flips hero sprite assets scale.
        //This also changes the position of the other transforms inside the sprite transform
        if (moveInput > 0)
        {
            faceLeft = false;
            characterSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            faceLeft = true;
            characterSprite.transform.localScale = new Vector3(-1, 1, 1);
        }
}

    private void FixedUpdate()
    {
        Vector2 vel = rigidbody.velocity;
        vel.x = moveInput * movementSpeed;

        bool onGround = GroundCheck();

        if(jumpInput == true && onGround == true)
        {
            vel.y = jumpSpeed;
        }

        rigidbody.velocity = vel;
    }

    bool GroundCheck()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, groundRadiusCheck, layers);
        return hitCollider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, groundRadiusCheck);
    }

}

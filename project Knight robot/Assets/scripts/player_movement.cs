using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public AudioSource Walksound;
    public AudioSource jumpsound;
    private Rigidbody2D rb;
    private BoxCollider2D coll; 
    private SpriteRenderer sprite;
    private Animator anlm;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling } 

    // Start is called before the first frame update
    private void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
       anlm = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);



        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            jumpsound.Play();
        }

        UpdateAnimationState();
    
        /*
            if (Input.GetButton("Horizontal") && IsGrounded())
            {
                Walksound.Play();

            }
        */
            if (rb.velocity.x != 0)
            {
                if (!Walksound.isPlaying)
                {
                    Walksound.Play();
                }
            } 
            else
            {
                Walksound.Stop();
            }
        }

            
    

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true; 
        }
        else
        {
            state = MovementState.idle;
        }  

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        } 
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anlm.SetInteger("state", (int)state);
    } 
    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    } 

    private void Flip()
    { 
        // Switch the way the player is labelled as facing. 

        transform.Rotate(0f, 180f, 0f);
    }  

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomrangAttack : MonoBehaviour
{
    public Transform firepoint;
    public Transform projectile;
    public float throwDistance = 5f;
    public float throwSpeed = 3f;
    public LayerMask layers;

    Vector3 setDestination;
    bool throwing;
    bool returning;

    PlayerMovement player; 

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    } 

    // Update is called once per frame
    void Update()
    {
        
    } 

    void fire()
    {
        //Throw direction based on player facting 
        Vector3 throwDirection = -firepoint.right;
        if (player.faceLeft == false)
            throwDirection = firepoint.right;
        

        //Shoot a raycast to see if it hits anything before the set distance 
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, throwDirection * throwDistance, throwDistance, layers); 
        
        //If we hit something 
        if(hit.collider != null)
        {
            setDestination = hit.point;
        } 
        else
        {
            //Otherwise set the destination to our current pluss our length 
            setDestination = firepoint.position + (throwDirection * throwDistance);
        }
        projectile.position = firepoint.position; // put the projectile where we are 
        projectile.gameObject.SetActive(true); // make it visible 
        throwing = true; // Say we are throwing
    } 

    //Moves the projectile to the set location when throwing is set to true 
    void projectileToDestination()
    {
        if (throwing == false) 
            return; 
        //Moves from current position to the new position over a set time. 
                                                     //Current position, point to go to, speed in which we move 
        projectile.position = Vector3.MoveTowards(projectile.position, setDestination, throwSpeed * Time.deltaTime); 

        //Gets magnitude between the two points. If it is close, stop moving and start returning 
        if((projectile.position - setDestination).magnitude < 0.4)
        {
            throwing = false; 
            returning = true;
        }
    }
}

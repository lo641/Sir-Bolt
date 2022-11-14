using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BommerangAttack : MonoBehaviour
{
    public Transform firePoint;
    public Transform projectile;
    public float throwDistance = 5f;
    public float throwSpeed = 3f;
    public LayerMask layers;

    Vector3 setDestination;
    bool throwing;
    bool returning;

    PlayerMovement player;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !returning && !throwing)
            Fire();

        ProjectileToDestination();
        Return();
    }

    void Fire()
    {
        //Shoot a raycast to see if it hits anything before the set distance

        Vector3 throwDirection = -firePoint.right;
        if (player.faceLeft == false)
            throwDirection = firePoint.right;
        else
                    if (player.faceLeft == true)
            throwDirection = -firePoint.right;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, throwDirection * throwDistance, throwDistance, layers);

        if(hit.collider != null)
        {
            setDestination = hit.point;
        }
        else
        {
            setDestination = firePoint.position + (throwDirection * throwDistance);
        }
        projectile.position = firePoint.position;
        projectile.gameObject.SetActive(true);
        throwing = true;
    }


    //Moves the projectile to the set location when throwing is set to true
    void ProjectileToDestination()
    {
        if (throwing == false)
            return;

        //Move from current position to the new position over a set time.
        projectile.position = Vector3.MoveTowards(projectile.position, setDestination, throwSpeed * Time.deltaTime);

        //Gets magnitude between the two points. If it is close, stop moving and start returning
        if((projectile.position - setDestination).magnitude < 0.4)
        {
            throwing = false;
            returning = true;
        }
    }

    //Returns the projectile to the palyer when returning is set to true
    void Return()
    {
        if (returning == false)
            return;

        //Move from current position to the new position over a set time.
        projectile.position = Vector3.MoveTowards(projectile.position, firePoint.position, throwSpeed * 2 * Time.deltaTime);

        //Gets magnitude between the two points. If it is close, hide asset and turn off returning
        if ((projectile.position - firePoint.position).magnitude < 0.4)
        {
            projectile.gameObject.SetActive(false);
            returning = false;
        }
    }
}

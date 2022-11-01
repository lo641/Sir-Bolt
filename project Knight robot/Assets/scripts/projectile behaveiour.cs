using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilebehaveiour : MonoBehaviour
{
    public float speed = 4.5f;
    public Vector3 Launchoffset;
    public bool Thrown;
    private void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(Launchoffset);
        transform.position += transform.right * Time.deltaTime * speed; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

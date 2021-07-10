using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriggers : MonoBehaviour
{
    private BoxCollider2D playerCollider;

    [SerializeField]
    private BoxCollider2D platformCollider;

    [SerializeField]
    private BoxCollider2D triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, triggerCollider, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }
    
}

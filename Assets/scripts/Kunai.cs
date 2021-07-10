using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Kunai : MonoBehaviour
{
    private Rigidbody2D kunaiRigidbody;
    [SerializeField]
    private float speed;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        kunaiRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        kunaiRigidbody.velocity = direction * speed;
    }
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        
    }
    

}

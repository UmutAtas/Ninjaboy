using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float movementSpeed;
    protected bool facingRight;
    [SerializeField]
    protected GameObject kunaiPrefab;
    public bool Attack { get; set; }
    public Animator MyAnimator { get; set; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
    public virtual void ThrowKunai(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(kunaiPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Kunai>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(kunaiPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Kunai>().Initialize(Vector2.left);
        }
    }
}

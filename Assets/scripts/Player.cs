using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character          
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;
         
    public  LayerMask whatIsGround; 
        
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool airControl;

    public Rigidbody2D MyRigidbody { get; set; }
    public bool Jump { get; set; }
    public bool Slide { get; set; }
    public bool OnGround { get; set; }

    private Vector2 startPos;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        startPos = transform.position;
        
        MyRigidbody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        HandleInput();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        OnGround = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);
        
        HandleLayers();
               
    }

    private void HandleMovement(float horizontal)
    {
        if (!Attack && !Slide && OnGround || airControl)
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        if (MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if (Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal)
    {
        if ((horizontal > 0 &&
             !facingRight &&
             !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") &&
             !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
            ||
            (horizontal < 0 &&
             facingRight &&
             !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") &&
             !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")))
        {
            ChangeDirection();
        }
    }
    
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MyAnimator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            MyAnimator.SetTrigger("throw");
        }
    }
    
    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject !=gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowKunai(int value)
    {
        if (!OnGround && value == 1 || OnGround && value ==0)
        {
            base.ThrowKunai(value);
        }
        
    }
    
}

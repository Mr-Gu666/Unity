using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    // 移动
    public float Speed;
    private Vector3 MoveVelocity;
    private float horizontalMove = 0f;
    private float movementSmoothing = 0.05f;
    // 跳跃
    public float JumpForce;
    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public Transform CellingCheck;
    private float firstJumpTime;
    // 碰撞
    private Rigidbody2D rigidbody;
    // 动画
    public Animator animator;

    public bool AirControl;
    public bool CouldJumpTwice;
    private bool onGround;
    private bool floating;
    private bool jump;
    private bool facingRight;
    private bool isJumping;
    private bool jumpTwice;

    // overlap(交叠)的半径
    const float groundedRadius = .2f;
    // 判断能否站起来的半径
    const float cellingRadius = .2f;
    // 两次跳跃最大间隔
    const float deltaJump = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        MoveVelocity = Vector3.zero;
        onGround = false;
        floating = false;
        jump = false;
        facingRight = true;
        isJumping = false;
        jumpTwice = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Move
        if (!floating)
        {
            //水平移动速度？这个速度是向量吗？
            horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;
        }
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            if (isJumping)
            {
                float delta = Time.time - firstJumpTime;
                if (delta <= deltaJump)
                {
                    jumpTwice = true;
                }
            }
            firstJumpTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        checkIfOnGround();
        float move = horizontalMove * Time.fixedDeltaTime;
        //遇到jump卡顿是因为写在了一个文件里面，后续的jump输入造成了干扰
        Move(move,jump,jumpTwice);
        SetAnimation(jump,jumpTwice);
        jump = false;
        jumpTwice = false;
    }

    void Move(float move,bool jump,bool jumpTwice)
    {
        //在地面上可进行左右移动
        if (onGround || AirControl)
        {
            if (horizontalMove > 0 && !facingRight)
            {
                Filp();
            }
            if (horizontalMove < 0 && facingRight)
            {
                Filp();
            }
            Vector3 targetVelocity = new Vector2(move * 10f, rigidbody.velocity.y);
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref MoveVelocity, movementSmoothing);
        }
        //选择了jump且在地面可以jump
        if (jump && onGround)
        {
            rigidbody.AddForce(new Vector2(0f, JumpForce));
            isJumping = true;
        }
        if (jumpTwice)
        {
            rigidbody.AddForce(new Vector2(0f, JumpForce * 0.5f));
            isJumping = false;
            //Debug.Log("jump twice");
        }
    }

    //检查是否在地面上
    void checkIfOnGround()
    {
        onGround = false;
        if (Physics2D.OverlapCircle(GroundCheck.position, groundedRadius, GroundLayer))
        {
            onGround = true;
        }
    }
    //人物水平翻转
    void Filp()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void SetAnimation(bool jump,bool isDoubleJump)
    {
        if (horizontalMove == 0)
        {
            animator.SetBool("IsMove", false);
        }
        else
        {
            animator.SetBool("IsMove", true);
        }
        if (onGround)
        {
            animator.SetBool("OnGround", true);
            animator.SetBool("IsJump", false);
            animator.SetBool("IsDoubleJump", false);
        }
        else
        {
            animator.SetBool("OnGround", false);
            if (isDoubleJump)
            {
                animator.SetBool("IsDoubleJump",true);
            }
            else
            {
                animator.SetBool("IsJump", true);
            }
        }
    }
}

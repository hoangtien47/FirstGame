using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isDead = false;
    public float horizontal;
    public float speed;
    public float jumpingPower;
    public float fall;
    public float lowjump;
    public bool isFacingRight = true;
    public bool isJumping;
    public bool isFalling;
    public bool inCB;
    public bool inDash;
    private bool inHurt;
    // Slope
    private Vector2 colliderSize;
    private Vector2 slopeNormalPerp;
    [SerializeField] public float slopeCheckDistance;
    [SerializeField] public float slopeCheckDistanceHori;
    public float slopeAngle;
    public float slopeAngleOld = 0;
    public float slopeSideAngle;
    public bool onSlope;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D haveFriction;
    //
    public bool isWallSliding;
    [SerializeField] private float wallSlidingSpeed = 2f;

    public bool isWallJumping;
    private float wallJumpingDirection;
    public float wallJumpingTime;
    private float wallJumpingCounter;
    public float wallJumpingDuration;
    public Vector2 wallJumpingPower = new Vector2();
    //dashing
    [SerializeField] private bool canDash = true;
    public bool isDashing;
    [SerializeField] private float dashingPower = 3f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCoolDown = 1f;
    //Component
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D cc;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;


    private void Start()
    {
        inCB = false;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        colliderSize = cc.size;
    }
    private void Update()
    {
        if (isDead)
            return;
        if (isDashing)
        {
            return;
        }
        AlwaysCheckSlope();
        SetHorizontal();
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        if (!isWallJumping && onSlope && !isJumping && IsGrounded())
        {
            rb.velocity = new Vector2(-horizontal * speed * slopeNormalPerp.x , -horizontal * speed * slopeNormalPerp.y);
        }
        if (Input.GetKeyDown(KeyCode.Z) && canDash && IsGrounded())
        {
            StartCoroutine(Dash());
            GetComponent<PlayerAnimator>().SetDashing();
        }
        betterJump();
        WallSlide();
        WallJump();
        SlopeCheck();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void SetHorizontal()
    {
        if (inCB || inDash)
        {
            horizontal = 0;
            if (GetComponent<PlayerCombatBehaviour>().GetForce())
            {
                if (isFacingRight)
                {
                    horizontal = 2;
                }
                else
                {
                    horizontal = -2;
                }
            }
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
    }
    private void AlwaysCheckSlope()
    {
        if (slopeAngle != slopeAngleOld)
        {
            onSlope = true;
        }
    }
    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0, colliderSize.y / 2);
        SlopeCheckVertical(checkPos);
        SlopeCheckHorizontal(checkPos);
    }
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeCheckRight = Physics2D.Raycast(checkPos, Vector2.right, slopeCheckDistanceHori, groundLayer);
        RaycastHit2D slopeCheckLeft = Physics2D.Raycast(checkPos, Vector2.left, slopeCheckDistanceHori, groundLayer);

        if (slopeCheckRight && slopeAngle != slopeAngleOld)
        {
            onSlope = true;

        }
        else if (slopeCheckLeft && slopeAngle != slopeAngleOld)
        {
            onSlope = true;
        }
        else if (!slopeCheckRight && !slopeCheckLeft && slopeAngle == slopeAngleOld)
        {
            onSlope = false;

        }
    }
    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D check = Physics2D.Raycast(checkPos - new Vector2(-colliderSize.x * 2 / 10, 0), Vector2.down, slopeCheckDistance, groundLayer);
        RaycastHit2D checkB = Physics2D.Raycast(checkPos - new Vector2(colliderSize.x * 2 / 10, 0), Vector2.down, slopeCheckDistance, groundLayer);
        if (check && isFacingRight)
        {
            slopeNormalPerp = Vector2.Perpendicular(check.normal).normalized;
            slopeAngle = Vector2.Angle(check.normal, Vector2.up);
            Debug.DrawRay(check.point, slopeNormalPerp, color: Color.red);
            Debug.DrawRay(check.point, check.normal, color: Color.green);
        }
        if (checkB && !isFacingRight)
        {
            slopeNormalPerp = Vector2.Perpendicular(checkB.normal).normalized;
            slopeAngle = Vector2.Angle(checkB.normal, Vector2.up);
            Debug.DrawRay(checkB.point, checkB.normal, color: Color.green);
            Debug.DrawRay(checkB.point, slopeNormalPerp, color: Color.red);
        }
        if (onSlope && horizontal == 0)
        {
            rb.sharedMaterial = haveFriction;

        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }
    //
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;

                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void betterJump()
    {
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fall) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowjump) * Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                isJumping = true;
                FindAnyObjectByType<AudioManager>().Play("PlayerJump");

            }
        }
        if (rb.velocity.y < 0 && !IsGrounded())
        {
            isJumping = false;
            isFalling = true;
        }
        if (IsGrounded())
        {
            isFalling = false;
        }
    }

    public bool StopWallonGrounnd()
    {
        if (IsGrounded())
        {
            return false;
        }
        else if (!IsWalled())
        {
            return false;
        }
        else if (IsWalled())
        {
            if (horizontal == 0 && rb.velocity.y != 0)
                return false;
        }
        return true;
    }
    public void SetInCB(bool status)
    {
        inCB = status;
    }

    public void SetInDash(bool status)
    {
        inDash = status;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
    public void ResetBool()
    {
        canDash = true;
        isDashing = false;
        inCB = false;
        isWallSliding = false;
        inHurt = true;
    }
    public void setOutHurt()
    {
        inHurt = false;
    }
}

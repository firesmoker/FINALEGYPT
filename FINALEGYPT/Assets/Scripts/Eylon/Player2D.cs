using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = new Vector3(0, 0, 0);
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpHeight = 1.5f;

    Animator myAnimator;
    public float gravity = 0.1f;
    public GameObject head;
    public float headHeight = 1;
    public float headDown = 0.2f;
    public bool canCrouch = true;
    public bool crouching = false;
    private Rigidbody2D rigidbody2D;
    public bool touchingGround;
    public LayerMask groundLayer;
    public float distance;
    public float headBumpHeight;
    public AudioClip footStepSound1, footStepSound2, jumpSound, landingSound;
    private AudioSource audio;
    public bool landed = true;
    public bool debugRay = true;
    public bool debugRayWidth = true;
    public float playerWidth = 4f;
    public float debugHeadWidth = 2f;

    bool FacingRight = true;
    bool HasTorch = true;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        CrouchCalculation();
        MovementCalculation();
    }

    public void MovementCalculation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _velocity.x = horizontalInput;
        if(_velocity.x != 0)
        {
            myAnimator.SetBool("Walking", true);
        }
        else
        {
            myAnimator.SetBool("Walking", false);
        }
        if (!IsGrounded())
        {
            canCrouch = false;
            _velocity.y -= gravity;
        }

        else if (IsGrounded())
        {
            _velocity.y = 0;
            canCrouch = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                PlayJump();
                landed = false;
            }
        }
        HeadBump();
        rigidbody2D.velocity = (_velocity * _speed);
    }

    public void CrouchCalculation()
    {
        if (Input.GetKey(KeyCode.S) && landed)
        {
            Crouch();
        }
        else
        {
            StopCrouch();
        }
    }

    public void Jump()
    {
        StopCrouch();
        Debug.Log("JUMP!");
        _velocity.y = _jumpHeight;
    }


    public void StopCrouch()
    {
        crouching = false;
        Transform headPosition = head.transform;
        headPosition.position = new Vector3(transform.position.x, transform.position.y + headHeight, 0);
    }

    public void Crouch()
    {
        crouching = true;
        Transform headPosition = head.transform;
        headPosition.position = new Vector3(transform.position.x, transform.position.y + headDown, 0);
    }

    public void HeadBump()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, headBumpHeight, groundLayer);
        if (debugRay)
        {
            Debug.DrawRay(position, direction * headBumpHeight, Color.blue);
            Vector2 positionEnd = new Vector2(position.x, position.y + headBumpHeight);
            Debug.DrawRay(positionEnd, Vector2.right* debugHeadWidth, Color.blue);
            Debug.DrawRay(positionEnd, Vector2.left * debugHeadWidth, Color.blue);
        }
            
        if (hit.collider != null)
            _velocity.y -= 1;
    }


    public bool IsGrounded()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y - 2.79f);
        Vector2 rightPosition = new Vector2(position.x + playerWidth, position.y);
        Vector2 leftPosition = new Vector2(position.x - playerWidth, position.y);
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rightPosition, direction, distance, groundLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(leftPosition, direction, distance, groundLayer);
        bool groundHit = false;
        if (debugRay)
        {
            Vector2 originalPosition = new Vector2(position.x, position.y + 2.79f);
            Debug.DrawRay(originalPosition, direction * 2.79f, Color.red);
        }
            
        if (debugRayWidth)
        {
            Debug.DrawRay(position, Vector2.right * playerWidth, Color.red);
            Debug.DrawRay(position, Vector2.left * playerWidth, Color.red);
        }
            


        if (hit.collider != null || hitRight.collider != null || hitLeft.collider != null)
            groundHit = true;
        //if (hitRight.collider != null)
        //    groundHit = true;
        //if (hitLeft.collider != null)
        //    groundHit = true;
        if (groundHit)
        {
            if (landed == false)
            {
                landed = true;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                PlayLanding();
            }
            //Debug.Log("grounded");
            return true;
        }
        //Debug.Log("notgrouneded");
        return false;
    }


    public void ResetY()
    {
        _velocity.y = 0;
    }

    public void FlipSprite(bool toRight)
    {
        if (!toRight)
        {
            FacingRight = true;
            transform.localScale = new Vector2(-0.5f, 0.5f);
        }
        else
        {
            FacingRight = false;
            transform.localScale = new Vector2(0.5f, 0.5f);
        }
    }

    public bool PlayerStands()
    {
        if (_velocity.x == 0)
        {
            return true;
        }
        return false;
    }

    public bool PlayerIsStill()
    {
        if (PlayerStands() && IsGrounded())
        {
            return true;
        }
        return false;
    }

    public void PlayFootstep(int i)
    {
        if(IsGrounded())
        {
            if (i == 1)
                audio.PlayOneShot(footStepSound1);
            else if (i == 2)
                audio.PlayOneShot(footStepSound2);
        }
        
    }

    public void PlayJump()
    {
        audio.PlayOneShot(jumpSound);
    }

    public void PlayLanding()
    {
        audio.PlayOneShot(landingSound);
    }

}

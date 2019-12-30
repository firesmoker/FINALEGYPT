using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = new Vector3(0, 0, 0);
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private bool _isGrounded = false;
    public bool _jumping;
    public float gravity = 0.1f;
    public GameObject head;
    public float headHeight = 1;
    public float headDown = 0.2f;
    public bool canCrouch = true;
    public bool crouching = false;
    private Rigidbody2D rigidbody2D;
    public bool touchingGround;
    public LayerMask groundLayer;
    public float distance = 1.45f;

    bool FacingRight = true;
    bool HasTorch = true;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        MovementCalculation();
        CrouchCalculation();
    }

    public void MovementCalculation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _velocity.x = horizontalInput;
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
            }
        }
        

        //if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        //{
        //    Jump();
        //}
        //transform.Translate(_velocity * _speed * Time.deltaTime);
        rigidbody2D.velocity = (_velocity * _speed);
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
    }

    public void CrouchCalculation()
    {
        if (Input.GetKey(KeyCode.S) && canCrouch)
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
        _jumping = true;
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

    IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _jumping = false;
    }

    //public void isGroundedRay()
    //{
    //    touchingGround = Physics2D.Raycast(transform.position, Vector2.down).distance == 0;
    //    if (touchingGround)
    //    {
    //        canCrouch = true;
    //        _isGrounded = true;
    //        _velocity.y = 0;
    //    }
    //    else
    //    {
    //        _isGrounded = false;
    //    }
    //}

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        //float distance = 2.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            Debug.Log("grounded");
            return true;
        }
        Debug.Log("notgrouneded");
        return false;
    }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.tag == "Platform" && collision != null)
        //    {
        //        canCrouch = true;
        //        _isGrounded = true;
        //        _velocity.y = 0;
        //        Debug.Log("collision");
        //    }
        //}
        //
        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.tag == "Platform" && collision != null)
        //    {
        //        _isGrounded = false;
        //        Debug.Log("NOOOO collision");
        //    }
        //}


        public void ResetY()
    {
        _velocity.y = 0;
        _isGrounded = false;
    }

    public void FlipSprite(bool toRight)
    {
        if (toRight)
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
}

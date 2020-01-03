using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = new Vector3(0, 0, 0);
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] GameObject myTorch;
    private CharacterController _controller;
    [SerializeField] private bool _isGrounded = false;
    public bool _jumping;
    public float gravity = 1;
    public GameObject head;
    public float headHeight = 1;
    public float headDown = 0.2f;
    public bool canCrouch = true;
    public bool crouching = false;

    bool FacingRight = true;
    bool HasTorch = true;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        //myTorch = FindObjectOfType<TorchScript>() as GameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementCalculation();
    }

    public void MovementCalculation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _velocity.x = horizontalInput;
        if (!_isGrounded)
        {
            canCrouch = false;
            _velocity.y -= gravity;
        }

        else if (_isGrounded && !_jumping)
        {
            
            _velocity.y = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        if (Input.GetKey(KeyCode.S) && canCrouch)
        {
            Crouch();
        }
        else
        {
            StopCrouch();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
        _controller.Move(_velocity * _speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Mouse0) && HasTorch)
        {
            ThrowTorch();
        }
    }

    private void ThrowTorch()
    {
        if(!HasTorch) { return; }

        HasTorch = false;


    }

    public void Jump()
    {
        StopCrouch();
        _jumping = true;
        Debug.Log("JUMP!");
        _velocity.y = _jumpHeight;
    }

    public bool isGrounded()
    {
        return true;
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

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Platform" && other != null)
        {
            canCrouch = true;
            _isGrounded = true;
            _velocity.y = 0;
            Debug.Log("collision");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform" && other != null)
        {
            _isGrounded = false;
            Debug.Log("NOOOO collision");
        }
            
    }

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

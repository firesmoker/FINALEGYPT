using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForCamera : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _moveDirection = Vector3.zero;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _gravity = 1f;
    [SerializeField] private float _jumpHeight = 15f;
    [SerializeField] private bool _autoMove = false;
    private bool _isDoubleJumping = false;
    private float _yVelocity;
    [SerializeField] private float _coinNumber = 0f;
    private Vector3 _velocity;
    private float _addedVelocity = 0f;
    [SerializeField] private GameObject _pointA;
    [SerializeField] private GameObject _pointB;
    public Player2D player2d;


    // Start is called before the first frame update
    void Start()
    {
        //_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = (Input.GetAxis("Horizontal"));
        if (_autoMove)
        {
            horizontalInput = 1f;
        }
        Vector3 direction = new Vector3(horizontalInput + _addedVelocity/_speed, 0);
        _velocity = direction * _speed;

        if(_controller.isGrounded)
        {
            _addedVelocity = 0f;
            _yVelocity = -_gravity;
            _isDoubleJumping = false;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //Jump();
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isDoubleJumping)
            {
                //Jump();
                _yVelocity = _jumpHeight + _gravity;
                _isDoubleJumping = true;
            }
                _yVelocity -= _gravity;

            //if(!_isJumping)
            //{
            //velocity.y = -_gravity;
            //}
        }
        _velocity.y = _yVelocity;
        //Debug.Log("yVelocity is" + _yVelocity);

        _controller.Move(_velocity * Time.deltaTime);

        float xBorder = transform.position.x;
        if (transform.position.x <= _pointA.transform.position.x)
        {
            xBorder = _pointA.transform.position.x;
        }
        else if(transform.position.x >= _pointB.transform.position.x)
        {
            xBorder = _pointB.transform.position.x;
        }
        transform.position = new Vector3(xBorder, transform.position.y, transform.position.z);
    }

    public void Jump()
    {
        StartCoroutine(JumpTime(_jumpHeight));
    }

    IEnumerator JumpTime(float jumpHeight)
    {
        float jumpStart = 0;
        while(jumpStart < jumpHeight)
        {
            yield return new WaitForEndOfFrame();
            jumpStart++;
        }

    }

    public void StopRising()
    {
        _yVelocity = -_gravity;
    }

    public float GetCoinNumber()
    {
        return _coinNumber;
    }

    public void AddCoins(int number)
    {
        _coinNumber += number;
    }

    public bool PlayerGrounded()
    {
        return _controller.isGrounded;
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
        if(PlayerStands() && PlayerGrounded())
        {
            return true;
        }
        return false;
    }

    public void ExitPlatformVelocity(float velocity)
    {
        if (Input.GetAxis("Horizontal") == 0f)
        {
            _addedVelocity = velocity;
            Debug.Log(Input.GetAxis("Horizontal"));
        }
        Debug.Log(_addedVelocity);
    }


    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //
    //     //if (collision.collider.gameObject.CompareTag("Platform")) // && !_controller.isGrounded)
    //     //{
    //       //  Debug.Log("collider tag good");
    //       //  _yVelocity = -_gravity;
    //     //}
    //     //Debug.Log(collision.collider.tag);
    //     if(_controller.isGrounded == false)
    //     {
    //         if (!_collided)
    //         {
    //             _collided = true;
    //             _playerYlocation = gameObject.transform.localPosition.y;
    //             Debug.Log("first collision yLocation is" + _playerYlocation);
    //         }
    //         else if(!_collidedAgain)
    //         {
    //             Debug.Log("y position is" + transform.position.y + "and yLocation" + _playerYlocation);
    //             _collidedAgain = true;
    //             if (transform.position.y <= _playerYlocation)
    //             {
    //                 _yVelocity -= _gravity * 10;
    //                 Debug.Log("YESSSSSSS");
    //                 
    //             }
    //
    //         }
    //
    //         //_collided = true;
    //         //_yVelocity -= _gravity*10;
    //     }
    // }



}

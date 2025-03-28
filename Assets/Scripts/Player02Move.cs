using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Move : MonoBehaviour
{

        [SerializeField] private float Speed = 10f;
    [SerializeField] private float JumpForce = 3f;

    //что бы эта переменная работала добавьте тэг "Ground" на вашу поверхность земли
    private bool _isGrounded = false;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // обратите внимание что все действия с физикой 
    // необходимо обрабатывать в FixedUpdate, а не в Update
    void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }

    private void MovementLogic()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _rb.AddForce(movement * Speed);
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * JumpForce);

                // Обратите внимание что я делаю на основе Vector3.up 
                // а не на основе transform.up. Если персонаж упал или 
                // если персонаж -- шар, то его личный "верх" может 
                // любое направление. Влево, вправо, вниз...
                // Но нам нужен скачек только в абсолютный вверх, 
                // потому и Vector3.up
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }

    // private Rigidbody _rb;
    // [SerializeField] private float _speed;
    //  [SerializeField] private float _rotateSpeed;
    // private Vector3 moveVector;
    // void Awake()
    // {
    //     _rb = GetComponent<Rigidbody>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     moveVector.x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
    //     moveVector.z = Input.GetAxis("Vertical")* _rotateSpeed * Time.deltaTime;
    //     _rb.MovePosition(_rb.position + moveVector  );
    // }
    // // void FixedUpdate() {
    // //     float sideForce = Input.GetAxis("Horizontal") * _rotateSpeed;
    // //     float forwardForce = Input.GetAxis("Vertical") * _speed;

    // //     _rb.AddRelativeForce(0f, 0f, forwardForce);
    // //     _rb.AddRelativeTorque(0f, sideForce, 0f);
    // // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove2 : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private float jumpPower = 50f;
    private Rigidbody _rigidBody;
    public bool isGrounded = true;
    private Vector3 jumpDirection = Vector3.up;

    Quaternion _rotation = Quaternion.identity;
    [SerializeField] private float turnSpeed = 7f;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _direction.Set(horizontal, 0f, vertical);
        _direction.Normalize();
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _direction, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation(desiredForward);
    }
    void OnAnimatorMove()
    {
        _rigidBody.MovePosition(_rigidBody.position + _direction * speed);
        _rigidBody.MoveRotation(_rotation);
    }
    private void Jump()
    {
        if (isGrounded)
        {
            this._rigidBody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        var ground = other.gameObject.GetComponentInParent<Ground>();
        if (ground)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        var ground = other.gameObject.GetComponentInParent<Ground>();
        if (ground)
        {
            isGrounded = false;
        }
    }
}

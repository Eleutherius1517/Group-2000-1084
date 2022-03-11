using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speedMove;
    public float jumpPower;

    private float _gravityForce;
    private Vector3 _moveVector;// направление движения персонажа

    private CharacterController _characterController;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        GamingGravity();
    }
    private void Move()
    {
        if (_characterController.isGrounded)
        {
            _moveVector = Vector3.zero;
            _moveVector.x = Input.GetAxis("Horizontal") * speedMove;
            _moveVector.z = Input.GetAxis("Vertical") * speedMove;

            // поворот персонажа в сторону перемещения
            if (Vector3.Angle(Vector3.forward, _moveVector) > 1f || Vector3.Angle(Vector3.forward, _moveVector) == 0f)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }

        _moveVector.y = _gravityForce;

        _characterController.Move(_moveVector * Time.deltaTime);// передвижение по направлению

    }
    private void GamingGravity()
    {
        if (!_characterController.isGrounded) _gravityForce -= 20f * Time.deltaTime;
        else _gravityForce = -1;
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded) _gravityForce = jumpPower;
    }
}

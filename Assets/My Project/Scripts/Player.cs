using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField] private float speed = 0.3f;

    public GameObject enemyPrefab;
    public Transform spawnPosition;

    public GameObject bombPrefab;
    public Transform spawnPositionBomb;

    private bool _isSpawnEnemies;
    [SerializeField] Enemyes _enemyes;

    [SerializeField] private float jumpPower = 50f;
    private Rigidbody _rigidBody;
    public bool isGrounded = true;
    private Vector3 jumpDirection = Vector3.up;

    Quaternion _rotation = Quaternion.identity;
    [SerializeField] private float turnSpeed = 7f;

    void Start()
    {
        this._rigidBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //_direction.x = Input.GetAxis("Horizontal");
        //_direction.z = Input.GetAxis("Vertical");

        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Jump();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.TakeDownBomb();
        }
        _enemyes = FindObjectOfType<Enemyes>();
        if (_enemyes == null)
        {
            _isSpawnEnemies = true;
        }
    }

    private void TakeDownBomb()
    {
        var bombObj = Instantiate(bombPrefab, spawnPositionBomb.position, spawnPositionBomb.rotation);
    }

    void FixedUpdate()
    {

        //Move(Time.fixedDeltaTime);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _direction.Set(horizontal, 0f, vertical);
        _direction.Normalize();
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _direction, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation(desiredForward);


        if (_isSpawnEnemies)
        {
            _isSpawnEnemies = false;
            SpawnEnemies();
        }
        
    }
    void OnAnimatorMove()
    {
        _rigidBody.MovePosition(_rigidBody.position + _direction * speed);
        _rigidBody.MoveRotation(_rotation);
    }
    private void Move(float delta)
    {

        transform.position += _direction.normalized * speed * delta;
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

    private void SpawnEnemies()
    {
        var enemyObj = Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);
        var enemy = enemyObj.GetComponent<Enemyes>();
        enemy.Init(20);
    }
}

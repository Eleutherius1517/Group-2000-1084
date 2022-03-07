using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _direction;
    public float speed = 3f;

    public GameObject enemyPrefab;
    public Transform spawnPosition;

    private bool _isSpawnEnemies;
    [SerializeField] Enemyes _enemyes;

    [SerializeField] private float jumpPower = 50f;
    private Rigidbody _rigidBody;
    public bool isGrounded = true;
    private Vector3 jumpDirection = Vector3.up;

    void Start()
    {
        this._rigidBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        //_direction.y = Input.GetAxis("Jump");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Jump();
        }
        _enemyes = FindObjectOfType<Enemyes>();
        if (_enemyes == null)
        {
            _isSpawnEnemies = true;
        }
    }
    void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
        //if (transform.position.y > 0)
        //{
        //    _direction.y = -_direction.y;
        //    Move(Time.fixedDeltaTime);
        //}
        

        if (_isSpawnEnemies)
        {
            _isSpawnEnemies = false;
            SpawnEnemies();
        }
        
    }
    private void Move(float delta)
    {
        transform.position += _direction * speed * delta;
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

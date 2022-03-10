using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyes : MonoBehaviour
{
    [SerializeField] private float _life = 20f;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _speedRotate = 1f;
    private Bullet _bullet;

    public float speed = 2f;
    public Vector3 dir;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 3)
        {
            _bullet = FindObjectOfType<Bullet>();
            if (_bullet == null)
            {
                Fire();
            }

        }
    }

    void FixedUpdate()
    {
        transform.Translate(speed * dir * Time.deltaTime, Space.World);
        var direction = _player.transform.position - transform.position;
        var stepRotate = Vector3.RotateTowards(transform.forward, direction, _speedRotate * Time.fixedDeltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(stepRotate);
    }

    public void Init(float life)
    {
        Destroy(gameObject, 3f);
        _life = life;

    }
    public void TakeDamage(float amount)
    {
        _life -= amount;
        if (_life <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void Fire()
    {
        var bulletObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.Init(_player.transform, 2, 0.1f);
    }
}

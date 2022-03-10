using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Player _player;
    private Bullet _bullet;
    [SerializeField] private float _speedRotate = 1f;
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
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
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 3)
        {
            var direction = _player.transform.position - transform.position;
            var stepRotate = Vector3.RotateTowards(transform.forward, direction, _speedRotate * Time.fixedDeltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(stepRotate);

        }
        
    }
    private void Fire()
    {
        var bulletObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.Init(_player.transform, 2, 0.1f);
    }
}

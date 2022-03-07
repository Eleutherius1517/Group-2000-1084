using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyes : MonoBehaviour
{
    [SerializeField] private float _life = 20f;

    public float speed = 2f;
    public Vector3 dir;

    void FixedUpdate()
    {
        transform.Translate(speed * dir * Time.deltaTime, Space.World);
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
}

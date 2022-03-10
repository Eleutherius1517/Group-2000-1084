using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _bombDamage = 300f;
    public GameObject bombFlash;

    private void OnTriggerEnter(Collider other)
    {
        var enemies = other.gameObject.GetComponentInParent<Enemyes>();
        if (enemies)
        {
            enemies.TakeDamage(_bombDamage);
            GameObject ImpactGo = Instantiate(bombFlash, transform);
            Destroy(ImpactGo, 2f);
            Destroy(gameObject, 5f);
        }
    }
}

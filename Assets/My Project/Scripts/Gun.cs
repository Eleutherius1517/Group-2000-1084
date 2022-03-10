using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public GameObject muzzle;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    private float count = 0;

    private float nextTimetoFire = 0f;
    
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
    }
    private void FixedUpdate()
    {
        if (count >= 3)
        {
            Debug.Log("Good bye");
            Application.Quit();
        }
    }
    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemyes target = hit.transform.GetComponent<Enemyes>();
            if (target != null)
            {
                target.TakeDamage(damage);
                count++;
                Debug.Log(count++);
            }
            GameObject ImpactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGo, 2f);
        }
    }
}

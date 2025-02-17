using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] AmmoType ammoType;
    
    bool canShoot = true;
    
    // muzzle
    //[SerializeField] ParticleSystem muzzleFlash;
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;
    public GameObject projectilePrefab;
    public GameObject projectileToDisableOnFire;

    private void OnEnable()
    {
        canShoot = true; 
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true) 
        {
            StartCoroutine(Shoot());
        }
    }
    
    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRayCast();
    
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    
    private void PlayMuzzleFlash()
    {
    
        // --- Spawn muzzle flash ---
        var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);
    
        // --- Shoot Projectile Object ---
        if (projectilePrefab != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
        }
    
        // --- Disable any gameobjects, if needed ---
        if (projectileToDisableOnFire != null)
        {
            projectileToDisableOnFire.SetActive(false);
            Invoke("ReEnableDisabledProjectile", 3);
        }
            
    }
    private void ReEnableDisabledProjectile()
    {
        projectileToDisableOnFire.SetActive(true);
    }
    
    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
    
    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}

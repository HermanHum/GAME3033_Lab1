using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    private Vector3 hitLocation;

    protected override void FireWeapon()
    {
        if (WeaponStats.bulletsInClip > 0 && !isReloading)
        {
            base.FireWeapon();
            if (firingEffect)
            {
                firingEffect.Play();
            }
            Ray screenRay = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(screenRay, out RaycastHit hit, WeaponStats.fireDistance, WeaponStats.weaponHitLayers))
            {
                DealDamage(hit);

                hitLocation = hit.point;
                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * WeaponStats.fireDistance, Color.red, 1);
            }
        }
        else if (WeaponStats.bulletsInClip <= 0)
        {
            // trigger a reload when no bullets left
            weaponHolder.StartReloading();
        }
    }

    private void DealDamage(RaycastHit hitInfo)
    {
        IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
        damageable?.TakeDamage(WeaponStats.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, 0.1f);
    }
}

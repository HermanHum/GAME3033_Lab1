using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    protected override void FireWeapon()
    {
        Vector3 hitLocation;

        if (WeaponStats.bulletsInClip > 0 && !isReloading  && !weaponHolder.PlayerController.isRunning)
        {
            base.FireWeapon();
            Ray screenRay = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(screenRay, out RaycastHit hit, WeaponStats.fireDistance, WeaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;
                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * WeaponStats.fireDistance, Color.red, 1);
            }
            else if (WeaponStats.bulletsInClip <= 0)
            {
                // trigger a reload when no bullets left
            }
        }
    }
}

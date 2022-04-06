using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI weaponNameText;
    [SerializeField]
    TextMeshProUGUI currentBulletCountText;
    [SerializeField]
    TextMeshProUGUI totalBulletCountText;
    [SerializeField]
    WeaponComponent weaponComponent;

    private void Start()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
        OnWeaponEquipped(null);
    }

    private void OnDestroy()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    // Set up events for OnWeaponEquipped to handle the weapon component we grab

    private void Update()
    {
        if (!weaponComponent) return;

        currentBulletCountText.text = weaponComponent.WeaponStats.bulletsInClip.ToString();
        totalBulletCountText.text = weaponComponent.WeaponStats.totalBullets.ToString();
    }

    private void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        if (_weaponComponent)
        {
            weaponComponent = _weaponComponent;
            weaponNameText.text = weaponComponent.WeaponStats.weaponName;
        }
        else
        {
            weaponNameText.text = "";
            currentBulletCountText.text = "0";
            totalBulletCountText.text = "0";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Pistol,
    MachineGun
}

public enum WeaponFiringPattern
{
    SemiAuto,
    FullAuto,
    ThreeShotBurst,
    FiveShotBurst,
    PumpAction
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public WeaponFiringPattern firingPattern;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public int totalBullets;
}

public class WeaponComponent : MonoBehaviour
{
    [SerializeField]
    private Transform gripLocation;
    public Transform GripLocation { get { return gripLocation; } }
    [SerializeField]
    private WeaponStats weaponStats;
    public WeaponStats WeaponStats { get { return weaponStats; } }
    protected WeaponHolder weaponHolder;

    [SerializeField]
    protected ParticleSystem firingEffect;

    protected bool isFiring;
    protected bool isReloading;

    protected Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Initialize(WeaponHolder _weaponHolder)
    {
        weaponHolder = _weaponHolder;
    }

    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            //fire weapon
            CancelInvoke(nameof(FireWeapon));
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
        if (firingEffect && firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }
    }

    protected virtual void FireWeapon()
    {
        print("Firing weapon!");
        weaponStats.bulletsInClip--;
    }

    public virtual void StartReloading()
    {
        if (!isReloading)
        {
            isReloading = true;
            ReloadWeapon();
        }
    }

    public virtual void StopReloading()
    {
        isReloading = false;
    }

    // Set ammo counts here
    protected virtual void ReloadWeapon()
    {
        // check to see if there is a firing effect and stop it
        StopFiringWeapon();

        int bulletsToReload = weaponStats.clipSize - weaponStats.totalBullets;
        if (bulletsToReload < 0)
        {
            weaponStats.bulletsInClip = weaponStats.clipSize;
            weaponStats.totalBullets -= weaponStats.clipSize;
        }
        else
        {
            weaponStats.bulletsInClip = weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
    }
}

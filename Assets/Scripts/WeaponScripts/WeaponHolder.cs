using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    private GameObject weaponToSpawn;
    [SerializeField]
    private Sprite crossHairImage;
    [SerializeField]
    private Transform weaponSocketLocation;
    [SerializeField]
    private Transform gripIKSocketLocation;
    [SerializeField]
    private GameObject spawnedWeapon;

    private PlayerController playerController;
    public PlayerController PlayerController { get { return playerController; } }
    private Animator playerAnimator;
    private WeaponComponent equippedWeapon;
    public WeaponComponent EquippedWeapon { get { return equippedWeapon; } set { equippedWeapon = value; } }

    private bool firingPressed;

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingingHash = Animator.StringToHash("isReloading");

    // Start is called before the first frame update
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();

        //spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation);
        //equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        //equippedWeapon.Initialize(this);
        //PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        //gripIKSocketLocation = equippedWeapon.GripLocation;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!gripIKSocketLocation) return;
        if (!playerController.isReloading)
        {
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.position);
        }
    }

    public void OnReload(InputValue value)
    {
        if (!equippedWeapon || playerController.inInventory) return;
        playerController.isReloading = value.isPressed;
        StartReloading();
    }

    public void StartReloading()
    {
        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.WeaponStats.totalBullets <= 0)
        {
            return;
        }

        equippedWeapon.StartReloading();

        playerAnimator.SetTrigger(isReloadingingHash);
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
    }

    public void StopReloading()
    {
        playerController.isReloading = false;
        playerAnimator.ResetTrigger(isReloadingingHash);
        equippedWeapon.StopReloading();

        if (firingPressed)
        {
            StartFiring();
        }
    }

    public void OnAim(InputValue value)
    {
        if (playerController.inInventory) return;
        playerController.isAiming = value.isPressed;
    }

    public void OnFire(InputValue value)
    {
        if (!equippedWeapon || playerController.inInventory) return;
        firingPressed = value.isPressed;
        //playerController.isFiring = value.isPressed;
        //playerAnimator.SetBool(isFiringHash, value.isPressed);
        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    private void StartFiring()
    {
        if (equippedWeapon.WeaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        }

        playerAnimator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    private void StopFiring()
    {
        equippedWeapon.StopFiringWeapon();
        playerAnimator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
    }

    public void EquipWeapon(WeaponScriptable weaponScriptable)
    {
        if (!weaponScriptable) return;

        spawnedWeapon = Instantiate(weaponScriptable.itemPrefab, weaponSocketLocation);
        if (!spawnedWeapon) return;

        UnEquipWeapon();

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialize(this, weaponScriptable);
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        gripIKSocketLocation = equippedWeapon.GripLocation;
    }

    public void UnEquipWeapon()
    {
        if (!equippedWeapon) return;
        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
    }
}

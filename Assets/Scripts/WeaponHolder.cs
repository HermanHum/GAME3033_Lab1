using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    private GameObject weaponToSpawn;
    [SerializeField]
    private Sprite crossHairImage;
    [SerializeField]
    private Transform weaponSocketLocation;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}

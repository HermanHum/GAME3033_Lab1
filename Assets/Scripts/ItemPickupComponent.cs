using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupComponent : MonoBehaviour
{
    [SerializeField]
    private ItemScriptable pickupItem;

    [SerializeField, Tooltip("Manual Override for drop amount, if left at -1 it will use the amount from the scriptable object")]
    private int amount = -1;

    [SerializeField]
    private MeshRenderer propMeshRenderer;
    [SerializeField]
    private MeshFilter propMeshFilter;

    private ItemScriptable itemInstance;

    // Start is called before the first frame update
    private void Start()
    {
        InstantiateItem();
    }

    private void InstantiateItem()
    {
        itemInstance = Instantiate(pickupItem);
        if (amount > 0)
        {
            itemInstance.SetAmount(amount);
        }
        ApplyMesh();
    }

    private void ApplyMesh()
    {
        if (propMeshFilter) propMeshFilter.mesh = pickupItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        if (propMeshRenderer) propMeshRenderer.materials = pickupItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Add to inventory
        InventoryComponent playerInventory = other.GetComponent<InventoryComponent>();
        if (playerInventory) playerInventory.AddItem(itemInstance, amount);

        Destroy(gameObject);
    }
}

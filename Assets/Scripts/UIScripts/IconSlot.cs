using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconSlot : MonoBehaviour
{
    private ItemScriptable Item;

    private Button ItemButton;
    private TMP_Text ItemText;

    [SerializeField] private ItemSlotAmountCanvas AmountWidget;
    [SerializeField] private ItemSlotEquippedCanvas EquippedWidget;

    void Awake()
    {
        ItemButton = GetComponent<Button>();
        ItemText = GetComponentInChildren<TMP_Text>();
    }

    public void Initialize(ItemScriptable item)
    {
        Item = item;
        ItemText.text = Item.itemName;
        AmountWidget.Initialize(item);
        EquippedWidget.Initialize(item);

        ItemButton.onClick.AddListener(UseItem);
        Item.OnItemDestroyed += OnItemDestroyed;
    }

    public void UseItem()
    {
        Debug.Log($"{Item.itemName} used!");
        Item.UseItem(Item.Controller);
    }

    private void OnItemDestroyed()
    {
        Item = null;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (Item) Item.OnItemDestroyed -= OnItemDestroyed;
    }
}

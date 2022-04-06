using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquippableScriptable : ItemScriptable
{
    private bool isEquipped = false;
    public bool Equipped { get { return isEquipped; } set { isEquipped = value; OnEquipStatusChange?.Invoke(); } }

    public delegate void EquipStatusChange();
    public event EquipStatusChange OnEquipStatusChange;

    public override void UseItem(PlayerController playerController)
    {
        Equipped = !isEquipped;
    }
}

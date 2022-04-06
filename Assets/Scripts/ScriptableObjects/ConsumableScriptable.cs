using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable", order = 1)]
public class ConsumableScriptable : ItemScriptable
{
    public int effect = 0;

    public override void UseItem(PlayerController playerController)
    {
        // Check to see if the player is at max health, then return
        HealthComponent healthComponent = playerController.GetComponent<HealthComponent>();
        if (healthComponent.CurrentHealth >= healthComponent.MaxHealth) return;
        // Heal player with potion
        healthComponent.Heal(effect);

        ChangeAmount(-1);

        if (amountValue <= 0)
        {
            DeleteItem(playerController);
        }
    }
}

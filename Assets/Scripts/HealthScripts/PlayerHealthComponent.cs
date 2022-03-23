using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    private void Start()
    {
        PlayerEvents.InvokeOnHealthInitialized(this);
    }
}

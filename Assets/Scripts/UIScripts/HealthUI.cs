using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI currentHealthCountText;
    [SerializeField]
    TextMeshProUGUI maxHealthCountText;
    HealthComponent playerHealthComponent;

    private void OnHealthInitialized(HealthComponent healthComponent)
    {
        playerHealthComponent = healthComponent;
    }

    private void OnEnable()
    {
        PlayerEvents.OnHealthInitialized += OnHealthInitialized;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHealthInitialized -= OnHealthInitialized;
    }

    private void Update()
    {
        currentHealthCountText.text = playerHealthComponent.CurrentHealth.ToString("0");
        maxHealthCountText.text = playerHealthComponent.MaxHealth.ToString("0");
    }
}

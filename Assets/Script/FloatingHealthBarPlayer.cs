using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBarPlayer : MonoBehaviour
{
    public Slider slider;
    public Player player;
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    public void Update()
    {
        UpdateHealthBar(player.health, player.maxHealth);
    }

}

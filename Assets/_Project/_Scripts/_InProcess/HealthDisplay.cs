using System;
using UnityEngine;
using UnityEngine.UI; // Добавляем для работы со Slider
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;

public class HealthDisplay : MonoBehaviour
{

    public void UpdateHealthUI( float currentHealth, float maxHealth)
    {
             TMP_Text healthText = GetComponentInChildren<TMP_Text>();
             if (healthText != null)
             {
                 healthText.text = currentHealth + "/" + maxHealth;
             }
            Slider healthBar = GetComponentInChildren<Slider>();
            healthBar.value = currentHealth / maxHealth; // Обновляем значение слайдера

    }


}
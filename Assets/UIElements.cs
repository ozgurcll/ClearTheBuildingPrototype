using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIElements : MonoBehaviour
{
    public Image healthfill;
    public Image ammofill;

    private Attack playerAmmo;
    private Destroy playerHealth;

    private void Awake()
    {
        playerAmmo = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        playerHealth = playerAmmo.GetComponent<Destroy>();

    }

    void Update()
    {
        UpdateHealthFill();
        UpdateAmmoFill();
    }

    private void UpdateAmmoFill()
    {
        ammofill.fillAmount = (float)playerAmmo.GetAmmo / playerAmmo.GetClipSize;
    }

    private void UpdateHealthFill()
    {
        healthfill.fillAmount = (float)playerHealth.GetHealth / playerHealth.GetMaxHealth;
    }
}

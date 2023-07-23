using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera mainCamera;
    public Image healthImage;
    private float currentHealth;
    private float maxHealth;

    private void Start()
    {
        GetComponentInParent<Unit>().healthBar = this;
        maxHealth = GetComponentInParent<Unit>().hitPoints;
        currentHealth = maxHealth;
        mainCamera = Camera.main;
    }

    public void UpdateHealthBar(float takenDamage)
    {
        // Hasar miktarýna göre saðlýk çubuðunu güncelle
        currentHealth += takenDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        float fillAmount = currentHealth / maxHealth;
        healthImage.fillAmount = fillAmount;
        //Debug.Log("healthImage.fillAmount: " + healthImage.fillAmount);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    UpdateHealthBar(100);
        //}
        // UI elemanýný kameraya doðru döndürme
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}

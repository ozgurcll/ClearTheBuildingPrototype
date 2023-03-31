using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Health Setting")]
    public bool healthPowerUp = false;
    public int healthAmount = 25;

    [Header("Ammo Setting")]
    public bool ammoPowerUp = false;
    public int ammoAmount = 5;

    [Header("Transform Settings")]
    public Vector3 turnVector  = Vector3.zero;

    [Header("Scale Settings")]
    [SerializeField] private float period = 2f;
    [SerializeField] Vector3 scaleVector;
    private float scaleFactor;
    private Vector3 startScale;

    [Header("Audio Settings")]
    public AudioClip clipToPlay;
    private void Awake()
    {
        startScale = transform.localScale;
    }
    private void Start()
    {
        if(healthPowerUp && ammoPowerUp)
        {
            healthPowerUp = false;
            ammoPowerUp = false;
        }
        else if (healthPowerUp)
        {
            ammoPowerUp = false;
        }
        else if (ammoPowerUp)
        {
            healthPowerUp = false;
        }
    }
    void Update()
    {
        transform.Rotate(turnVector);
        SinusWawe();
    }

    private void SinusWawe()
    {
        if(period <= 0)
        {
            period = 0.1f;
        }
        float cycles = Time.timeSinceLevelLoad / period;
        const float piX2 = Mathf.PI * 2;

        float sinusWawe = Mathf.Sin(cycles * piX2);

        scaleFactor = sinusWawe / 2 + 1.5f;

        Vector3 offset = scaleFactor * scaleVector;

        transform.localScale = startScale + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(clipToPlay , transform.position);

            if (healthPowerUp)
            {
                other.gameObject.GetComponent<Destroy>().GetHealth += healthAmount;
            }
            else if (ammoPowerUp)
            {
                other.gameObject.GetComponent<Attack>().GetAmmo += ammoAmount;
            }
            Destroy(gameObject);
        }
    }
}

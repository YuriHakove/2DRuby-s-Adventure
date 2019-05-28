using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectenClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null && controller.Health < controller.maxHealth)
        {
            controller.ChangeHealth(1);
            Destroy(gameObject);
            controller.PlaySound(collectenClip);
        }
    }
}

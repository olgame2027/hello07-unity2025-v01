using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    [SerializeField] float damage = 5;
    [SerializeField] string ownerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem hs = other.GetComponent<HealthSystem>();
        if (hs != null && !other.CompareTag(ownerTag))
        {
            hs.TakeDamage(damage);
        }
    }
}

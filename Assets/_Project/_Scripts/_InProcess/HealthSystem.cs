using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] GameObject healthDisplay;

    private static readonly int IsDead = Animator.StringToHash("IsDead");
    [SerializeField] float healthPoints = 100f;
    [SerializeField] float maxHealthPoints = 100f;

    bool dead = false;

    public event Action OnDeath;

    public bool Dead
    {
        get { return dead; }
    }

    public void TakeDamage(float damage)
    {
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        healthDisplay.GetComponent<HealthDisplay>().UpdateHealthUI(healthPoints, maxHealthPoints);
        if (healthPoints == 0)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        healthPoints = Mathf.Min(healthPoints + heal, maxHealthPoints);
        healthDisplay.GetComponent<HealthDisplay>().UpdateHealthUI(healthPoints, maxHealthPoints);
    }

    private void Die()
    {
        if (Dead) return;
        dead = true;

        if (gameObject.GetComponent<Enemy>() != null)
        {
            healthDisplay.SetActive(false); //GetComponent<HealthDisplay>().UpdateHealthUI(healthPoints, maxHealthPoints);
            GameManager.Instance.BattleScore[gameObject.GetComponent<Enemy>().Type] += 1;
        }

        var animator = GetComponent<Animator>();
        if (animator)
        {
            animator.SetTrigger(IsDead);
        }

        OnDeath?.Invoke();

        // gameObject.SetActive(false);
    }
}
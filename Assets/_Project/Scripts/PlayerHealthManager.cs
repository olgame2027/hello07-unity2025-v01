using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int playerHealth;

    public bool gameover;

    public TMP_Text healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = 1000;
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerHealth.ToString();
        if (gameover)
            SceneManager.LoadScene("Level01");
    }

    public void Damage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
            gameover = true;
    }
}

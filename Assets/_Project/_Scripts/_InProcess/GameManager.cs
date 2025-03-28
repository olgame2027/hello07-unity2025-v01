using System;
using System.Collections.Generic;
using System.Linq;
using DialogueEditor;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton для доступа из других частей игры

    public InventoryManager inventory;
    public GameObject LevelPanel;
    private GameObject player;
    
    
    public TMP_Text goldText;
    public TMP_Text enemyText;
    public TMP_Text questText;
    public int questCount;
    public int playerExperience = 0;
    public Dictionary<EnemyType, int> BattleScore = new Dictionary<EnemyType, int>();    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        inventory = inventory.GetComponent<InventoryManager>();
        foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
        {
            BattleScore.Add(type, 0);
        }
        player.GetComponent<HealthSystem>().OnDeath += ShowLevelPanel;
        LevelPanel.SetActive(false);
    }
    private void Update()
    {
        goldText.text = playerExperience.ToString();
        enemyText.text = (BattleScore[EnemyType.Skeleton] + BattleScore[EnemyType.Goblin]).ToString();
        questText.text = questCount.ToString();

        //////////////////////////////////////////// TO DO
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     LevelPanel.SetActive(true);
        // }
    }
    
   
    public void ShowLevelPanel()
    {
        LevelPanel.SetActive(true);
        player.GetComponent<ThirdPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Time.timeScale =  0f ; // 0 - пауза, 1 - нормальная скорость
    }
    public void RebootLevel()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Level01");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Игра закрывается"); 
    }
}
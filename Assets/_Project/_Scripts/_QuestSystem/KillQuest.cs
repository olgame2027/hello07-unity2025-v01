using DialogueEditor;
using UnityEngine;
using UnityEngine.Serialization;

//[System.Serializable]
//[CreateAssetMenu(fileName = "Kill Quest",menuName = "Quests/New kill quest")]
public class KillQuest : Quest
{
    public EnemyType typeOfEnemy;
    public int requiredKills; // Требуемое количество боевых побед
    public int currentKills; // Текущее количество 

    public KillQuest(int requiredKills = 1, int currentKills = 0)
    {
        this.requiredKills = requiredKills;
        this.currentKills = currentKills;
    }

    public override void Evaluate()
    {
        currentKills = GameManager.Instance.BattleScore[typeOfEnemy];
        if (currentKills >= requiredKills)
        {
            isCompleted = true;
        }
    }
}

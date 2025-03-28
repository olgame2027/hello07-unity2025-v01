using DialogueEditor;
using UnityEngine;
using UnityEngine.Serialization;

public enum QuestType
{
    Kill,   // Убить врага
    Collect, // Найти предмет
    Explore // Исследовать локацию
}
//[System.Serializable]
public abstract class Quest : MonoBehaviour
{
    public string description; // Описание цели
    public bool isCompleted;      // Выполнена ли цель
    public bool isActive;         // Принято к испольнению
    public QuestType questType;   // Тип цели
//    public int rewardGold; // Награда за выполнение квеста
    public int rewardExp; // Опыт за выполнение квеста
    
    public ItemScriptableObject rewardItemType;
    public int rewardAmount; // Награда за выполнение квеста
    
    public NPCConversation startConversation;
    public NPCConversation finishConversation;

    public void SetActive()
    {
        isActive = true;
        // TO Do вывести в UI о принятых квестах
    }

    public void GetReward()
    {
        GameManager.Instance.inventory.AddItem(rewardItemType, rewardAmount);
        GameManager.Instance.playerExperience += rewardExp;
        GameManager.Instance.questCount += 1;
    }
    public abstract void Evaluate(); // Метод для проверки выполнения цели
}




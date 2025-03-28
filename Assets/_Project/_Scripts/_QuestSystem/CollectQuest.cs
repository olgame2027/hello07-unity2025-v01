using System.Linq;
using DialogueEditor;
using UnityEngine;
using UnityEngine.Serialization;


//[System.Serializable]
//[CreateAssetMenu(fileName = "Collect Quest",menuName = "Quests/New collect quest")]
public class CollectQuest : Quest
{
    public ItemType typeOfItem;
    public int requiredItems; // Требуемое количество предметов
    public int currentItems;  // Текущее количество предметов
    

    public CollectQuest(int requiredItems = 10, int currentItems = 0)
    {
        this.requiredItems = requiredItems;
        this.currentItems = currentItems;
    }

    public override void Evaluate()
    {
        currentItems = GameManager.Instance.inventory.slots.FirstOrDefault(slot => slot.item?.itemType == typeOfItem)?.amount ?? 0;
        if (currentItems >= requiredItems)
        {
            isCompleted = true;
        }
    }
}

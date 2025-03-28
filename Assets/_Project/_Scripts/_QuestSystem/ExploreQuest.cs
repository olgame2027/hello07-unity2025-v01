using DialogueEditor;
using UnityEngine;
using UnityEngine.Serialization;


//[System.Serializable]
//[CreateAssetMenu(fileName = "Explore Quest",menuName = "Quests/New explore quest")]
public class ExploreQuest : Quest
{
    
    public string locationName; // Название локации, которую нужно исследовать
    public bool isLocationVisited; // Посещена ли локация

    public ExploreQuest(string locationName = "", bool isLocationVisited = false)
    {
        this.locationName = locationName;
        this.isLocationVisited = isLocationVisited;
    }

    public override void Evaluate()
    {
        if (isLocationVisited)
        {
            isCompleted = true;
        }
    }
}
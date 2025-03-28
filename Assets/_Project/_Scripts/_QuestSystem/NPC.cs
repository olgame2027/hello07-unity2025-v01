
using System;
using System.Collections.Generic;
using System.Linq;
using DialogueEditor;
using UnityEngine;

public class NPC : MarkedObject
{
    public string npcName = "NPC"; // Имя NPC
    
    public List<Quest> quests; // Список квестов
    
    private int currentQuest;
    public NPCConversation CurrentConversation
        {
            get => currentQuest < quests.Count ? (quests[currentQuest].isActive ? quests[currentQuest].finishConversation : quests[currentQuest].startConversation) : FinelConversation; 
        }

    public NPCConversation FinelConversation;
    private void Awake()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true; // NPC как триггер
        }
    }

    private void Start()
    {
        quests = new List<Quest>();
        foreach (var c in gameObject.GetComponentsInChildren<Quest>())
        {
            quests.Add(c);
        }
        currentQuest = 0;
    }

    private void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    private void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StartDialogue()
    {
       if (CurrentConversation != null)
       {
           ConversationManager.Instance.StartConversation(CurrentConversation);
           ShowMouse();
       }
       else
       {
           Debug.Log("NPC is currently offline");  // TO DO сделать диалаог ни о чем
       }
    }

    public void EndDialogue()
    {
        if (ConversationManager.Instance.IsConversationActive && CurrentConversation != null)
        {
            ConversationManager.Instance.EndConversation();
            HideMouse();
        }
    }

    public override void LabelTurnOn()
    {
        ItemDisplay.instance.ShowItemName(transform, npcName);
    }
    
    public void UpdateQuest() //, QuestType questType, int amount
    {
            quests[currentQuest].Evaluate();
            ConversationManager.Instance.SetBool("isComplete", quests[currentQuest].isCompleted);
            if (quests[currentQuest].isCompleted)  currentQuest++;
    }
}
// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
  //  public GameObject crosshair;
    public GameObject UIPanel;
    public Transform inventoryPanel;
    public bool isOpened;
    public List<InventorySlot> slots = new List<InventorySlot>();
    private void Awake()
    {
        UIPanel.SetActive(false);
        
    }
    void Start()
    {
        
        for(int i = 0; i < inventoryPanel.childCount; i++)
        {
            if(inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
       // UIPanel.SetActive(false);
    }
    
    public bool AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach(InventorySlot slot in slots)
        {
            if(slot.item == _item)
            {
                if(slot.amount + _amount <= _item.maximumAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmount.text = slot.amount.ToString();
                    return true;
                }
                break;
        
            }
        }
        foreach(InventorySlot slot in slots)
        {
            if(slot.isEmpty)//false
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = _amount.ToString();
                return true;
            }
        }
        return false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpened = ! isOpened;
            if(isOpened)
            {
                UIPanel.SetActive(true);
                
                
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale =  0f ; // 0 - пауза, 1 - нормальная скорость
                
                
            //    crosshair.SetActive(false);
            }
            else
            {
               UIPanel.SetActive(false);
               
               
               Cursor.lockState = CursorLockMode.Locked;
               Cursor.visible = false; 
               Time.timeScale =  1f ; // 0 - пауза, 1 - нормальная скорость
         
               
               //    crosshair.SetActive(true);
            }
        }
     }      

}

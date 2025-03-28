using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// IPointerDownHandler - Следит за нажатиями мышки по объекту на котором висит этот скрипт
/// IPointerUpHandler - Следит за отпусканием мышки по объекту на котором висит этот скрипт
/// IDragHandler - Следит за тем не водим ли мы нажатую мышку по объекту
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerClickHandler
{
    public InventorySlot oldSlot;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Находим скрипт InventorySlot в слоте в иерархии
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        //Делаем картинку прозрачнее
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        // чтобы нажатия мышкой не игнорировали эту картинку
        GetComponentInChildren<Image>().raycastTarget = false;
        // Делаем DraggableObject ребенком UIPanel(del InventoryPanel) чтобы он был над другими слотами инвенторя 
        transform.SetParent(transform.parent.parent.parent);  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        if (eventData.clickCount == 2 && eventData.button == PointerEventData.InputButton.Left)
        {
            InventorySlot slot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent
                .GetComponent<InventorySlot>();
            if (slot != null && !slot.isEmpty && slot.item is FoodItem fi)
            {
                // увеличить здоровье
                player.GetComponent<HealthSystem>().Heal(fi.healAmount);
                // уменьшить количество на 1
                slot.amount--;
                if (slot.amount == 0)
                {
                    NullifySlotData(slot);
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        // Делаем картинку опять не прозрачной
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // И чтобы мышка опять могла ее засечь
        GetComponentInChildren<Image>().raycastTarget = true;

        //Поставить DraggableObject обратно в свой старый слот
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        //Если мышка отпущена над объектом по имени UIPanel, то...
        if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel")
        {
            // Выброс объектов из инвентаря - Спавним префаб обекта перед персонажем
            GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward,
                Quaternion.identity);
            // Устанавливаем количество объектов такое какое было в слоте
            itemObject.GetComponent<Item>().amount = oldSlot.amount;
            // убираем значения InventorySlot
            NullifySlotData(oldSlot);
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "HealingPanel") 
        {
            // если это fooditem, то отхилится
            if (oldSlot.item.itemType == ItemType.Food)
            {
                //GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
                HealthSystem healthSystem = player.gameObject.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    float amount = (oldSlot.item as FoodItem)?.healAmount ??  (oldSlot.item as GarbageItem)?.harmAmount ?? 0f;
                    healthSystem.Heal(amount);
                    oldSlot.amount--;
                    oldSlot.itemAmount.text = oldSlot.amount.ToString();
                    if (oldSlot.amount == 0)
                    {
                        NullifySlotData(oldSlot);
                    }
                }
            }
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() !=
                 null)
        {
            //Перемещаем данные из одного слота в другой
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent
                .GetComponent<InventorySlot>());
        }
    }

    void NullifySlotData(InventorySlot oldSlot)
    {
        // убираем значения InventorySlot
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconGO.GetComponent<Image>().sprite = null;
        oldSlot.itemAmount.text = "";
    }

    void ExchangeSlotData(InventorySlot newSlot)
    {
        // Временно храним данные newSlot в отдельных переменных
        ItemScriptableObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
       // GameObject iconGO = newSlot.iconGO;
        Sprite iconGO = newSlot.iconGO.GetComponent<Image>().sprite;
        TMP_Text itemAmount = newSlot.itemAmount;

        // Заменяем значения newSlot на значения oldSlot
        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
            newSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.itemAmount.text = "";
        }

        newSlot.isEmpty = oldSlot.isEmpty;

        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(iconGO); //(iconGO.GetComponent<Image>().sprite);
            oldSlot.itemAmount.text = amount.ToString();
           
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
            oldSlot.itemAmount.text = "";
        }

        oldSlot.isEmpty = isEmpty;
    }
}
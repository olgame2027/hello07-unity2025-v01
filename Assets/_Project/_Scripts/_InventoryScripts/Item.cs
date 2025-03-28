//using System.Collections;

using System;
using System.Collections.Generic;
using UnityEngine;

public class Item : MarkedObject
{
   public ItemScriptableObject item;
   public int amount;
   
   private Outline outline;

   private void Awake()
   {
      outline = GetComponent<Outline>();
      outline.OutlineWidth = 0f;
   }
   public override void LabelTurnOn()
   {
      ItemDisplay.instance.ShowItemName(transform, item.itemName);
      outline.OutlineWidth = 5f;
   }

   public override void LabelTurnOff()
   {
       ItemDisplay.instance.HideItemName();
       outline.OutlineWidth = 0f;
   }
}

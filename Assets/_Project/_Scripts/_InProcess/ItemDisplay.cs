using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Cinemachine;

public class ItemDisplay : MonoBehaviour
{
   public static ItemDisplay instance;
   public TMP_Text itemLabel;
   private Transform target;
   private Canvas canvas;
   private CinemachineBrain cineBrain;

   private void Awake()
   {
      instance = this;
      canvas = itemLabel.GetComponentInParent<Canvas>();
      cineBrain = Camera.main.GetComponent<CinemachineBrain>(); // Получаем CinemachineBrain с главной камеры
      itemLabel.gameObject.SetActive(false);
   }
   
   private void LateUpdate()
   {
      if (target != null && cineBrain != null)
      {
         Camera cam = cineBrain.OutputCamera;
         if (cam != null)
         {
            Vector3 labelPosition = target.position + Vector3.up * 1.5f;
            canvas.transform.position = labelPosition;
            Vector3 directionToCamera = cam.transform.position - labelPosition;
            canvas.transform.rotation = Quaternion.LookRotation(-directionToCamera, Vector3.up);
         }
      }
   }
   public void ShowItemName(Transform item, string name)
   {
       target = item;
       itemLabel.text = name;
       itemLabel.gameObject.SetActive(true);
   }
   
   public void HideItemName()
   {
       itemLabel.gameObject.SetActive(false);
       target = null;
   }
   
}
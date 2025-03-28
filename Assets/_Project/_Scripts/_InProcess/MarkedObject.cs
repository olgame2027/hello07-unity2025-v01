using UnityEngine;


    public abstract class MarkedObject :MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                LabelTurnOn();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                LabelTurnOff();
            }
        }

        public abstract void LabelTurnOn();

        public virtual void LabelTurnOff()
        {
            ItemDisplay.instance.HideItemName();
        }
    }

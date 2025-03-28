using UnityEngine;
using System;
using DialogueEditor;
using StarterAssets;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    // [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject itemDisplay;
    HealthSystem _healthSystem;
    [SerializeField] private InventoryManager _inventoryManager;

    // События для предметов
    // public static event Action<Transform, string> OnItemEntered;
    // public static event Action OnItemExited;
    //
    // // События для врагов
    // public static event Action<Enemy> OnEnemyContact;
    //
    // // События для NPC
    // public static event Action<NPC> OnNPCEntered;
    // public static event Action<NPC> OnNPCExited;

    private Animator _animator;
    private StarterAssetsInputs _input;

    private bool _hasAnimator;
    private int _animIDAttack;
    private Item _collectableItem;
    private NPC _npc;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //   _inventoryManager = inventory.GetComponent<InventoryManager>();
        _healthSystem = GetComponent<HealthSystem>();
        _hasAnimator = TryGetComponent(out _animator);
        _input = GetComponent<StarterAssetsInputs>();
        _animIDAttack = Animator.StringToHash("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.attack && _npc == null)
        {
            int _hitIndex = Random.Range(0, 3);
            _animator.SetInteger(_animIDAttack, _hitIndex + 1);
            weapon.SetActive(true);
        }
        else
        {
            _animator.SetInteger(_animIDAttack, 0);
            weapon.SetActive(false);
        }
        
        // if (_npc != null && ConversationManager.Instance.IsConversationActive && _input.ESC)
        // {
        //     _npc.EndDialogue();
        //     _npc = null;
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        _collectableItem = other.GetComponent<Item>();
        _npc = other.GetComponent<NPC>();
     
    }

    private void OnTriggerStay(Collider other)
    {
        if (_collectableItem != null && _input.collect) //Input.GetKeyDown(KeyCode.E))
        {
            if (_inventoryManager.AddItem(_collectableItem.item, _collectableItem.amount))
            {
                //  при подборе
                ItemDisplay.instance.HideItemName(); // убираем с экран надпись
                Destroy(_collectableItem.gameObject); // Удаляем предмет
                _collectableItem = null;
            }
        }
        else
        {
            if (_npc != null && _input.dialog)
            {
                _npc.StartDialogue();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_collectableItem != null)
        {
            _collectableItem = null;
        }

        if (_npc != null && ConversationManager.Instance.IsConversationActive)
        {
            _npc.EndDialogue();
            _npc = null;
        }
    }
}

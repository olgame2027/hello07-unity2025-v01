using UnityEngine;
using System;

public enum EnemyType{Skeleton, Goblin}
public class Enemy : MarkedObject
{
    public Transform patrolRoute;
    HealthSystem _healthSystem;
    [SerializeField] public GameObject weapon;
    private GameObject player;
    public EnemyType Type = EnemyType.Skeleton;
    void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>().OnDeath += SetWin;
        //  _animIDAttack = Animator.StringToHash("Attack"); 
    }

    private void SetWin()
    {
        GetComponent<Animator>().SetBool("isWin", true);
    }

    public override void LabelTurnOn()
    {
        ItemDisplay.instance.ShowItemName(transform, "Skeleton");
    }

    // public override void LabelTurnOff()
    // {
    //     ItemDisplay.instance.HideItemName();
    //    
    // }

    

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharaterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    CharaterStats myStats;
    public float attackDelay = .6f;
    const float combatCooldown = 5;
    float lastAttackTime;
    public bool InCombat {get;private set;}
    public event System.Action OnAttack;

    void Start(){
        myStats = GetComponent<CharaterStats>();
    }
    public void Attack(CharaterStats targetStats){
        if(attackCooldown<=0f){
            StartCoroutine(DoDamage(targetStats,attackDelay));
            if(OnAttack != null){
                OnAttack();
            }
            attackCooldown = 1f/attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
        
    }

    void Update(){
        attackCooldown -=Time.deltaTime;
        if(Time.time - lastAttackTime > combatCooldown){
            InCombat = false;
        }
    }

    IEnumerator DoDamage(CharaterStats stats,float delay){
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
        if(stats.currentHealth <=0){
            InCombat = false;
        }
    }
}

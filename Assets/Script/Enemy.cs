using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : Interactable
{
    CharaterStats myStats;
PlayerManager playerManager;
    void Start(){
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharaterStats>();
    }
    public override void Interact(){
        base.Interact();
        Debug.Log("caocaocao");
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if(playerCombat!=null){
            playerCombat.Attack(myStats);
        }
    }
}

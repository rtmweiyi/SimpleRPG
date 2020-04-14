using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform InteractionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact(){
        Debug.Log("Interact with "+transform.name);
    }

    void Update(){
        if(isFocus && !hasInteracted){
            Debug.Log("sbsbsbsbsbsbsb");
            float distance = Vector3.Distance(player.position,InteractionTransform.position);
            if(distance<=radius){
                Debug.Log("INTERACT");
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform){
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused(){
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if(InteractionTransform==null)
            InteractionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionTransform.position,radius);
    }
}

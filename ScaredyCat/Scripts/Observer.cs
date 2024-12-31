using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        // check that the player is in range
        if (other.transform == player)
        {
            m_IsPlayerInRange = true; 
        }

    }

    void OnTriggerExit(Collider other)
    {
        // might be a wall in the way, detect when player leaves trigger 
        if (other.transform == player) 
        {
            m_IsPlayerInRange = false; 
        }
    }

    void Update()
    {
        if(m_IsPlayerInRange)
        {
            // calculates direction between POV object and player
            Vector3 direction = player.position - transform.position + Vector3.up;
            // creates a ray with the origin and direction  
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}

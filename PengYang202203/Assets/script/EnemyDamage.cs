using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class EnemyDamage : MonoBehaviour
{
    public int livesRemaining;
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Triggered");
            LoseLife();
        }

        if (collision.name == "hitbox")
        {
            Debug.Log("Triggered2");
            LoseLife();
        }
    }
    public void LoseLife()
    {
        if (livesRemaining > 0)
        {
            livesRemaining--;
        }
        
        if(livesRemaining == 0)
        {
            Debug.Log("Death");
            FindObjectOfType<LevelManager>().Restart();
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

// Stick the player to the platform so not move by himself while on it.

public class StickyPlatform : MonoBehaviour
{
    const string playerTag = "Player";
    const string borderTag = "Border";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide1");
        if (collision.CompareTag(playerTag))
        {
            transform.SetParent(collision.gameObject.transform);

            Debug.Log("collide2");
        }
        else if (collision.CompareTag(borderTag))
        {
            transform.SetParent(null);
        }
    }

    /*    private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(playerTag))
    
            {
                transform.SetParent(null);
               // collision.gameObject.transform.SetParent(null);
            }
        }*/
}

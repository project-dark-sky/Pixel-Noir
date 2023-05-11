using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PowerUpsScript : MonoBehaviour
{
    [SerializeField]
    AllowedTiles allowedTiles = null;

    [SerializeField]
    TileBase tile = null;
    const string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if he took the powerup he can now walk in this tile.
        if (collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject);
            allowedTiles.InsertTile(tile);
        }
    }
}

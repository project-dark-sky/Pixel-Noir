using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoatScript : MonoBehaviour
{
    const string playerTag = "Player";

    [SerializeField]
    AllowedTiles allowedTiles = null;

    [SerializeField]
    TileBase waterTile = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if he took the boat the player can walk on the water
        if (collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject);
            allowedTiles.InsertTile(waterTile);
        }
    }
}

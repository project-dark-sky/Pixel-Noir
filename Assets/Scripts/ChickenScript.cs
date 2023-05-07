using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChickenScript : MonoBehaviour
{
    const string playerTag = "Player";

    [SerializeField]
    AllowedTiles allowedTiles = null;

    [SerializeField]
    TileBase stoneTile = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject);
            allowedTiles.InsertTile(stoneTile);
        }
    }
}

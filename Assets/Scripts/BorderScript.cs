using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    const string boatTag = "Platform";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(boatTag))
        {
            Collider collider = GetComponent<Collider>();
            collider.isTrigger = false;
        }
    }
}

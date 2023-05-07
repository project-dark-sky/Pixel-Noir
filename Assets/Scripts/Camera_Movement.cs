using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Script represents the campera follows the player/target
public class Camera_Movement : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void Update()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
    }
}

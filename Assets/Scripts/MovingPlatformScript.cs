using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Script represents the moving platforms from point1 to point2
public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] targetPoints;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float eps = .1f;

    private int currentPointIndex = 0;

    private bool isMoving = false;
    const string PlayerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = collision.CompareTag(PlayerTag);
        Debug.Log("collider1");
        if (isPlayer)
        {
            isMoving = true;
            Debug.Log("collider2");
        }
    }

    void Update()
    {
        // check the distance from the platform and current point

        bool notReachedTarget =
            Vector2.Distance(targetPoints[currentPointIndex].transform.position, transform.position)
            < eps;

        if (notReachedTarget && isMoving)
        {
            currentPointIndex++;
            isMoving = false;
            if (currentPointIndex >= targetPoints.Length)
            {
                currentPointIndex = 0;
            }
        }
        //move towards current point
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoints[currentPointIndex].transform.position,
            Time.deltaTime * speed
        );
    }
}

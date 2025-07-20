using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform targetPosition;  // bottom position
    public float speed = 2f;

    private bool isGoingDown = false;

    void FixedUpdate()
    {
        if (isGoingDown)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition.position,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                isGoingDown = false;
                DoorController.Instance.OpenDoors();  // Open doors here
            }
        }
    }
}

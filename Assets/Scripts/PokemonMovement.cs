using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;

    private int currentPoint = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentPoint];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Rotar hacia el objetivo
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentPoint = (currentPoint + 1) % waypoints.Length;
        }
    }
}
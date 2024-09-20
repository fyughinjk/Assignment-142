using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    public GameObject player; // Assign the player GameObject here
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player
    public float detectionRange = 10f; // How far the enemy can "see"
    public LayerMask obstacles; // LayerMask to define what is considered an obstacle (walls, etc.)

    private Renderer enemyRenderer; // To handle visibility

    void Start()
    {
        // Get the Renderer component to control visibility
        enemyRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (player != null)
        {
            // Check if the player is within detection range
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= detectionRange && HasLineOfSight())
            {
                // If the enemy has line of sight, move towards the player and make the enemy visible
                MoveTowardsPlayer();
                enemyRenderer.enabled = true; // Make enemy visible
            }
            else
            {
                // If the enemy does not have line of sight, make it invisible
                enemyRenderer.enabled = false; // Make enemy invisible
            }
        }
    }

    // Function to move the enemy towards the player
    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // Function to check if the enemy has line of sight to the player
    bool HasLineOfSight()
    {
        // Cast a ray from the enemy to the player to check if there are obstacles in the way
        RaycastHit hit;
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange, ~obstacles))
        {
            // If the ray hits the player, there's line of sight
            if (hit.transform == player.transform)
            {
                return true;
            }
        }

        // If the ray hits anything else, there's no line of sight
        return false;
    }
}

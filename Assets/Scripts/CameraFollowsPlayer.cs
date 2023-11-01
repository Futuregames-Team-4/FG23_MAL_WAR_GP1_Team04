using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    Transform playerTransform;
    Transform tf;

    Vector3 cameraOffset = new Vector3(-5, 5, -5);

    private void Awake()
    {
        tf = GetComponent<Transform>();

        // Find the player using the "Player" tag.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Check if the player was found
        if (playerTransform == null)
        {
            Debug.LogError("Player not found. Ensure the player has the correct tag.");
        }
    }

    void Update()
    {
        if (playerTransform != null) // Make sure the player transform is assigned
        {
            tf.position = playerTransform.position + cameraOffset;
        }
    }
}


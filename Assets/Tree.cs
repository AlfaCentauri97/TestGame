using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Wind Settings")]
    public float swaySpeed = 1.0f; // Speed of the swaying
    public float swayAmount = 5.0f; // Amount of rotation in degrees

    private float initialRotationZ;

    void Start()
    {
        // Store the initial rotation to use as a baseline
        initialRotationZ = transform.eulerAngles.z;
    }

    void Update()
    {
        // Calculate the sway angle using a sine wave
        float swayAngle = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // Apply the rotation to the tree
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, initialRotationZ + swayAngle);
    }
}
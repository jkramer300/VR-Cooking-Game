using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HebelRotation : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    HingeJoint hinge;
    public float leverOutput;
    public float minValue, maxValue;
    public Transform leverTransform;   // Hebel-Mesh
    public float minAngle = 0f;        // Untere Grenze (z. B. "aus")
    public float maxAngle = 45f;       // Obere Grenze (z. B. "an")
    public float rotationSpeed = 5f;   // Wie schnell der Hebel folgt

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        float betweenZeroAndOne = (hinge.angle - hinge.limits.min) / (hinge.limits.max - hinge.limits.min);
        leverOutput = minValue + (maxValue - minValue) * betweenZeroAndOne;

        Debug.Log($"Angle is:{leverOutput}");
    }
}

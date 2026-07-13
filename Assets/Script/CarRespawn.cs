using UnityEngine;

public class CarRespawn : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 lastSafePosition;
    private Quaternion lastSafeRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        lastSafePosition = transform.position;
        lastSafeRotation = transform.rotation;
    }

    public void SaveCheckpoint(Transform checkpoint)
    {
        lastSafePosition = checkpoint.position;
        lastSafeRotation = checkpoint.rotation;
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = lastSafePosition;
        transform.rotation = lastSafeRotation;
    }
}
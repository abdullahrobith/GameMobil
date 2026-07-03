using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    [Header("Wheel Meshes")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    [Header("Waypoint")]
    public Transform[] waypoints;

    [Header("Driving")]
    public float minMotorTorque = 7000f;
    public float maxMotorTorque = 8500f;
    public float maxSpeed = 80f;
    public float maxSteerAngle = 30f;
    public float waypointDistance = 6f;

    [Header("Obstacle Detection")]
    public float sensorLength = 8f;
    public LayerMask obstacleLayer;

    [Header("Recovery")]
    public float stuckTime = 3f;
    public float reverseTime = 1.5f;
    public float reverseTorque = -4000f;

    private Rigidbody rb;

    private float motorTorque;
    private int currentWaypoint = 0;

    private float stuckTimer = 0f;
    private bool isRecovering = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        motorTorque = Random.Range(minMotorTorque, maxMotorTorque);
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0)
            return;

        if (isRecovering)
        {
            UpdateAllWheels();
            return;
        }

        Drive();

        DetectObstacle();

        CheckStuck();

        UpdateAllWheels();
    }

    void Drive()
    {
        Transform target = waypoints[currentWaypoint];

        Vector3 localTarget =
            transform.InverseTransformPoint(target.position);

        float steer =
            Mathf.Clamp(
                (localTarget.x / localTarget.magnitude) * maxSteerAngle,
                -maxSteerAngle,
                maxSteerAngle);

        frontLeftCollider.steerAngle = steer;
        frontRightCollider.steerAngle = steer;

        float torque = motorTorque;

        // Melambat saat tikungan
        if (Mathf.Abs(steer) > 15f)
            torque *= 0.7f;

        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rearLeftCollider.motorTorque = torque;
            rearRightCollider.motorTorque = torque;
        }
        else
        {
            rearLeftCollider.motorTorque = 0;
            rearRightCollider.motorTorque = 0;
        }

        float distance =
            Vector3.Distance(transform.position, target.position);

        if (distance < waypointDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }
    }

    void DetectObstacle()
    {
        RaycastHit hit;

        Vector3 origin =
            transform.position +
            transform.forward * 1.5f +
            Vector3.up * 0.5f;

        Debug.DrawRay(origin,
            transform.forward * sensorLength,
            Color.red);

        if (Physics.Raycast(origin,
            transform.forward,
            out hit,
            sensorLength,
            obstacleLayer))
        {
            // Belok sedikit agar menghindari tembok
            frontLeftCollider.steerAngle += 20f;
            frontRightCollider.steerAngle += 20f;

            rearLeftCollider.motorTorque *= 0.5f;
            rearRightCollider.motorTorque *= 0.5f;
        }
    }

    void CheckStuck()
    {
        if (rb.linearVelocity.magnitude < 1f)
        {
            stuckTimer += Time.fixedDeltaTime;

            if (stuckTimer >= stuckTime)
            {
                StartCoroutine(RecoverRoutine());
            }
        }
        else
        {
            stuckTimer = 0;
        }
    }

    IEnumerator RecoverRoutine()
    {
        isRecovering = true;

        rearLeftCollider.motorTorque = 0;
        rearRightCollider.motorTorque = 0;

        yield return new WaitForSeconds(0.2f);

        float timer = 0;

        while (timer < reverseTime)
        {
            timer += Time.deltaTime;

            rearLeftCollider.motorTorque = reverseTorque;
            rearRightCollider.motorTorque = reverseTorque;

            frontLeftCollider.steerAngle = 0;
            frontRightCollider.steerAngle = 0;

            yield return null;
        }

        rearLeftCollider.motorTorque = 0;
        rearRightCollider.motorTorque = 0;

        Vector3 direction =
            waypoints[currentWaypoint].position -
            transform.position;

        direction.y = 0;

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                1f);

        yield return new WaitForSeconds(0.3f);

        stuckTimer = 0;

        isRecovering = false;
    }

    void UpdateAllWheels()
    {
        UpdateWheel(frontLeftCollider, frontLeftWheel);
        UpdateWheel(frontRightCollider, frontRightWheel);
        UpdateWheel(rearLeftCollider, rearLeftWheel);
        UpdateWheel(rearRightCollider, rearRightWheel);
    }

    void UpdateWheel(WheelCollider collider, Transform wheel)
    {
        Vector3 pos;
        Quaternion rot;

        collider.GetWorldPose(out pos, out rot);

        wheel.position = pos;
        wheel.rotation = rot * Quaternion.Euler(0, 0, 90);
    }
}
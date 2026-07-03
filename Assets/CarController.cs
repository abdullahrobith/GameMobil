using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float MoveSpeed = 8000f;
    public float MaxSpeed = 80f;
    public float SteerAngle = 30f;
    public float BrakeForce = 3000f;

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

    [Header("Audio")]
    public AudioSource engineAudio;

    [Header("Engine Sound")]
    public float idlePitch = 0.8f;
    public float maxPitch = 2.0f;
    public float pitchSmoothness = 2f;

    private Rigidbody rb;

    private float vertical;
    private float horizontal;
    private bool braking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        if (engineAudio != null)
        {
            engineAudio.loop = true;
            engineAudio.pitch = idlePitch;

            if (!engineAudio.isPlaying)
                engineAudio.Play();
        }
    }

    void Update()
    {
        vertical = Keyboard.current.wKey.isPressed ? 1 :
                Keyboard.current.sKey.isPressed ? -1 : 0;

        horizontal = Keyboard.current.aKey.isPressed ? -1 :
                    Keyboard.current.dKey.isPressed ? 1 : 0;

        braking = Keyboard.current.spaceKey.isPressed;

        UpdateEngineSound();

        UpdateWheel(frontLeftCollider, frontLeftWheel);
        UpdateWheel(frontRightCollider, frontRightWheel);
        UpdateWheel(rearLeftCollider, rearLeftWheel);
        UpdateWheel(rearRightCollider, rearRightWheel);
    }

    void FixedUpdate()
    {
        Move();
        Steer();
        Brake();
        LimitSpeed();
    }

    void Move()
    {
        rearLeftCollider.motorTorque = vertical * MoveSpeed;
        rearRightCollider.motorTorque = vertical * MoveSpeed;
    }

    void Steer()
    {
        float steer = horizontal * SteerAngle;

        frontLeftCollider.steerAngle = steer;
        frontRightCollider.steerAngle = steer;
    }

    void Brake()
    {
        float brake = braking ? BrakeForce : 0f;

        frontLeftCollider.brakeTorque = brake;
        frontRightCollider.brakeTorque = brake;
        rearLeftCollider.brakeTorque = brake;
        rearRightCollider.brakeTorque = brake;
    }

    void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > MaxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * MaxSpeed;
        }
    }

    void UpdateEngineSound()
    {
        if (engineAudio == null) return;

        float throttle =
            Mathf.Abs(vertical);

        float targetPitch =
            Mathf.Lerp(
                idlePitch,
                maxPitch,
                throttle
            );

        engineAudio.pitch =
            Mathf.Lerp(
                engineAudio.pitch,
                targetPitch,
                pitchSmoothness * Time.deltaTime
            );
    }

    void HandleEngineSound()
    {
        if (engineAudio == null) return;

        bool movingInput = Mathf.Abs(vertical) > 0.1f;

        if (movingInput)
        {
            if (!engineAudio.isPlaying)
            {
                engineAudio.Play();
            }
        }
        else
        {
            if (engineAudio.isPlaying)
            {
                engineAudio.Stop();
            }
        }
    }

    void UpdateWheel(WheelCollider collider, Transform wheel)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        wheel.position = position;

        // Koreksi rotasi roda
        wheel.rotation = rotation * Quaternion.Euler(0, 0, 90);
    }
}
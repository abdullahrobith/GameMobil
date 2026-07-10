using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float MoveSpeed = 12000f;
    public float ReverseSpeed = 6000f;
    public float MaxSpeed = 120f;
    public float SteerAngle = 30f;
    public float BrakeForce = 2500f;


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


    // INPUT
    private float horizontal;

    private bool gasPressed;
    private bool brakePressed;


    // sumber input tombol
    private bool gasPressedByButton;
    private bool brakePressedByButton;


    [Header("UI")]
    public TMP_Text speedText;


    [Header("Mobile")]
    public Joystick joystick;


    private RaceProgress progress;



    void Start()
    {
        rb = GetComponent<Rigidbody>();

        progress = GetComponent<RaceProgress>();

        rb.centerOfMass = new Vector3(0, -0.5f, 0);



        if(engineAudio != null)
        {
            engineAudio.loop = true;
            engineAudio.pitch = idlePitch;

            if(!engineAudio.isPlaying)
                engineAudio.Play();
        }
    }



    void Update()
    {
        GetSteeringInput();

        KeyboardInput();

        UpdateEngineSound();

        UpdateSpeed();


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



    // =====================================
    // STEERING
    // =====================================

    void GetSteeringInput()
    {
        float keyboardHorizontal = 0;


        if(Keyboard.current != null)
        {
            keyboardHorizontal =
                Keyboard.current.aKey.isPressed ? -1 :
                Keyboard.current.dKey.isPressed ? 1 : 0;
        }



        if(joystick != null)
        {
            horizontal = joystick.Horizontal;
        }
        else
        {
            horizontal = keyboardHorizontal;
        }
    }



    // =====================================
    // KEYBOARD TEST
    // =====================================

    void KeyboardInput()
    {
        if(Keyboard.current == null)
            return;



        // GAS keyboard W
        if(Keyboard.current.wKey.isPressed)
        {
            gasPressed = true;
        }
        else if(!gasPressedByButton)
        {
            gasPressed = false;
        }



        // REM / MUNDUR keyboard S atau SPACE
        if(
            Keyboard.current.sKey.isPressed ||
            Keyboard.current.spaceKey.isPressed
          )
        {
            brakePressed = true;
        }
        else if(!brakePressedByButton)
        {
            brakePressed = false;
        }
    }



    // =====================================
    // GAS BUTTON
    // =====================================

    public void GasDown()
    {
        gasPressedByButton = true;

        gasPressed = true;

        Debug.Log("GAS DITEKAN");
    }



    public void GasUp()
    {
        gasPressedByButton = false;

        gasPressed = false;

        Debug.Log("GAS DILEPAS");
    }



    // =====================================
    // BRAKE BUTTON
    // =====================================

    public void BrakeDown()
    {
        brakePressedByButton = true;

        brakePressed = true;

        Debug.Log("REM DITEKAN");
    }



    public void BrakeUp()
    {
        brakePressedByButton = false;

        brakePressed = false;

        Debug.Log("REM DILEPAS");
    }



    // =====================================
    // MOVEMENT
    // =====================================

    void Move()
    {
        float torque = 0;


        float speed =
            rb.linearVelocity.magnitude;



        // GAS MAJU
        if(gasPressed)
        {
            torque = MoveSpeed;
        }



        // BRAKE SAAT DIAM = MUNDUR
        if(brakePressed)
        {
            if(speed < 2f)
            {
                torque = -ReverseSpeed;
            }
        }



        rearLeftCollider.motorTorque = torque;

        rearRightCollider.motorTorque = torque;
    }



    // =====================================
    // STEERING
    // =====================================

    void Steer()
    {
        float steer =
            horizontal * SteerAngle;


        frontLeftCollider.steerAngle = steer;

        frontRightCollider.steerAngle = steer;
    }



    // =====================================
    // BRAKE
    // =====================================

    void Brake()
    {
        float brake = 0;


        // hanya rem jika mobil sedang bergerak
        if(brakePressed &&
           rb.linearVelocity.magnitude > 2f)
        {
            brake = BrakeForce;
        }



        frontLeftCollider.brakeTorque = brake;
        frontRightCollider.brakeTorque = brake;

        rearLeftCollider.brakeTorque = brake;
        rearRightCollider.brakeTorque = brake;
    }



    // =====================================
    // SPEED LIMIT
    // =====================================

    void LimitSpeed()
    {
        if(rb.linearVelocity.magnitude > MaxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized *
                MaxSpeed;
        }
    }



    // =====================================
    // ENGINE SOUND
    // =====================================

    void UpdateEngineSound()
    {
        if(engineAudio == null)
            return;



        float throttle =
            gasPressed ? 1 : 0;



        float targetPitch =
            Mathf.Lerp(
                idlePitch,
                maxPitch,
                throttle);



        engineAudio.pitch =
            Mathf.Lerp(
                engineAudio.pitch,
                targetPitch,
                pitchSmoothness *
                Time.deltaTime);
    }



    // =====================================
    // WHEEL UPDATE
    // =====================================

    void UpdateWheel(
        WheelCollider collider,
        Transform wheel)
    {
        if(collider == null ||
           wheel == null)
            return;



        collider.GetWorldPose(
            out Vector3 position,
            out Quaternion rotation);



        wheel.position = position;


        wheel.rotation =
            rotation *
            Quaternion.Euler(0,0,90);
    }



    // =====================================
    // SPEED UI
    // =====================================

    void UpdateSpeed()
    {
        if(speedText == null)
            return;



        float speed =
            rb.linearVelocity.magnitude *
            3.6f;



        speedText.text =
            Mathf.RoundToInt(speed)
            + " KM/H";
    }
}
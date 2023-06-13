using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    public float forwardSpeed = 0;

    public const float MaxSpeedBoost = 5300 / 100;
    Rigidbody _rb;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    // Boost settings
    public float boostForce = 1000f;      // The force applied during the boost
    public float boostDuration = 2f;      // The duration of the boost
    public float boostCooldown = 5f;      // The cooldown period between boosts

    private bool isBoosting = false;      // Indicates if the boost is currently active
    private float boostTimer = 0f;        // Timer for tracking the boost duration
    private float cooldownTimer = 0f;     // Timer for tracking the boost cooldown

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        forwardSpeed = Vector3.Dot(_rb.velocity, transform.forward);
        forwardSpeed = (float)System.Math.Round(forwardSpeed, 2);

        // Check for boost activation
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting && cooldownTimer <= 0f)
        {
            StartBoost();
        }

        // Update boost timer
        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0f)
            {
                EndBoost();
            }
        }

        // Update boost cooldown
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void StartBoost()
    {
        isBoosting = true;
        boostTimer = boostDuration;
        ApplyBoostForce();

        // TODO: Add visual/audio effects or any other actions for the boost start

        Debug.Log("Boost activated!");
    }

    private void EndBoost()
    {
        isBoosting = false;
        cooldownTimer = boostCooldown;

        // TODO: Add visual/audio effects or any other actions for the boost end

        Debug.Log("Boost ended. Cooldown started.");
    }

    private void ApplyBoostForce()
    {
        // Apply the boost force to the car
        _rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
    }
}
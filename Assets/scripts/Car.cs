using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Car : MonoBehaviour
{
    public static Car Instance { get; private set; }

    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    private Rigidbody rb;

    public bool braking;
    public bool backing;
    public float speed;
    
    public float brakeForce;
    
    public float backMultiplier;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        MessageSystem.Instance.ShowMessage("Follow the arrow on bottom of the screen to find your next pick up.", 7f);
    }
    
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        var visualWheel = collider.transform.GetChild(0);
        collider.GetWorldPose(out var position, out var rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        speed = Vector3.Dot(transform.forward, rb.velocity);
        
        var motorAxis =  Input.GetAxis("Vertical");

        var motorAxisAbs = Mathf.Abs(motorAxis);
        var hasPlayerInput = motorAxisAbs > .1f;
        
        var motor = maxMotorTorque * motorAxis;
        var steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        
        braking = false;
        backing = false;

        if (hasPlayerInput)
        {
            if (motorAxis < 0f)
            {
                if (speed > 0f)
                {
                    braking = true;
                }
            }
            else
            {
                if (speed < 0f)
                {
                    braking = true;
                } 
                else if (speed < 1f)
                {
                    motor *= 2;
                }
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            hasPlayerInput = true;
            braking = true;
        }
        
        
            
        if(speed < -0.4f && hasPlayerInput)
        {
            backing = true;
        }
        
        foreach (var axleInfo in axleInfos) {
            
            if (braking)
            {
                axleInfo.leftWheel.brakeTorque = brakeForce * axleInfo.brakePower;
                axleInfo.rightWheel.brakeTorque = brakeForce * axleInfo.brakePower;
            }
            else
            {
                axleInfo.leftWheel.brakeTorque = 0f;
                axleInfo.rightWheel.brakeTorque = 0f;
                
                if (axleInfo.motor) 
                {
                    if (backing)
                    {
                        axleInfo.leftWheel.motorTorque = motor * backMultiplier;
                        axleInfo.rightWheel.motorTorque = motor * backMultiplier;
                    }
                    else if(!braking)
                    {
                        axleInfo.leftWheel.motorTorque = motor;
                        axleInfo.rightWheel.motorTorque = motor;
                    }
                }
            }
           
            if (axleInfo.steering) 
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
        
        //
        // var velocityDir = Vector3.Normalize(rb.velocity);
        //
        // rb.centerOfMass = Vector3.up * centerOfMass;
        // if (Input.GetKey(KeyCode.W))
        // {
        //     rb.AddForce(transform.forward * accelerationForce,  ForceMode.Acceleration);
        // }
        //
        // if (Input.GetKey(KeyCode.S))
        // {
        //     if (Vector3.Dot(transform.forward, rb.velocity) > 1)
        //     {
        //         rb.AddForce(-velocityDir*breakingForce, ForceMode.Acceleration);
        //         breaking = true;
        //     }
        //     else if (Vector3.Dot(transform.forward, rb.velocity) > 0)
        //     {
        //         breaking = true;
        //     }
        //     else
        //     {
        //         rb.AddForce(-transform.forward * backingForce);
        //         backing = true;
        //     }
        // }
        //
        // if (Input.GetKey(KeyCode.A))
        // {
        //     rb.AddTorque(-Vector3.up*turnTorque);
        // }
        //
        // if (Input.GetKey(KeyCode.D))
        // {
        //     rb.AddTorque(Vector3.up*turnTorque);
        // }
    }
    
    [System.Serializable]
    public class AxleInfo {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor; // is this wheel attached to motor?
        public bool steering; // does this wheel apply steer angle?
        
        [Range(0f, 1f)] public float brakePower;
    }
}

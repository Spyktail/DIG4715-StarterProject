using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody2D carRigidBody2D;
    public float accelerationFactor;
    public float turnFactor;
    public float driftFactor;
    public float minSpeedTurnDivisor = 8;
    public float MaxSpeed;
    float velocityVsUp;
    float gasPedal = 0;
    float Turning = 0;
    float rotationAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        SetInputVector(inputVector);
    }

    void FixedUpdate()
    {
        MakeCarGo();
        SteerCar();
        KillOrthogonalVelocity();

    }

    public float Drag;
    void MakeCarGo()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);
        if (velocityVsUp > MaxSpeed && gasPedal > 0)
            return;
        if (velocityVsUp < -MaxSpeed * 0.5f && gasPedal < 0)
            return;
        if (carRigidBody2D.velocity.sqrMagnitude > MaxSpeed * MaxSpeed && gasPedal > 0)
            return;

        if (gasPedal == 0)
            carRigidBody2D.drag = Mathf.Lerp(carRigidBody2D.drag, Drag, Time.fixedDeltaTime);
        else carRigidBody2D.drag = 0;
        
        Vector2 engineForceVector = transform.up * gasPedal * accelerationFactor;
        carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void SteerCar()
    {
        float minSpeedToTurnFactor = (carRigidBody2D.velocity.magnitude/minSpeedTurnDivisor);
        minSpeedToTurnFactor = Mathf.Clamp01(minSpeedToTurnFactor);
        rotationAngle -= Turning * turnFactor * minSpeedToTurnFactor;
        carRigidBody2D.MoveRotation(rotationAngle);

    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody2D.velocity, transform.right);
        carRigidBody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector) 
    {
        gasPedal = inputVector.y;
        Turning = inputVector.x;
    }

}

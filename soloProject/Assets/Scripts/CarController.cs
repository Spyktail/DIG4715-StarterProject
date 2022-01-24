using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    float twoSecTimer = 2;
    bool twoSecondRunning = false;
    public AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip loseClip;
    float rotationAngle = 0;
    public Text instructions;
    /*public Text winCondText = "";

    public void winCond() 
    {
        if (winCondition = 1) 
        {

        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instructions.text = "Use the WASD keys to make it to end without falling in a black hole!";
        twoSecondRunning = true;
    }

    public void WinCond(int winCondition) 
    {
        if (winCondition == 1)
        {
            instructions.text = "YOU WIN!!!";
        }
        if (winCondition == 2)
        {
            instructions.text = "You Lose!";
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*if (twoSecTimer <= 0)
        {
            Vector2 inputVector = Vector2.zero;
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            SetInputVector(inputVector);

            if (inputVector.y != 0 || inputVector.x != 0)
            {
                Timer.instance.SetTimer(true);
            }
        }*/

        if (twoSecondRunning)
        {
            if (twoSecTimer > 0)
            {
                twoSecTimer -= Time.deltaTime;
            }
            else 
            {
            twoSecTimer = 0;

            instructions.text = "";

            Vector2 inputVector = Vector2.zero;
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            SetInputVector(inputVector);

            if (inputVector.y != 0 || inputVector.x != 0)
            {
                Timer.instance.SetTimer(true);
            }
            }
        }

        if (Timer.instance.timeRemaining == 0)
        {
            Vector2 position = transform.position;
            position.y = position.y - 50f;
            transform.position = position;
            
            Destroy(this);

            audioSource.PlayOneShot(loseClip);

            instructions.text = "You Lose!";
        }
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

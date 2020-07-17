using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidBody : MonoBehaviour
{
    public Rigidbody controller;
    public Transform horizontal;
    public Transform vertical;

    public Transform PlayerMain;

    public Transform groundCheck;
    public float groundDist = 0.5f;
    public LayerMask groundMask;
    public float downGravity;
    public float upGravity;
    public float moveSpeed = 13f;
    public float reverseForwardAcc = 50f;
    public float reverseStrafeAcc = 50f;

    public float jumpSpeed = 14f;

    public float jumpTimerMax = 0.2f;
    float jumpTimer = 0f;

    public int gravAmmo = 0;
    public int keys = 0;

    public bool doubleJumpEnabled;
    public float doubleJumpRatio;
    private bool canDoubleJump = false;


    public Vector3 storedVelocity;
    public bool inARotation = false;
    public float rotateAmount = 0f;
    public float cumRotateAmount = 0f;
    public Vector3 rotateAxis;
    public OrientWidget OW;
    public float rotationTimeMax = 0.5f;
    public float rotationTimeSum = 0f;

    float x;
    float z;
    bool isJumpPressed;
    bool isGrounded;
    bool isFire1 = false;

    public float coyoteTime = 0.1f;
    public float coyoteTimer = 0f;


    GameObject[] universes;
    Vector3[] universeOrigins;

    public float drag;
    public float dragGround = 6f;

    // Start is called before the first frame update
    void Start()
    {
       universes = GameObject.FindGameObjectsWithTag("Universe");
    }

    public void AddAmmo()
    {
        gravAmmo++;
    }
    public void AddKey()
    {
        keys++;
    }

    void Update()
    {
        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (coyoteTimer < coyoteTime) 
        {
            coyoteTimer += Time.deltaTime;
        }

        isJumpPressed = Input.GetButton("Jump") && (jumpTimer <= 0f);
        isFire1 = Input.GetButton("Fire1");


        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (isGrounded) 
        {
            coyoteTimer = 0f;
            canDoubleJump = true;
        }
        if (gravAmmo > 0 && isFire1 && !inARotation)
        {
            StartGravityRotate();
        }
        if (inARotation) 
        {
            StepGravityRotate();
        }
    }

    void StartGravityRotate()
    {
        
        if (OW.GetAxis().y != -1f && (OW.GetAxisRight() != OW.GetAxis()))
        {
            universeOrigins = new Vector3[universes.Length];
            for (int i = 0; i < universes.Length; i++) 
            {
                universeOrigins[i] = universes[i].transform.position;
            }
            gravAmmo--;
            storedVelocity = controller.velocity;
            cumRotateAmount = 0f;
            controller.velocity = Vector3.zero;
            inARotation = true;
            rotationTimeSum = 0f;

            rotateAmount = 90f;
            if (OW.GetAxis().y == 1f)
            {
                rotateAmount = 180f;
            }

            rotateAxis = OW.GetAxisRight();

        }
    }
    void StepGravityRotate()
    {
        float deltaTheta = Time.deltaTime / rotationTimeMax * rotateAmount;
        if (cumRotateAmount + deltaTheta >= rotateAmount)
        {
            EndGravityRotate();
            return;
        }
        cumRotateAmount += deltaTheta;

        for (int i = 0; i < universes.Length; i++)
        {
            universes[i].transform.RotateAround(controller.position - PlayerMain.position + universeOrigins[i], rotateAxis, deltaTheta);
        }



        //Move the world into a constant position
        //Vector3 offset = Vector3.zero - everything.position;
        //transform.position = transform.position + offset;
        //everything.position = everything.position + offset;
        //Move the world into a constant position
        Vector3 offset = universeOrigins[0] - universes[0].transform.position;
        transform.position = transform.position + offset;
        foreach (GameObject u in universes)
        {
            u.transform.position = u.transform.position + offset;
        }
        //everything.position = everything.position + offset;
    }
    void EndGravityRotate() 
    {
        for (int i = 0; i < universes.Length; i++)
        {
            universes[i].transform.RotateAround(controller.position - PlayerMain.position + universeOrigins[i], rotateAxis, rotateAmount - cumRotateAmount);
        }
        inARotation = false;
        controller.velocity = Quaternion.AngleAxis(rotateAmount, rotateAxis) * storedVelocity;

        //Move the world into a constant position
        Vector3 offset = universeOrigins[0] - universes[0].transform.position;
        transform.position = transform.position + offset;
        foreach (GameObject u in universes) 
        {
            u.transform.position = u.transform.position + offset;
        }
        //everything.position = everything.position + offset;
    }






    Vector3 NoAccAdd(Vector3 a, Vector3 b) 
    {
        float prevMag = a.magnitude;
        a = a + b;
        if (prevMag > moveSpeed) 
        {
            a = Vector3.ClampMagnitude(a, prevMag);
        }
        return a;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inARotation)
        {
            PlanarControls();
            if (!isGrounded)
            {
                controller.AddForce(controller.velocity.normalized * -drag);

            }
            else
            {
                controller.AddForce(controller.velocity.normalized * -dragGround);
            }



            if (isJumpPressed && (isGrounded || coyoteTimer < coyoteTime))
            {
                controller.velocity += Vector3.up * jumpSpeed;
                isJumpPressed = false;
                jumpTimer = jumpTimerMax;
            }
            else if (isJumpPressed && canDoubleJump && !isGrounded && doubleJumpEnabled) 
            {
                canDoubleJump = false;
                controller.velocity = controller.velocity + (-controller.velocity.y + jumpSpeed * doubleJumpRatio) * Vector3.up;
                isJumpPressed = false;
                jumpTimer = jumpTimerMax;
            }

            //if (!isGrounded)
            //{
            if (controller.velocity.y <= 0)
            {
                controller.AddForce(Vector3.down * downGravity);
            }
            else
            {
                controller.AddForce(Vector3.down * upGravity);
            }
            //}
        }
    }
    void PlanarControls() 
    {
        float mag = (x * x + z * z);
        if (mag > 0)
        {
            x = x / Mathf.Sqrt(mag);
            z = z / Mathf.Sqrt(mag);
        }
        float altMoveSpeedz = moveSpeed * Mathf.Abs(z);
        float altMoveSpeedx = moveSpeed * Mathf.Abs(x);
        Vector3 velocityNoY = controller.velocity;
        Vector3 velocityNoYPre = velocityNoY;
        velocityNoY.y = 0;
        float velMag = velocityNoY.magnitude;
        //Vector3 vel = (horizontal.right * x * moveSpeed + horizontal.forward * z * moveSpeed);
        if (z == 0 && Mathf.Abs(Vector3.Dot(velocityNoY, horizontal.forward)) <= moveSpeed && velMag <= moveSpeed)
        {
            velocityNoY -= (Vector3.Dot(velocityNoY, horizontal.forward)) * horizontal.forward;
        }

        if (z > 0 && Mathf.Abs(Vector3.Dot(velocityNoY, horizontal.forward)) < altMoveSpeedz && velMag <= moveSpeed)
        {
            //velocityNoY += (altmoveSpeed - Vector3.Dot(velocityNoY, horizontal.forward)) * horizontal.forward;
            velocityNoY = NoAccAdd(velocityNoY, (altMoveSpeedz - Vector3.Dot(velocityNoY, horizontal.forward)) * horizontal.forward);
        }
        else if (z < 0 && Mathf.Abs(Vector3.Dot(velocityNoY, -horizontal.forward)) < altMoveSpeedz && velMag <= moveSpeed)
        {
            //velocityNoY += (altmoveSpeed - Vector3.Dot(velocityNoY, -horizontal.forward)) * -horizontal.forward * Mathf.Abs(z);
            velocityNoY = NoAccAdd(velocityNoY, (altMoveSpeedz - Vector3.Dot(velocityNoY, -horizontal.forward)) * -horizontal.forward * Mathf.Abs(z));
        }
        else if (z > 0 && Vector3.Dot(velocityNoY, horizontal.forward) < 0)
        {
            controller.AddForce(z * horizontal.forward * reverseForwardAcc);
        }
        else if (z < 0 && Vector3.Dot(velocityNoY, -horizontal.forward) < 0)
        {
            controller.AddForce(z * horizontal.forward * reverseForwardAcc);
        }

        if (x == 0 && Mathf.Abs(Vector3.Dot(velocityNoY, horizontal.right)) <= moveSpeed && velMag <= moveSpeed)
        {
            velocityNoY -= (Vector3.Dot(velocityNoY, horizontal.right)) * horizontal.right;
        }

        if (x > 0 && Mathf.Abs(Vector3.Dot(velocityNoY, horizontal.right)) < altMoveSpeedx && velMag <= moveSpeed)
        {
            //velocityNoY += (altMoveSpeedx - Vector3.Dot(velocityNoY, horizontal.right)) * horizontal.right;
            velocityNoY = NoAccAdd(velocityNoY, (altMoveSpeedx - Vector3.Dot(velocityNoY, horizontal.right)) * horizontal.right);
        }
        else if (x < 0 && Mathf.Abs(Vector3.Dot(controller.velocity, -horizontal.right)) < altMoveSpeedx && velMag <= moveSpeed)
        {
            //velocityNoY += (altMoveSpeedx - Vector3.Dot(velocityNoY, -horizontal.right)) * -horizontal.right;
            velocityNoY = NoAccAdd(velocityNoY, (altMoveSpeedx - Vector3.Dot(velocityNoY, -horizontal.right)) * -horizontal.right);
        }
        else if (x > 0 && Vector3.Dot(velocityNoY, horizontal.right) < 0)
        {
            controller.AddForce(x * horizontal.right * reverseStrafeAcc);
        }
        else if (x < 0 && Vector3.Dot(controller.velocity, -horizontal.right) < 0)
        {
            controller.AddForce(x * horizontal.right * reverseStrafeAcc);
        }

        velocityNoY.y = 0;
        if (velMag <= moveSpeed)
        {
            velocityNoY = Vector3.ClampMagnitude(velocityNoY, moveSpeed);
        }

        controller.velocity = controller.velocity.y * Vector3.up + velocityNoY;
    }
}

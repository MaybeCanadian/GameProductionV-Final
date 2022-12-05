using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

public class PlayerMovementController: MonoBehaviour
{
    [Header("Player Movement")]
    [Tooltip("How fast the player moves up and down")]
    public float ForwardWalkSpeed = 5.0f;
    [Tooltip("How fast the player moves left and right")]
    public float StrafeSpeed = 4.0f;
    [Tooltip("The force applied to the player when they jump")]
    public float JumpForce = 10.0f;
    [Tooltip("The speed in which the player rotates toward the mouse")]
    public float rotationSpeed = 10.0f;
    public float MaxVelX = 10.0f;
    public float MaxVelZ = 10.0f;

    [Header("Connected Objects")]
    [Tooltip("This is set in the start function but you can do it manually")]
    private Rigidbody rb;
    [Tooltip("This is the object that refers to the character model")]
    public GameObject BodyObject;
    private Camera mainCamera;
    [Tooltip("This is used for the hidden plane attached to the player for use of the mouse controls")]
    public LayerMask CameraRayCastLayer;


    private float FacingAngle;
    [Header("Extra Bools")]
    [Tooltip("The bool of if on ground, for view purposes")]
    public bool IsGrounded;

    private Vector3 MovementVector;

    [Header("Animation")]
    public Animator BodyAnimations;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        IsGrounded = false;
        MovementVector = new Vector3(0, 0, 0);
        
        //Cursor.lockState = CursorLockMode.Locked;   //found info from this tutortial https://docs.unity3d.com/ScriptReference/Cursor-lockState.html
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Time.timeScale != 0.0f)
        {
            RotateTowardsCamera();
        }
    }

    private void Move()
    {
        Vector3 TempMovementVector = MovementVector;

        Vector3 InvertedMovementVector = new Vector3(TempMovementVector.x, TempMovementVector.y, TempMovementVector.z);
        Vector3 RotatedVector = BodyObject.transform.localRotation * InvertedMovementVector;
        //BodyAnimations.SetFloat("ForwardSpeed", RotatedVector.normalized.z);
        //BodyAnimations.SetFloat("RightSpeed", RotatedVector.normalized.x);

        TempMovementVector = Quaternion.Euler(0, mainCamera.gameObject.transform.eulerAngles.y, 0) * TempMovementVector;
        //I got this from the rotation video as well, this offsets the moveDirection to be up relative to the camera
        transform.position += TempMovementVector * Time.deltaTime;
    } //moves the character with the wasd, it moves on the x and z axis and uses simple transform

    public void InputMove(Vector2 movement)
    {
        movement = movement.normalized;
        MovementVector = (movement.y * ForwardWalkSpeed * Vector3.forward) + (movement.x * StrafeSpeed * Vector3.right);
    }

    private void RotateTowardsCamera() //https://www.youtube.com/watch?v=-0GFb9l3NHM
    {
        Ray MouseRay = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(MouseRay, out RaycastHit HitInfo, maxDistance: 300f, CameraRayCastLayer))
        {
            Vector3 target = HitInfo.point;

            target = new Vector3(target.x, BodyObject.transform.position.y, target.z);
            //we remove the y component

            //Quaternion storedRotation = BodyObject.transform.rotation;

            BodyObject.transform.LookAt(target);
           
        }
    }

    public void Jump()
    {
            if (IsGrounded)
            {
                rb.AddForce(JumpForce * rb.mass * Vector3.up);
                IsGrounded = false;
            }
    }

    private void OnCollisionStay(Collision collision)
    {
        IsGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }

}

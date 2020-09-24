using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerContrl : MonoBehaviour
{
    // ******************
    // PLAYER CONTROLLER
    // ******************

    private CharacterController controller;
    
    [SerializeField]
    private float speed = 50f;
    
    private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    public static PlayerContrl Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if the game is paused or not
        if(StateMng.Instance.GetState() == GameState.EXPLORATION)
        {
            Movement();
            MouseMovement(true);
        }
        else
        {
            MouseMovement(false);
        }

        Pause();
    }

    // -------------------------
    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) // Input detected
        {
            WalkRun(direction);
        }
        else // Idle
        {
            if (!controller.isGrounded)
            {
                WalkRun(direction); // Applying gravity if idle and not in ground
            }
            else
            {
                // Handle idle animation...
            }
        }
    }

    // Used in 'Movement'
    private void WalkRun(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        // Gravity
        moveDir.y -= 9.81f;

        controller.Move(moveDir * speed * Time.deltaTime);
    }
    // -------------------------

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StateMng.Instance.SetPause();
        }
    }

    private void MouseMovement(bool command)
    {
        if (cam.GetComponent<CinemachineBrain>().enabled != command)
        {
            cam.GetComponent<CinemachineBrain>().enabled = command;
        }
    }
}
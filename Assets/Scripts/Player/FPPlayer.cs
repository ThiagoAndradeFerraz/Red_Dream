using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayer : MonoBehaviour
{
    private enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }

    [Header("Speed settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotateSpeed;

    private float minimumVert = -45.0f;
    private float maximumVert = 45.0f;
    private RotationAxis axis = RotationAxis.MouseX;

    private Camera cam;
    private CharacterController controller;
    
    private float raycastRange = 100f;

    public static FPPlayer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Camera>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Walk();
        Rotate();
        RayCastMethod();
    }

    private void Walk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        //transform.Rotate(0, mouseY * rotateSpeed * Time.deltaTime, 0);
        
        controller.Move(direction * walkSpeed * Time.deltaTime);

    }

    private void RayCastMethod()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, raycastRange))
        {
            //Debug.Log(hit.transform.name);
        }
        else
        {

        }
    }

    private void Rotate()
    {

        //transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);

        float rotationX = 0;
        rotationX -= Input.GetAxis("Mouse Y") * rotateSpeed;
        rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert); //Clamps the vertical angle within the min and max limits (45 degrees)

        float rotationY = cam.transform.localEulerAngles.y;

        cam.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

    }



}

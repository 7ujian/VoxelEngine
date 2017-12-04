using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed = 4f;
    public float mouseSensitivity = 40f;
    public float rotateSpeed = 40f;
    public Transform cameraTransform;
    public float jumpSpeed = 6f;
    public float flySpeed = 4f;
    public float gravity = 9.8f;
    private float speedY;
    private CharacterController cc;
    private bool isJump = false;
    private Vector3 moveDirection = Vector3.zero;
    

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Camera
        var mouseX = 0f;
        var mouseY = 0f;

        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        }
        else
        {
            mouseX = Input.GetAxis("Rotate X");
            mouseY = Input.GetAxis("Rotate Y");
        }
        
        if (mouseX != 0 || mouseY != 0)
        {
            transform.Rotate(Vector3.up, mouseX * rotateSpeed * Time.deltaTime);
            cameraTransform.Rotate(Vector3.right, mouseY * rotateSpeed * Time.deltaTime);
        }
        
//        var v = Input.GetAxis("Vertical");
//        var h = Input.GetAxis("Horizontal");
//
//        var velocity = Vector3.zero;
//        
//        if (v != 0 || h != 0)
//        {
//            velocity += v * speed * transform.forward;
//            velocity += h * speed * transform.right;            
//        }
        var y = moveDirection.y;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        moveDirection.y = y;
        
        if (Input.GetButton("Fly"))
        {
            moveDirection.y = flySpeed;
        }
        
        if (cc.isGrounded) {            
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;            
        }

        
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);




    }

}

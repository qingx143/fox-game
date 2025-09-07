using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Configurations")]
    public float walkSpeed; // set this in inspector 
    public float runSpeed;  // set this in inspector 
    public float jumpSpeed; // set this in inspector

    [Header("Runtime")]
    Vector3 newVelocity;
    [SerializeField] bool isGrounded = false;    // makes use of collision method in MonoBehaviour class

    // Start is called once before the first execution of Update after the MonoBehaviour is created ---------------------------------------- START
    void Start()
    {
        // hides and locks the mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame ---------------------------------------------------------------------------------------------------- UPDATE
    void Update()
    {
        // PLAYER MOVEMENT
        // retains the vertical velocity of the rb while discarding the forward and horizontal velocity
        newVelocity = Vector3.up * rb.linearVelocity.y; // this is equivalent to new Vector3(0f, rb.velocity.y, 0f)

        // Input.GetKey(Key) checks whether a key is pressed -> returns a boolean
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Input.GetAxis() retrieves the value of a virtual axis defined in Input Manager
        // -> returns a float value between -1 and 1 that represents input intensity along that axis
        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;

        // PLAYER JUMP -> if you press space and you're on the ground
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // this adds a upward force when jumping
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    // using FixedUpdate() because physics is involved ------------------------------------------------------------------------------ FIXED UPDATE
    void FixedUpdate()
    {
        // applying new velocity to rb
        rb.linearVelocity = newVelocity;
    }

    // GROUND DETECTION METHODS -> you can also use Physics.Raycast() ------------------------------------------------- GROUND COLLISION DETECTION
    void OnCollisionStay(Collision col)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }
}

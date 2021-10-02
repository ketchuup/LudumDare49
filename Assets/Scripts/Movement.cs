using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 40f;
    public MovementController controller;

    private float horizontalMovement;
    private bool isJumping;
    private bool isCrouching;

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMovement * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }
}
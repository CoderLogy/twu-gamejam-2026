using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [SerializeField]
    private float speed = 10;

    private Rigidbody rb;
    private Vector2 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {   
        movement = speed * context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        UpdateDirection();

        // Set the player's velocity to the inputted movement
        rb.linearVelocity = new Vector3(
            movement.x,
            0,
            movement.y
        );
    }

    void UpdateDirection()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitInfo;

        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            Vector3 direction = hitInfo.point - transform.position;
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        }
    }

}

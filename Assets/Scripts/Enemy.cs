using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] Vector3 target = Vector3.zero;
    [SerializeField] string bulletTag = "Bullet";
    [SerializeField] string targetTag = "TrashPile";

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get the direction to the target (trash)
        Vector3 toTarget = target - transform.position;
        toTarget.y = 0;
        
        // Look and move towards the target
        transform.rotation = Quaternion.LookRotation(toTarget.normalized, Vector3.up);
        rb.linearVelocity = toTarget.normalized * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(bulletTag))
        {
            Destroy(gameObject);
        } 
        else if (collision.gameObject.tag.Equals(targetTag))
        {
            Destroy(gameObject);
        }

    }
}

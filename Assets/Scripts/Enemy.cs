using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using System.Security.AccessControl;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] Vector3 target = Vector3.zero;
    [SerializeField] string bulletTag = "Bullet";
    [SerializeField] string targetTag = "TrashPile";

    [SerializeField] int curvepoints = 3;// number of points in curve
    [SerializeField] float curveradius = 2f; //offset off path

    private Rigidbody rb;
    private Vector3[] pathpoints;
    private int currentpoint = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GeneratePath();
    }

    void FixedUpdate()
    // {
    //     // Get the direction to the target (trash)
    //     Vector3 toTarget = target - transform.position;
    //     toTarget.y = 0;
        
    //     // Look and move towards the target
    //     transform.rotation = Quaternion.LookRotation(toTarget.normalized, Vector3.up);
    //     rb.linearVelocity = toTarget.normalized * speed;

     {
        if (pathpoints == null || pathpoints.Length == 0) return;

        Vector3 nextPoint = pathpoints[currentpoint];
        Vector3 dir = nextPoint - transform.position;
        dir.y = 0;
        Vector3 moveDir = dir.normalized;

        // Look and move toward next point
        transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        rb.linearVelocity = moveDir * speed;

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, nextPoint) < 0.1f)
        {
            currentpoint++;
            if (currentpoint >= pathpoints.Length)
            {
                // Stop at target
                rb.linearVelocity = Vector3.zero;
            }
        }
    //}


    }

    private void GeneratePath()
    {
        pathpoints = new Vector3[curvepoints + 1];
        Vector3 start = transform.position;
        Vector3 end = target;

        for (int i = 0; i < curvepoints; i++)
        {
            float t = (i + 1f) / (curvepoints + 1f);
            Vector3 point = Vector3.Lerp(start, end, t);

            // Offset sideways to create curve
            Vector3 sideways = Vector3.Cross(Vector3.up, (end - start).normalized);
            point += sideways * UnityEngine.Random.Range(-curveradius, curveradius);

            pathpoints[i] = point;
        }
        pathpoints[curvepoints] = end;
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

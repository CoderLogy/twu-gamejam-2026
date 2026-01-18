using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] Vector3 target = Vector3.zero;
    [SerializeField] string bulletTag = "Bullet";
    [SerializeField] string targetTag = "TrashPile";

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target);
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

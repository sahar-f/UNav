using UnityEngine;
using UnityEngine.AI;

public class Line : MonoBehaviour
{
    public Transform marker;
    public Transform target;

    private NavMeshAgent agent;
    private LineRenderer lineRenderer;

    void Start()
    {
        agent = marker.GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
       
    }


    void startNavigation()
    {
        //target = transform.Find("1B71EngineeringBuilding");
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.position, path);
        //Debug.Log("Corners: " + path.corners.Length);
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
        if (marker.position == target.position)
        {
            lineRenderer.enabled = false;
        }



    }

    void Update()
    {
        // Set the destination of the NavMeshAgent to the target object
        //agent.SetDestination(target.position);

        // Get the path points from the NavMeshAgent
        startNavigation();
        



    }
}
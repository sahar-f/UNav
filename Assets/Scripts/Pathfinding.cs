using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public Transform marker;
    public Transform target;

    private NavMeshAgent agent;
    private LineRenderer lineRenderer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Set the destination of the NavMeshAgent to the target object
        agent.SetDestination(target.position);


        // Get the path points from the NavMeshAgent
        /*NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.position, path);
        //Debug.Log("Corners: " + path.corners.Length);
        Vector3[] corners = path.corners;
        // Add the marker position as the first point in the line renderer
        int numPoints = corners.Length + 1;
        Vector3[] linePoints = new Vector3[numPoints];
        linePoints[0] = marker.position;
        // Add the path points to the line renderer
        for (int i = 0; i < corners.Length; i++)
        {
            linePoints[i + 1] = corners[i];
            //Debug.DrawLine(corners[i], corners[i] + Vector3.up * 2, Color.red, 0.5f);
        }
        // Set the positions of the line renderer
        //Debug.Log("Numpts: " + numPoints);
        //Debug.Log("Linepts: " + linePoints);
        //lineRenderer.positionCount = numPoints;
        //lineRenderer.SetPosition(0, marker.transform.position);
        //lineRenderer.SetPosition(1, target.transform.position);*/


    }
}
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Line : MonoBehaviour
{
    public static GameObject marker;
    private static GameObject target;

    private static NavMeshAgent agent;
    private static LineRenderer lineRenderer;
    private static float distanceThreshold = 50f;
    public static TMP_InputField inputField;
    public static bool flag;
    public static string holder;


    void Start()
    {
        marker = GameObject.Find("GPSMarker");
        agent = marker.GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        flag = false;
        
        lineRenderer.enabled = false;
       
    }

    public static void beginNavigation(string name)
    {
        holder= name;
        flag = true;
    }
    void startNavigation(string name)
    {
        target = GameObject.Find(name);
        float distance = Vector3.Distance(marker.transform.position, target.transform.position);

        
            if (distance < distanceThreshold)
            {
                lineRenderer.enabled = false;
                flag = false;

                
            }
            else
            {
                NavMeshPath path = new NavMeshPath();
                agent.SetDestination(target.transform.position);
                agent.CalculatePath(target.transform.position, path);
                //Debug.Log("Corners: " + path.corners.Length);
                lineRenderer.positionCount = path.corners.Length;
                lineRenderer.SetPositions(path.corners);
                lineRenderer.enabled = true;
            }
        



    }
    void CancelNavigation()
    {
        lineRenderer.enabled=false;
    }

    void Update()
    {
        // Set the destination of the NavMeshAgent to the target object
        //agent.SetDestination(target.position);

        // Get the path points from the NavMeshAgent

        /*string search = inputField.text;
        if (search != " ")
        {
            startNavigation(search);
        }*/
        

        //or simply use the line below, 
        //input.onEndEdit.AddListener(SubmitName);  // This also works
        if (flag)
        {
            startNavigation(holder);
        }
    }




}
    
        
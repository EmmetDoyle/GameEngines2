using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPatrollerLeader : MonoBehaviour {

    public Path redPath;
    public GameObject mothership = null;
    public List<Vector3> waypoints = new List<Vector3>();

    public float gap;

    // Use this for initialization
    void Start () {
        CreateWaypoints(8);
        redPath.waypoints = waypoints;
    }

    void CreateWaypoints(int segments)
    {
        Debug.Log("here");
        float thetaInc = (Mathf.PI * 2.0f) / (float)segments;

        for (int i = 0; i < segments; i++)
        {
            float theta = thetaInc * i;
            float x = gap * Mathf.Sin(theta);
            float z = gap * Mathf.Cos(theta);

            Vector3 waypoint = new Vector3(x, 0, z);
            waypoints.Add(waypoint + mothership.transform.position);
            Debug.Log(x);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

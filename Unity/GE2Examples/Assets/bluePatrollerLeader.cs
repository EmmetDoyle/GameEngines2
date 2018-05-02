using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluePatrollerLeader : MonoBehaviour {

    public Path bluePath;
    public GameObject mothership = null;
    public List<Vector3> waypoints = new List<Vector3>();

    public float gap = 10;

    // Use this for initialization
    void Start () {
        CreateWaypoints(8);
        bluePath.waypoints = waypoints;
	}

    void CreateWaypoints(int segments)
    {
        float thetaInc = (Mathf.PI * 2.0f) / (float)segments;

        for (int i = 0; i < segments; i++)
        {
            float theta = thetaInc * i;
            float x = gap * Mathf.Sin(theta);
            float z = gap * Mathf.Cos(theta);

            Vector3 waypoint = new Vector3(x, 0, z);
            waypoints.Add(waypoint + mothership.transform.position);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

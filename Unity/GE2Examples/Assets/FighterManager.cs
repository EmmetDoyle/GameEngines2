using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterManager : MonoBehaviour {

    public enum FighterState
    {
        SEEK, FLEE, RETREAT
    }

    public FighterState state = FighterState.SEEK;
    public float range;
    public float crystalRange;
    public string otherTeam;

    public Seek seek;
    public Flee flee;
    public GameObject mothership;

	// Use this for initialization
	void Start () {
        StartCoroutine(CheckState());
        flee.enabled = false;
	}

    IEnumerator CheckState()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        while (true)
        {
            if (state == FighterState.SEEK)
            {
                UpdateSeekState();
            }
            else if (state == FighterState.FLEE)
            {
                UpdateFleeState();
            }
            else
            {
                UpdateRetreatState();
            }

            yield return new WaitForSeconds(1.0f / (float)4);
        }
    }

    private void UpdateSeekState()
    {
        //get all objects within range of me
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        Collider[] crystalColliders = Physics.OverlapSphere(transform.position, crystalRange);
        //if any
        float nearestDistance = float.MaxValue;
        GameObject closest = null;

        foreach (Collider c in crystalColliders)
        {
            if (c.gameObject.tag == "Crystal")
            {
                Debug.Log("here crystal");
                state = FighterState.RETREAT;
                break;
            }
        }

        foreach (Collider c in hitColliders)
        {
            if(c.gameObject.tag == otherTeam)
            {
                float distance = Vector3.Distance(transform.position, c.gameObject.transform.position);
                if (distance < nearestDistance)
                {
                    closest = c.gameObject;
                    nearestDistance = distance;
                }
            }
        }

        if(closest != null)
        {
            state = FighterState.FLEE;
            flee.enabled = true;
            flee.targetGameObject = closest;
            seek.enabled = false;
        }
        //if crystal
        //invoke retreat
        //else if object has other team flag
        //invoke flee

    }

    private void UpdateFleeState()
    {
        //get all objects within range of me
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, crystalRange);
        //if any
        //if crystal
        //invoke retreat
        //else
        //invoke seek

        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.tag == "Crystal")
            {
                state = FighterState.RETREAT;
                break;
            }
        }

        if (state != FighterState.RETREAT && Vector3.Distance(transform.position, flee.targetGameObject.transform.position) > range)
        {
            state = FighterState.SEEK;
            seek.enabled = true;
            flee.enabled = false;
        }
    }

    private void UpdateRetreatState()
    {
        flee.enabled = false;
        seek.enabled = true;
        seek.targetGameObject = mothership;
    }

    // Update is called once per frame
    void Update () {
		
	}
}

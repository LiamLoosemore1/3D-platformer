using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public HealthManager theHealthMan;

    public Renderer theRend;

    public Material cpOff;
    public Material cpOn;

	// Use this for initialization
	void Start ()
    {
        theHealthMan = FindObjectOfType<HealthManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CheckPointOn()
    {
        CheckPoint[] CheckPoints = FindObjectsOfType<CheckPoint>();
        foreach(CheckPoint cp in CheckPoints)
        {
            cp.CheckPointOff();
        }

        theRend.material = cpOn;
    }

    public void CheckPointOff()
    {
        theRend.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            theHealthMan.SetSpawnPoint(transform.position);
            CheckPointOn();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

    /// <summary>
    /// average velocity this fixedupdate()
    /// </summary>
    public Vector3 m_v;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        Vector3 deltaS = m_v * Time.deltaTime;
        this.transform.position += deltaS;
    }
}

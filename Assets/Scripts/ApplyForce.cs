using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class ApplyForce : MonoBehaviour {
    
    public Vector3 m_forceVector;

    private PhysicsEngine m_physicsEngineComp;

    private void Start()
    {
        m_physicsEngineComp = this.GetComponent<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        m_physicsEngineComp.AddForce(m_forceVector);
    }
}

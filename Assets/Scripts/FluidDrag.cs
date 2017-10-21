using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://en.wikipedia.org/wiki/Drag_(physics)
/// </summary>
public class FluidDrag : MonoBehaviour
{
    [Range(1.0f, 2.0f)]
    public float m_velocityExponent;    //[none]

    public float m_dragConstant;        //

    private PhysicsEngine m_physicsEngineComp;

    private void Start()
    {
        m_physicsEngineComp = this.GetComponent<PhysicsEngine>();		
	}
	
	void FixedUpdate ()
    {
        Vector3 velocityVector = m_physicsEngineComp.m_velovityVector;
        float speed = velocityVector.magnitude;
        float dragSize = CalculateDrag(speed);

        Vector3 dragVector = dragSize * -velocityVector.normalized;
        m_physicsEngineComp.AddForce(dragVector);
    }

    private float CalculateDrag(float speed)
    {
        return m_dragConstant * Mathf.Pow(speed, m_velocityExponent);
    }
}

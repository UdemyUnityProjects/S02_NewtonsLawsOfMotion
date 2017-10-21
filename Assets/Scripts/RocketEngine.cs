using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {


    public float m_fuelMass;                        //[kg]
    public float m_maxThrust;                       //kN [kg m s^-2]
    public float m_thrustPercent;                   //[none]

    public Vector3 m_thrustUnitVector;              //[none]

    private PhysicsEngine m_physicsEngineComp;

    private void Start()
    {
        m_physicsEngineComp = this.GetComponent<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        m_physicsEngineComp.AddForce(m_thrustUnitVector);
    }
}

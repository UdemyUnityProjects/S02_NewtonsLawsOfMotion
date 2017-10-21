using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://en.wikipedia.org/wiki/Rocket_engine
/// </summary>
[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {


    public float m_fuelMass;                        //[kg]
    public float m_maxThrust;                       //kN [kg m s^-2]

    [Range(0, 1.0f)]
    public float m_thrustPercent;                   //[none]

    public Vector3 m_thrustUnitVector;              //[none]

    private float m_currentThrust;                  //N

    private PhysicsEngine m_physicsEngineComp;

    private void Start()
    {
        m_physicsEngineComp = this.GetComponent<PhysicsEngine>();
        m_physicsEngineComp.m_mass += m_fuelMass;
    }

    private void FixedUpdate()
    {
        if(m_fuelMass > FuelThisUpdate())
        {
            //reduce fuel mass
            m_fuelMass -= FuelThisUpdate();
            m_physicsEngineComp.m_mass -= FuelThisUpdate();

            ExertForce();
        }
        else
        {
            Debug.LogWarning("Out of rocket fuel !");
        }        
    }

    private float FuelThisUpdate()
    {
        float exhaustMassFlow;
        float effectiveExhaustVelocity;

        //[m s^-1] liquid H O
        effectiveExhaustVelocity = 4462.0f;

        //thrust = massFlow * exhaustVelocity
        //massFlow = thrust / exhaustVelocity
        exhaustMassFlow = m_currentThrust / effectiveExhaustVelocity;

        //[kg]
        return exhaustMassFlow * Time.deltaTime;
    }

    private void ExertForce()
    {
        //*1000.0f to convert kN to N (Newton)
        m_currentThrust = m_thrustPercent * (m_maxThrust * 1000.0f);

        //N
        Vector3 thrustVector = m_thrustUnitVector.normalized * m_currentThrust;

        m_physicsEngineComp.AddForce(thrustVector);
    }
}

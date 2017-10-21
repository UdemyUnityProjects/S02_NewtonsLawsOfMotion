using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour
{
    private PhysicsEngine[] m_physicsEngineArray;

    private const float BIG_G = 6.673e-11f; // m^3 s^-2 kg^-1 ]

    // Use this for initialization
    void Start ()
    {
        m_physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        CalculateGravity();
    }

    private void CalculateGravity()
    {
        for (int i = 0; i < this.m_physicsEngineArray.Length; ++i)
        {
            for (int j = 0; j < this.m_physicsEngineArray.Length; ++j)
            {
                if (i == j)
                    continue;

                PhysicsEngine physicsEngineA = this.m_physicsEngineArray[i];

                if (physicsEngineA == this)
                    continue;

                PhysicsEngine physicsEngineB = this.m_physicsEngineArray[j];



                Debug.Log("Calculating gravitational force exerted on " + physicsEngineA.name +
                        " due to the gravity of " + physicsEngineB.name);


                Vector3 gravityFeltVector = Vector3.zero;

                //F = G * ((massA * massB) / distance * distance )
                //G = 6.7f
                //https://en.wikipedia.org/wiki/Gravitational_constant

                float mulMass = physicsEngineA.m_mass * physicsEngineB.m_mass;
                Vector3 forceDirection = physicsEngineB.transform.position - physicsEngineA.transform.position;
                float sqrDistance = (forceDirection).sqrMagnitude;

                float gravityMagnitude = (BIG_G * (mulMass / sqrDistance));

                gravityFeltVector = forceDirection.normalized * gravityMagnitude;

                physicsEngineA.AddForce(gravityFeltVector);
            }
        }
    }
}

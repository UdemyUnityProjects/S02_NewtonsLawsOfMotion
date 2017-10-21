using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

    /// <summary>
    /// average velocity this fixedupdate()
    /// </summary>
    public Vector3 m_v;
    public Vector3 m_netForceVector;

    public float m_mass = 1.0f;


    public List<Vector3> m_forceVectorList = new List<Vector3>();

    // Use this for initialization
    void Start ()
    { 
    }

    private void FixedUpdate()
    {
        AddForces();
        UpdateVelocity();

        //update position
        this.transform.position += m_v * Time.deltaTime;
    }

    private void AddForces()
    {
        m_netForceVector = Vector3.zero;

        for(int i = 0; i < m_forceVectorList.Count; ++i)
        {
            m_netForceVector += m_forceVectorList[i];
        }
    }

    private void UpdateVelocity()
    {
        Vector3 accelerationVector = m_netForceVector / m_mass;
        m_v += accelerationVector * Time.deltaTime;
    }
}

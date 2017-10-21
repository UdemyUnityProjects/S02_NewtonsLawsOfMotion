using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

    /// <summary>
    /// average velocity this fixedupdate()
    /// </summary>
    public Vector3 m_velovityVector;                //[m s^-1]
    public Vector3 m_netForceVector;                //N [kg m s^-2]

    public float m_mass = 1.0f;                     //[kg]


    private List<Vector3> m_forceVectorList = new List<Vector3>();

    private PhysicsEngine[] m_physicsEngineArray;

    private const float BIG_G = 6.673e-11f; // m^3 s^-2 kg^-1 ]


    /// <summary>
    /// TRAIL
    /// </summary>
    public bool showTrails = true;

    private LineRenderer lineRenderer;
    private int numberOfForces;

    // Use this for initialization
    private void Start()
    {
        SetupTrails();

        m_physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
       
        CalculateGravity();
        RenderTrails();
        UpdatePosition();
    }


    public void AddForce(Vector3 forceToAdd)
    {
        m_forceVectorList.Add(forceToAdd);
    }

    public List<Vector3> GetForceVectorList()
    {
        return m_forceVectorList;
    }

    
    private void CalculateGravity()
    {
        for(int i = 0; i < this.m_physicsEngineArray.Length; ++i)
        {
            for(int j = 0;  j < this.m_physicsEngineArray.Length; ++j)
            {
                if (i == j)
                    continue;

                PhysicsEngine physicsEngineA = this.m_physicsEngineArray[i];
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

    private void UpdatePosition()
    {
        //Sum forces and clear list
        m_netForceVector = Vector3.zero;
        for (int i = 0; i < m_forceVectorList.Count; ++i)
        {
            m_netForceVector += m_forceVectorList[i];
        }
        
        m_forceVectorList.Clear();

        //Update position with net force
        Vector3 accelerationVector = m_netForceVector / m_mass;
        m_velovityVector += accelerationVector * Time.deltaTime;
        this.transform.position += m_velovityVector * Time.deltaTime;
    }








    private void SetupTrails()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.yellow, Color.yellow);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.useWorldSpace = false;
    }

    private void RenderTrails()
    {
        if (showTrails)
        {
            lineRenderer.enabled = true;
            numberOfForces = m_forceVectorList.Count;
            lineRenderer.SetVertexCount(numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in m_forceVectorList)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                //lineRenderer.SetPosition(i + 1, forceVector);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

}




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
    }

    private void FixedUpdate()
    {
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
}

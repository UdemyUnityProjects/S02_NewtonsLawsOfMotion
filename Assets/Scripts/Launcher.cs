using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float m_maxLaunchSpeed;
    public AudioClip m_windUpSound, m_launchSound;
    public PhysicsEngine m_ballToLaunch;

    public float m_launchSpeed;
    private AudioSource m_audioSource;
    private float m_extraSpeedPerFrame;

	// Use this for initialization
	void Start () {
        m_audioSource = this.GetComponent<AudioSource>();
        m_audioSource.clip = m_windUpSound;
        m_extraSpeedPerFrame = (m_maxLaunchSpeed * Time.fixedDeltaTime) / m_audioSource.clip.length;
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        //increase ball speed to max over a few seconds
        //invoke repeating
        Debug.Log("Launcher clicked");

        m_launchSpeed = 0.0f;

        InvokeRepeating("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);

        m_audioSource.clip = m_windUpSound;
        m_audioSource.Play();

    }

    private void OnMouseUp()
    {
        //launch the ball
        CancelInvoke();
        m_audioSource.Stop();
        m_audioSource.clip = m_launchSound;
        m_audioSource.Play();

        PhysicsEngine newBall = Instantiate(m_ballToLaunch) as PhysicsEngine;
        newBall.transform.parent = (GameObject.Find("Launched Balls")).transform;

        Vector3 launchVelocity = new Vector3(1, 1, 0).normalized * m_launchSpeed;
        newBall.m_velovityVector = launchVelocity;
    }

    private void IncreaseLaunchSpeed()
    {
        if(m_launchSpeed <= m_maxLaunchSpeed)
        {
            m_launchSpeed += m_extraSpeedPerFrame;
        }
    }
}

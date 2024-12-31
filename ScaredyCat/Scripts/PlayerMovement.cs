using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; 
    Animator m_Animator;
    Rigidbody m_Rigidbody; 
    AudioSource m_AudioSource; 
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>(); 
    }

    // OnAnimatorMove specifies how root motion is applied from Animator where movement is applied but not rotation
    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    // FixedUpdate is called before the physics system solves any collisions and other interactions that have happened 
    void FixedUpdate()
    {
        // input axes return either 1 or -1
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // sets movement of character 
        m_Movement.Set(horizontal, 0f, vertical);

        // prevents movement in diagonals from being faster than horizontal or vertical directions 
        m_Movement.Normalize();

        // determines if there is horizontal or vertical input 
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        // determine if there is walking movement that will take place
        bool isWalking = hasHorizontalInput || hasVerticalInput; 

        // update the animator component based on the bool value of isWalking to update IsWalking parameter
        m_Animator.SetBool("IsWalking", isWalking); 

        if (isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }

        }
        else
        {
            m_AudioSource.Stop();

        }

        // calculate the character's forward vector 
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f); 

        // set the value of the rotation variable by creating a rotation looking in direction of desiredForward 
        m_Rotation = Quaternion.LookRotation(desiredForward);


        




        
    }
}


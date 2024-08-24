using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    
    [SerializeField] float mainthrust = 100;
    [SerializeField] float rotationthrust = 100;
    [SerializeField] AudioClip mainengine;
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField]ParticleSystem RocketJetParticles;
    [SerializeField]ParticleSystem rightThrusterParticles;
    [SerializeField]ParticleSystem leftThrusterParticles ;

        void Start()
    {
        rb=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

void ProcessInput()
{
  if (Input.GetKey(KeyCode.Space))
  {
    
    rb.AddRelativeForce(UnityEngine.Vector3.up * mainthrust *Time.deltaTime);
    if (!audioSource.isPlaying)
    {
        audioSource.PlayOneShot(mainengine);
    }
  }
  else
  {
    audioSource.Stop();
    
  }
}
void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            applyrotation(rotationthrust);
        }

        else if(Input.GetKey(KeyCode.D))
        {
           applyrotation(- rotationthrust);
        }
    }

    private void applyrotation(float rotationthisframe)
    {
        rb.freezeRotation=true;
        transform.Rotate(UnityEngine.Vector3.forward * rotationthisframe * Time.deltaTime);
        rb.freezeRotation=false;
    }
}

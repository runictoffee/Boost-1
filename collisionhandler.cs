using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionhandler : MonoBehaviour
{
    [SerializeField] float levelloaddelay=5f;
    [SerializeField] AudioClip success ;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successparticles ;
    [SerializeField] ParticleSystem crashparticles ;
    bool isTransitioning = false;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning){return;}
        switch (other.gameObject.tag) 
        {
            case "friendly":
                Debug.Log("this object is friendly ");
                break;
            case "Finish":
                StartsuccessSequence();
                break;
            default:
            StartCrashSequence();
            break;
        }

    }

    void StartsuccessSequence()
    {
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successparticles.Play(successparticles);
        GetComponent<NewBehaviourScript>().enabled=false;
        Invoke("loadNextlevel",levelloaddelay);
    }

    void StartCrashSequence()
    {
        isTransitioning=true   ;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashparticles.Play(crashparticles);
        GetComponent<NewBehaviourScript>().enabled=false;
        Invoke("Reloadlevel",levelloaddelay);
    }
    void Reloadlevel()
    {
        int currentsceneindex=SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentsceneindex );

    }
    void loadNextlevel()
    {
        int currentsceneindex=SceneManager.GetActiveScene().buildIndex;
        int nextsceneindex=currentsceneindex+1;
        if(nextsceneindex==SceneManager.sceneCountInBuildSettings)
        {
            nextsceneindex=0;
        }
        SceneManager.LoadScene(nextsceneindex);
    }
}

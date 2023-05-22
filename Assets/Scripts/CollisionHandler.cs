using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //PARAMETERS
    [SerializeField] ParticleSystem explosionVFX;
    //CACHE - references for readability or speed
    //STATE - private instance (member) variables
    bool isTransitioning = false;
    //PUBLIC METHOD    
    //PRIVATE METHOD
    
    private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning) { return; }
        
        InvokeMethod("ReloadScene", 1f);
    }

    private void InvokeMethod(string methodname, float delayTime)
    {
        if(!explosionVFX.isPlaying)
        {
            explosionVFX.Play();
        }        
        GetComponent<InputHandler>().enabled = false;
        Invoke(methodname, delayTime);
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

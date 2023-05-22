using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //PARAMETERS
    //CACHE - references for readability or speed
    [SerializeField] ParticleSystem explosionVFX;
    //STATE - private instance (member) variables
    private float enemyHP = 50f;

    //PUBLIC METHOD    
    //PRIVATE METHOD

    private void OnParticleCollision(GameObject other)
    {
        if (enemyHP <= 0f && !explosionVFX.isPlaying)
        {
            explosionVFX.Play();
            Destroy(gameObject, .3f);
        }
        //i'll fix the hard code later
        enemyHP -= 5f;
    }
}

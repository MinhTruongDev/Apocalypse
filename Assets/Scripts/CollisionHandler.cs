using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    //PARAMETERS
    //CACHE - references for readability or speed
    //STATE - private instance (member) variables


    //PUBLIC METHOD
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.name + " had hit " + collision.gameObject.name);
    }
    //PRIVATE METHOD

}

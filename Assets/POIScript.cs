using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIScript : MonoBehaviour
{
    bool Visited = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Visited == false)
        {
            other.gameObject.GetComponent<PlayerScript>().AddPOI();
            Visited = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIScript : MonoBehaviour
{
    bool ScanComplete = false;
    bool Scanning = false;
    [SerializeField] float ScanProgress = 0;
    [SerializeField] AudioSource Sonar;

    GameObject Player;
    string ProgressSquares = "□□□□□□□□□□";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && ScanComplete == false)
        {
            Scanning = true;
            Player = other.gameObject;
        }
    }

    private void Update()
    {
        if (Scanning)
        {

            ProgressSquares = ProgressSquares.Remove(Mathf.FloorToInt(ScanProgress), 1).Insert(Mathf.FloorToInt(ScanProgress), "■");
            
            
            ScanProgress += 1f * Time.deltaTime;
            Player.gameObject.GetComponent<PlayerScript>().ChangeText("Scanning signal: [" + ProgressSquares + "]");
        }
        if (ScanProgress >= 10 && !ScanComplete)
        {
            ScanComplete = true;
            Scanning = false; 
            Player.gameObject.GetComponent<PlayerScript>().AddPOI(1);
            Sonar.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Scanning = false;
            Player.gameObject.GetComponent<PlayerScript>().AddPOI(0);
        }
    }
}

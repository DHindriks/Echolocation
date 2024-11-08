using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerScript : MonoBehaviour
{

    Rigidbody rb;

    GameObject Cam;

    [SerializeField] int Speed;

    [SerializeField] TextMeshProUGUI PoiText;
    [SerializeField] int PoiVisited;


    [SerializeField] List<ParticleSystem> particles;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cam = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //forward/backward
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce((Cam.transform.forward * Speed) * Time.deltaTime, ForceMode.VelocityChange);
        }else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce((-Cam.transform.forward * Speed / 2 ) * Time.deltaTime, ForceMode.VelocityChange);
        }

        //Strafe
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce((-Cam.transform.right * Speed / 2) * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce((Cam.transform.right * Speed / 2) * Time.deltaTime, ForceMode.VelocityChange);
        }

        //Up/down
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce((-Cam.transform.up * Speed / 2) * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rb.AddForce((Cam.transform.up * Speed / 2) * Time.deltaTime, ForceMode.VelocityChange);
        }

        //scan
        if (Input.GetKey(KeyCode.Space))
        {
            Scan();
        }
    }

    public void AddPOI()
    {
        PoiVisited++;
        PoiText.text = "Scan POI: " + PoiVisited + "/3";
    }

    void Scan ()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}

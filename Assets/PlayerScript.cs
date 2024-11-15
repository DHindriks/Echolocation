using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerScript : MonoBehaviour
{

    Rigidbody rb;

    GameObject Cam;

    [SerializeField] int Speed;

    [SerializeField] TextMeshProUGUI PoiText;
    [SerializeField] int PoiVisited;

    [SerializeField] List<AudioClip> Scancomplete;
    [SerializeField] AudioSource PSource;


    [SerializeField] List<ParticleSystem> particles;

    [SerializeField] CanvasGroup GameOverScreen;
    [SerializeField] CanvasGroup GameOverText;

    bool ControlsEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cam = transform.GetChild(0).gameObject;
        AddPOI(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ControlsEnabled)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Menu");
            }
            return;
        }

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scan();
        }
    }

    public void AddPOI(int amount)
    {
        PoiVisited += amount;
        if (PoiVisited >= 3)
        {
            PoiText.text = "All signals recovered";
        }
        else
        {
            PoiText.text = "Scan Wreck signals: " + PoiVisited + "/3";
        }
        if (amount > 0)
        {
            PSource.clip = Scancomplete[Random.Range(0, Scancomplete.Count)];
            PSource.Play();
        }
    }

    public void ChangeText(string txt)
    {
        PoiText.text = txt;
    }

    public void GameOver()
    {
        GameOverScreen.alpha = 1;
        GameOverText.alpha = 1;
        ControlsEnabled = false;
    }

    void Scan ()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}

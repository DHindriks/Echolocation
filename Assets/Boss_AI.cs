using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;

public enum States
{
    Roam,
    Attack
}

public class Boss_AI : MonoBehaviour
{
    States Currentstate;
    [SerializeField] float Pullforce = 10;
    [SerializeField] Animator animator;
    [SerializeField] Cinemachine.CinemachineDollyCart dollyCart;
    [SerializeField] AudioSource AttackRoar;
    [SerializeField] Cinemachine.CinemachineVirtualCamera PlayerCam;
    [SerializeField] CanvasGroup GameoverScreen;


    GameObject Target;
    Rigidbody TargetRb;

    void Start()
    {
        SetState(States.Roam);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SetState(States.Attack);
            Target = other.gameObject;
            TargetRb = Target.GetComponent<Rigidbody>();
        }
    }

    void SetState(States NewState)
    {
        switch (NewState)
        {
            case States.Roam:
                animator.SetBool("AttackMode", false);
                Currentstate = States.Roam;
                GameoverScreen.alpha = 0;
                PlayerCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                dollyCart.m_Speed = 15;
                break;

            case States.Attack:
                Currentstate = States.Attack;
                animator.SetBool("AttackMode", true);
                dollyCart.m_Speed = 0;
                PlayerCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;
                AttackRoar.Play();
                break;


        }
    }

    void Update()
    {
        if (Currentstate == States.Attack)
        {
            transform.LookAt(Target.transform);

            TargetRb.AddForce((transform.position - Target.transform.position) * Pullforce * Time.deltaTime);

            if (Vector3.Distance(Target.transform.position, transform.position) <= 15)
            {
                GameoverScreen.alpha = 1 - (Vector3.Distance(Target.transform.position, transform.position) / 15) + 0.2f;
            }
            else if (Vector3.Distance(Target.transform.position, transform.position) > 100)
            {
                SetState(States.Roam);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().GameOver();
            Destroy(this);
        }
    }
}

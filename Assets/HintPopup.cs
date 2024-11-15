using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPopup : MonoBehaviour
{

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float DisplayDelay;
    [SerializeField] KeyCode CancelKey;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Display", DisplayDelay);
    }

    void Display()
    {
        canvasGroup.alpha = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(CancelKey))
        {
            Destroy(this.gameObject);
        }
    }
}

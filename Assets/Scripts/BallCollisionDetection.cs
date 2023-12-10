using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionDetection : MonoBehaviour
{
    public SessionManager sessionManager;
    public GameObject mainMenu;
    public GameObject finishScreen;

    public GameObject activityProps;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("COLIDIU: " + other);
        if (other.gameObject.tag == "lata")
        {
            sessionManager.Save();
            mainMenu.SetActive(true);
            finishScreen.SetActive(true);
            activityProps.SetActive(false);
        }
    }
}

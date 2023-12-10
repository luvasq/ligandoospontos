using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayOut : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject inicio;

    public GameObject activityProps;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            mainMenu.SetActive(true);
            inicio.SetActive(true);
            activityProps.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class checkKeyToStart : MonoBehaviour
{
    public SessionManager sessionManager;
    public GameObject mainMenu;
    public GameObject beforeStartPage;
    public GameObject activityProps;
    public GameObject trashCan;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            activityProps.SetActive(true);
            InstanciarLixo();
            sessionManager.SaveStartTime();

            mainMenu.SetActive(false);
            beforeStartPage.SetActive(false);
        }
    }

    void InstanciarLixo()
    {
        if (trashCan == null || activityProps == null)
        {
            Debug.LogError("Prefab ou objeto pai não atribuído no Inspector!");
            return;
        }

        // Posição desejada para a instância do prefab
        Vector3 posicaoPrefab = new Vector3(
            0.5f + 0.1f * sessionManager.activityConfig.activityConfig["Distância do lixo"],
            0.332f,
            -0.037f
        );

        // Instancia o prefab na posição desejada dentro do objeto pai
        GameObject instanciaPrefab = Instantiate(
            trashCan,
            posicaoPrefab,
            Quaternion.identity,
            activityProps.transform
        );

        instanciaPrefab.tag = "lata";
    }
}

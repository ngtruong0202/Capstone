using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    [Header("Plant Tree")]
    public GameObject pickPanel;
    public Transform contentObj;
    bool isContent;
    public GameObject planttreePanel;


    void Start()
    {
        pickPanel.SetActive(false);
        planttreePanel.SetActive(false);
    }


    public void OnPanelPickUp()
    {
        if (!pickPanel.activeSelf)
        {
            pickPanel.SetActive(true);
            isContent = true;
        }
    }

    public void UnPanelPickUp()
    {
        if (pickPanel.activeSelf)
        {
            pickPanel.SetActive(false);
            isContent = false;
        }
    }

}

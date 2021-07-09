using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ConnectingDotManager : MonoBehaviour
{
    public ConnectingDot[] connectingDot;
    public GameObject[] connectors;
    Connectors[] connectorClass;
    bool problemFinished;
    public TextMeshProUGUI congratulationText;

    public GameObject CongratulationCanvas;

    BoxCollider[] colliders;

    private GameObject initialConnectingDot;
    private bool hitConnectingDot;

    private void Start()
    {
        colliders = new BoxCollider[connectors.Length];
        connectorClass = new Connectors[connectors.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i] = connectors[i].GetComponent<BoxCollider>();
            connectorClass[i] = connectors[i].GetComponent<Connectors>();
        }

    }
    private void Update()
    {
        if (!CongratulationCanvas.activeInHierarchy && !problemFinished)
        {


            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("ConnectingDot"))
                    {
                        initialConnectingDot = hit.collider.gameObject;
                        hitConnectingDot = true;
                    }
                    else
                    {
                        hitConnectingDot = false;

                    }
                }
            }

            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Moved && hitConnectingDot)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("ConnectingDot"))
                    {
                        if (initialConnectingDot != hit.collider.gameObject)
                        {
                            print("diffrentDot");
                            foreach (var item in connectorClass)
                            {
                                if ((item.dots[0].gameObject == initialConnectingDot && item.dots[1].gameObject == hit.collider.gameObject) || (item.dots[1].gameObject == initialConnectingDot && item.dots[0].gameObject == hit.collider.gameObject))
                                {
                                    item.gameObject.GetComponent<BoxCollider>().enabled = false;
                                    item.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                                    break;
                                }
                            }
                            initialConnectingDot = hit.collider.gameObject;
                        }

                    }

                }

            }


            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {


                if (connectorClass.All(item => item.gameObject.GetComponent<BoxCollider>().enabled == false))
                {

                    print("Congratulation");
                    congratulationText.text = "Congrtulation. Now lets try diffrent number ";
                    CongratulationCanvas.SetActive(true);
                    problemFinished = true;
                }


            }
        }









    }

}

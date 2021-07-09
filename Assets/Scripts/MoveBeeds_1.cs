using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MoveBeeds_1 : MonoBehaviour
{
    //Ray ray;
    public BeedData_1 instance;

    public bool finger_is_lifted = true;
    public AudioClip beedMoveUPSound;
    public AudioClip beedMoveDownSound;
    int q = 0;

    [HideInInspector]
    public Vector2 startPos;
    [HideInInspector]
    public Vector2 endPos;
    public static event Action dosomthing;

    public GameObject initialBeed;

    public float[,] initial_position;
    public float[] temp;

    public Camera abacusCam;

    private void OnEnable()
    {
        initial_position = new float[9, 4];
        temp = new float[36];
    }


    private void Start()
    {

    }
    private void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }


        if (!Extensions.IsPointerOverUIObject())
        {


           
            if (Input.touchCount >= 1 && Input.touches[0].phase == TouchPhase.Began)
            {
                //finger_is_lifted = false;
                // startPos = Input.touches[0].position;

                startPos.x = abacusCam.ScreenToWorldPoint(Input.touches[0].position).x;
                startPos.y = abacusCam.ScreenToWorldPoint(Input.touches[0].position).y;

                Ray ray1 = abacusCam.ScreenPointToRay(Input.touches[0].position);

                Debug.DrawRay(ray1.origin, ray1.direction);

                if (Physics.Raycast(ray1, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.tag == "beed")
                    {
                        finger_is_lifted = false;
                        initialBeed = hit.collider.gameObject;
                        for (int i = 0; i < instance.beeds.Count; i++)
                        {
                            initial_position[instance.beeds[i].x, instance.beeds[i].y] = instance.beeds[i].beed.transform.localPosition.y;
                            temp[i] = instance.beeds[i].beed.transform.localPosition.y;

                        }
                    }
                }
            }

            if (Input.touchCount >= 1 && Input.touches[0].phase == TouchPhase.Ended)
            {
                if (finger_is_lifted == false)
                {
                    finger_is_lifted = true;
                    for (int i = 0; i < instance.beeds.Count; i++)
                    {
                        // initial_position[instance.beeds[i].x,instance.beeds[i].y] 
                        instance.beeds[i].beed.transform.localPosition = new Vector3(instance.beeds[i].beed.transform.localPosition.x, initial_position[instance.beeds[i].x, instance.beeds[i].y], instance.beeds[i].beed.transform.localPosition.z);
                    }
                }
                else
                {
                    for (int t = 0; t < instance.beeds.Count; t++)
                    {
                        initial_position[instance.beeds[t].x, instance.beeds[t].y] = instance.beeds[t].beed.transform.localPosition.y;
                        temp[t] = instance.beeds[t].beed.transform.localPosition.y;
                    }

                }
            }


            if (Input.touchCount >= 1 && Input.touches[0].phase == TouchPhase.Moved && finger_is_lifted == false)
            {
                //finger_is_lifted = true;
                //endPos = Input.touches[0].position;
                endPos.x = abacusCam.ScreenToWorldPoint(Input.touches[0].position).x;
                endPos.y = abacusCam.ScreenToWorldPoint(Input.touches[0].position).y;
                if (initialBeed.tag == "beed")
                {
                    for (int i = 0; i < instance.beeds.Count; i++)
                    {
                        if (initialBeed == instance.beeds[i].beed)
                        {
                            if (instance.beeds[i].state == 0)
                            {
                                MoveUP(instance.beeds[i].x, instance.beeds[i].y, instance.beeds[i].state, i);
                                break;
                            }
                            if (instance.beeds[i].state == 1)
                            {
                                //finger_is_lifted = false;
                                MoveDOWN(instance.beeds[i].x, instance.beeds[i].y, instance.beeds[i].state, i);
                                break;
                            }
                        }
                    }
                }

            }

            else if (Input.GetMouseButtonDown(0))
            {

                Ray ray = abacusCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    /*
                    if (hit.collider.gameObject.tag == "beed")
                    {
                        for (int i = 0; i < instance.beeds.Count; i++)
                        {
                            if (hit.collider.gameObject == instance.beeds[i].beed)
                            {
                                if (instance.beeds[i].state == 0)
                                {
                                    MoveUP(hit, instance.beeds[i].x, instance.beeds[i].y, instance.beeds[i].state, i);
                                    break;
                                }
                                if (instance.beeds[i].state == 1)
                                {
                                    MoveDOWN(hit, instance.beeds[i].x, instance.beeds[i].y, instance.beeds[i].state, i);
                                    break;
                                }
                            }
                        }
                    }*/
                }
            }
        }

    }

    public void MoveUP(int x, int y, int state, int i)
    {
        for (int k = y; k < 4; k++)
        {
            if (instance.beeds[i].state == 0)
            {
                if ((endPos.y - startPos.y) <= 0)
                    instance.beeds[i].beed.gameObject.transform.localPosition = new Vector3(-0.2795157f, initial_position[x, k] + (0), -9.051434f);
                else if ((endPos.y - startPos.y) < 0.82f)
                    instance.beeds[i].beed.gameObject.transform.localPosition = new Vector3(-0.2795157f, initial_position[x, k] + (endPos.y - startPos.y), -9.051434f);
                else
                {
                    instance.beeds[i].beed.gameObject.transform.localPosition = new Vector3(-0.2795157f, initial_position[x, k] + (0.82f), -9.051434f);
                    instance.beeds[i].state = 1 - instance.beeds[i].state;
                    finger_is_lifted = true;
                     SoundManager.Instance.Play(beedMoveUPSound);
                    Invoke("delayed_invoke", 0.1f);
                }

                i++;
            }
            else
            {
                break;
            }
        }
      //  SoundManager.Instance.Play(beedMoveUP);
    }



    public void MoveDOWN(int x, int y, int state, int i)
    {
        for (int k = y; k >= 0; k--)
        {
            if (instance.beeds[i].state == 1)
            {
                if ((endPos.y - startPos.y) >= 0)
                {
                    instance.beeds[i].beed.gameObject.transform.localPosition = new Vector3(-0.2795157f, initial_position[x, k] - 0, -9.051434f);

                }
                else if ((endPos.y - startPos.y) < 0 && (endPos.y - startPos.y) > -0.82f)
                {
                    instance.beeds[i].beed.gameObject.transform.localPosition = new Vector3(-0.2795157f, initial_position[x, k] + (endPos.y - startPos.y), -9.051434f);
                }
                else
                {
                    instance.beeds[i].beed.gameObject.transform.localPosition = new Vector3(-0.2795157f, initial_position[x, k] - 0.82f, -9.051434f);
                    instance.beeds[i].state = 1 - instance.beeds[i].state;
                    SoundManager.Instance.Play(beedMoveDownSound);

                    finger_is_lifted = true;
                    Invoke("delayed_invoke", 0.2f);
                }
                i--;
            }
            else
            {
                break;
            }
        }
      //  SoundManager.Instance.Play(beedMoveDOWN);

    }



    void delayed_invoke()
    {
        try
        {
            dosomthing();
        }
        catch
        {
            ;
        }
    }



}







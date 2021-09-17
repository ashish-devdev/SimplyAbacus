using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessGameObjectToPrefab_Wrapper : MonoBehaviour { 
    public AccessGameObjectToPrefab AccessGameObjectToPrefab { get { return FindObjectOfType<AccessGameObjectToPrefab>(); } }
}

public class AccessGameObjectToPrefab : MonoBehaviour
{
    public GameObject canvasWithGraphicRaycaster;

}

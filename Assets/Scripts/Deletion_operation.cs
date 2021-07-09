using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletion_operation : MonoBehaviour
{
    [System.Serializable]
    public class DeletionJsonWrapper
    {
        public AdditionJson[] Del;
    }
    DeletionJsonWrapper jsonData;

    public TextAsset deletion_json_data;

    private void OnEnable()
    {
        jsonData = JsonUtility.FromJson<DeletionJsonWrapper>(deletion_json_data.text);
    }



    void Start()
    {
    }


    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AddPedestrianComponents))]
public class AddPedestrianComponentsEditor : Editor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add Components"))
        {
            Debug.Log("Huy");
        }
    }
}

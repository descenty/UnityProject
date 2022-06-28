using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Road))]
public class RoadEditor : Editor {
    private Road road;
    private GameObject roadPrefab;
    private GameObject fBorder;
    private GameObject bBorder;
    private GameObject lBorder;
    private GameObject rBorder;
    private void OnEnable()
    {
        road = (Road)target;
        roadPrefab = Resources.Load<GameObject>("Prefabs/RoadPrefab");
        fBorder = road.fBorder;
        bBorder = road.bBorder;
        lBorder = road.lBorder;
        rBorder = road.rBorder;
    }
    private void OnSceneGUI()
    {
        Handles.BeginGUI();
        GameObject insRoad;
        if (GUI.Button(new Rect(HandleUtility.WorldToGUIPoint(road.transform.position + road.transform.forward * 2.5f), new Vector2(10, 10)), new GUIContent("ADD", "Add road")))
        {
            if(fBorder != null)
                Undo.DestroyObjectImmediate(fBorder);
            insRoad = Instantiate(roadPrefab, road.transform.position + road.transform.forward * 5f, Quaternion.LookRotation(road.transform.forward));
            Undo.RegisterCreatedObjectUndo(insRoad, "New Road");
            Undo.DestroyObjectImmediate(insRoad.GetComponent<Road>().bBorder.gameObject);
            Selection.activeObject = insRoad;
        }
        if (GUI.Button(new Rect(HandleUtility.WorldToGUIPoint(road.transform.position - road.transform.forward * 2.5f), new Vector2(10, 10)), new GUIContent("ADD", "Add road")))
        {
            if (bBorder != null)
                Undo.DestroyObjectImmediate(bBorder);
            insRoad = Instantiate(roadPrefab, road.transform.position - road.transform.forward * 5f, Quaternion.LookRotation(road.transform.forward));
            Undo.RegisterCreatedObjectUndo(insRoad, "New Road");
            Undo.DestroyObjectImmediate(insRoad.GetComponent<Road>().fBorder.gameObject);
            Selection.activeObject = insRoad;
        }
        if (GUI.Button(new Rect(HandleUtility.WorldToGUIPoint(road.transform.position - road.transform.right * 2.5f), new Vector2(10, 10)), new GUIContent("ADD", "Add road")))
        {
            if (lBorder != null)
                Undo.DestroyObjectImmediate(lBorder);
            insRoad = Instantiate(roadPrefab, road.transform.position - road.transform.right * 5f, Quaternion.LookRotation(road.transform.right));
            Undo.RegisterCreatedObjectUndo(insRoad, "New Road");
            Undo.DestroyObjectImmediate(insRoad.GetComponent<Road>().fBorder.gameObject);
            Selection.activeObject = insRoad;
        }
        if (GUI.Button(new Rect(HandleUtility.WorldToGUIPoint(road.transform.position + road.transform.right * 2.5f), new Vector2(10, 10)), new GUIContent("ADD", "Add road")))
        {
            if (rBorder != null)
                Undo.DestroyObjectImmediate(rBorder);
            insRoad = Instantiate(roadPrefab, road.transform.position + road.transform.right * 5f, Quaternion.LookRotation(road.transform.right));
            Undo.RegisterCreatedObjectUndo(insRoad, "New Road");
            Undo.DestroyObjectImmediate(insRoad.GetComponent<Road>().bBorder.gameObject);
            Selection.activeObject = insRoad;
        }
        Handles.EndGUI();
    }
}

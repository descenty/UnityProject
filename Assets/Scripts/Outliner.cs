using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outliner : MonoBehaviour
{
    private List<OutlineScript> outlines = new List<OutlineScript>();

    [SerializeField] private List<GameObject> outlineGameObjects = new List<GameObject>();
    public GameObject uniqueOutline;

    public Outliner externalOutliner;
    
    public void Init()
    {
        if (!uniqueOutline)
        {
            if (outlineGameObjects.Count == 0)
                outlineGameObjects.Add(gameObject);
            foreach (GameObject target in outlineGameObjects)
            {
                OutlineScript outline = target.AddComponent<OutlineScript>();
                outline.OutlineMode = OutlineScript.Mode.OutlineVisible;
                outline.OutlineWidth = 5f;
                outline.OutlineColor = Color.green;
                outline.enabled = false;
                outlines.Add(outline);
            }
        }
    }
    public void SetOutlineState(bool outlineState)
    {
        if (!uniqueOutline)
        {
            if (externalOutliner)
                externalOutliner.SetOutlineState(outlineState);
            else
                foreach (OutlineScript outline in outlines)
                    outline.enabled = outlineState;
        }
        else 
            uniqueOutline.SetActive(outlineState);
            
    }
}

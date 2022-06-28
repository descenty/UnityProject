using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGrid : MonoBehaviour
{
    GameObject cell;
    public int x, y;
    //При наведении отображение по round маске

    //Надо сделать Cell не с одной точкой, а с четыремя точками, как квадрат

    // Start is called before the first frame update
    void Start()
    {
        cell = Resources.Load<GameObject>(@"Prefabs\Cell");
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject cellObj = Instantiate(cell, transform);

                cellObj.transform.localPosition = new Vector3(i, 0, j);
                cellObj.transform.localRotation = Quaternion.Euler(90, 0, 0);
            }
        }
    }
}

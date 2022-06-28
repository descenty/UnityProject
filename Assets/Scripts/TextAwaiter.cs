using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAwaiter : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        StartCoroutine(TextAnim());
    }
    IEnumerator TextAnim()
    {
        int i = 0;
        while (true)
        {
            if (i == 3)
                i = 0;
            switch (i)
            {
                case 0:
                    GetComponent<Text>().text = "Ожидание оплаты.";
                    break;
                case 1:
                    GetComponent<Text>().text = "Ожидание оплаты..";
                    break;
                case 2:
                    GetComponent<Text>().text = "Ожидание оплаты...";
                    break;
            }
            yield return new WaitForSeconds(0.5f);
            i++;
        }
    }
}

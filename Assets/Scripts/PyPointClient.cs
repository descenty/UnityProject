using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PyPointClient : MonoBehaviour
{
    [System.Serializable]
    public class PickPoint
    {
        public int id;
        public string address;
        public float rating;
        public int cellsCount;
        public string owner;
    }
    void Start() {
        StartCoroutine(GetText());
    }
 
    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/api/pick-points/1/");
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            PickPoint pickPoint = JsonUtility.FromJson<PickPoint>(www.downloadHandler.text);
            print(pickPoint.id);
            print(pickPoint.address);
            print(pickPoint.rating);
            print(pickPoint.cellsCount);
            print(pickPoint.owner);
 
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}

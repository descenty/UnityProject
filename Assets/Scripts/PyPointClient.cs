using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class PyPointClient
{
    public static class Urls
    {
        public static readonly string auth = "token-auth/";
        public static readonly string pickPoints = "pick-points/";
        public static readonly string tokenCheck = pickPoints;
    }
    [System.Serializable]
    public class PickPoint
    {
        public int id;
        public string address;
        public float rating;
        public int cellsCount;
        public string owner;
    }

    private PyPointUITerminal terminal;
    private string url = "";
    private string authToken = "";

     public PyPointClient(PyPointUITerminal terminal, string url){
        this.terminal = terminal;
        this.url = url;
    } 

    private UnityWebRequest UnityAuthWebRequest(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url + Urls.tokenCheck);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Token {authToken}");
        return request;
    }

    public async UniTask<bool> Authenticate(string authToken)
    {
        UnityWebRequest request = UnityAuthWebRequest(url + Urls.tokenCheck);
        await request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success){
            this.authToken = authToken;
            return true;
        }
        else
            return false;
    }

    public async UniTask<List<PickPoint>> GetPickPoints()
    {
        UnityWebRequest request = UnityAuthWebRequest(url + Urls.pickPoints);
        await request.SendWebRequest();
        if (request.responseCode == 403)
        {
            terminal.ShowAuthErrorPage();
            return null;
        }
        else if (request.result != UnityWebRequest.Result.Success)
        {
            terminal.ShowUnexpectedErrorPage();
            return null;
        }
        else
            return JsonUtility.FromJson<List<PickPoint>>(request.downloadHandler.text);
    }
}

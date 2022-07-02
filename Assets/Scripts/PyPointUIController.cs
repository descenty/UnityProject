using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;


public class PyPointUIController : MonoBehaviour
{
    [SerializeField]
    private string authToken = "";
    [SerializeField]
    private string url = "";
    private PyPointClient client;

    [SerializeField]
    private GameObject authenticationPage;
    private GameObject pickPointsPage;
    private GameObject pickPointControlPage;

    private async void Start()
    {
        client = new PyPointClient(this, url);
        if (await client.Authenticate(authToken))
            ShowPickPointsPage();
        else
            ShowAuthenticationPage();

    }

    public void ShowAuthenticationPage(){
    }

    public async void ShowPickPointsPage(){
        List<PyPointClient.PickPoint> pickPoints = await client.GetPickPoints();
    }

    public void ShowAuthErrorPage(){
        print("403 Forbidden");
    }

    public void ShowUnexpectedErrorPage(){
        print("Unexpected error");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Networking; 


public class LoginLogoutRegister : MonoBehaviour
{
    public string baseUrl = "http://localhost:8080/Unity/"; 
    public InputField accountUserName; 
    public InputField accountPassword; 
    public Text info; 

    private string currentUsername; 
    private string ukey = "accountusername"; 

    // Start is called before the first frame update
    void Start()
    {
        currentUsername = ""; 

        if(PlayerPrefs.HasKey(ukey))
        {
            if(PlayerPrefs.GetString(ukey) != "")
            {
                currentUsername = PlayerPrefs.GetString(ukey); 
                info.text = "You are loged in = " + currentUsername; 
            }
            else 
            {
                info.text = "You are not loged in"; 
            }
        }
        else 
        {
            info.text = "You are not loged in."; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccountRegister()
    {
        // string Message = PostInput.text; 
        string uName = accountUserName.text; 
        string pWord = accountPassword.text; 
        StartCoroutine(RegisterNewAccount(uName, pWord));
    }

    public void AccountLogin()
    {
        string uName = accountUserName.text; 
        string pWord = accountPassword.text; 
        StartCoroutine(LogInAccount(uName, pWord)); 
    }

    public void AccountLogout()
    {
        currentUsername = " "; 
        PlayerPrefs.SetString(ukey, currentUsername); 
        info.text = "You are just loged out"; 
    }

    IEnumerator RegisterNewAccount(string uName, string pWord)
    {
        WWWForm form = new WWWForm(); 
        form.AddField("newAccountUsername", uName);
        form.AddField("newAccountPassword", pWord);
        using (UnityRequest www = UnityWebRequest.Post("http://localhost:8080/Unity/", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest(); 

            if(www.isNetworkError)
            {
                Debug.Log(www.error); 

            }
            else 
            {
                string resonseText = www.downloadHandler.text; 
                Debug.Log("Response Text from the server = " + responseText); 
                info.text = "Response Text from the server = " + responseText;

            }
        }
    }

    IEnumerator LogInAccount(string uName, string pWord)
    {
        WWWForm form = new WWWForm(); 
        form.AddField("loginUsername", uName); 
        form.AddField("loginPassword", pWord); 
        using (UnityRequest www = UnityWebRequest.Post("http://localhost:8080/Unity/", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer(); 
            yield return www.SendWebRequest(); 

            if(www.isNetworkError)
            {
                Debug.Log(www.error); 
            }
            else 
            {
                string responseText = www.downloadHandler.text; 
                Debug.Log("Response Text from the server = " + responseText); 
                info.text = "Response Text from the server " + responseText; 
                
            }
        }
    }
}

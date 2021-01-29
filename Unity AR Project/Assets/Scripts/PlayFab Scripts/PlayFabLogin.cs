using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;


public class PlayFabLogin : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject addLoginPanel;
    public GameObject recoverButton;
    
    public Text errorMessage;

    private string userEmail;
    private string userPassword;
    private string username;

    public void Start()
    {
        //If the Title ID is null or empty, goes in here
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "2B652";
        }

        //test
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true }; //request to log in
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure); //submit the request

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
        else
        {
#if UNITY_ANDROID
            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginAndroidSuccess, OnLoginAndroidFailure);
#endif
        }

    }

    #region LOGIN ANDROID
    private static string ReturnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }
    private void OnLoginAndroidSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");

        loginPanel.SetActive(false);
    }
    private void OnLoginAndroidFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnAddLoadSuccess(AddUsernamePasswordResult result)
    {
        Debug.Log("Login android success");
        errorMessage.text = "Login Successful";
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);

        addLoginPanel.SetActive(false);
    }
    #endregion

    #region LOGIN PlayerPrefs
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);

        loginPanel.SetActive(false);
        recoverButton.SetActive(false);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        errorMessage.text = "ERROR: Wrong email or password.";
        //var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username }; //request to register the user
        //PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);

        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        errorMessage.text = "ERROR: " + error.GenerateErrorReport();
    }
    #endregion


    #region GET
    public void getUserEmail(string p_email)
    {
        userEmail = p_email;
    }

    public void getUserPassword(string p_password)
    {
        userPassword = p_password;
    }

    public void getUsername(string p_username)
    {
        username = p_username;
    }
    #endregion


    public void onClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    public void onClickAddLogin()
    {
        var addLoginRequest = new AddUsernamePasswordRequest { Email = userEmail, Password = userPassword, Username = username };
        PlayFabClientAPI.AddUsernamePassword(addLoginRequest, OnAddLoadSuccess, OnRegisterFailure);
    }
    public void OpenRegister()
    {
        registerPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
    public void onClickRegister()
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username }; //request to register the user
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }
    public void OpenAddLogin()
    {
        addLoginPanel.SetActive(true);
    }
}

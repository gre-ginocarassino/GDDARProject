using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using PlayFab.Json;
using UnityEngine.SceneManagement;

public class PlayFabController : MonoBehaviour
{
    public static PlayFabController PFC; //Singleton

    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject addLoginPanel;
    public GameObject recoverButton;

    public GameObject loadingPanel;

    public Text errorMessage;

    private string userEmail;
    private string userPassword;
    public string username;
    private string myID;

    private void OnEnable()
    {
        if (PlayFabController.PFC == null)
        {
            PlayFabController.PFC = this;
        }
        else
        {
            if (PlayFabController.PFC != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private IEnumerator ActivationRoutine()
    {
        //Wait for 14 secs.
        //yield return new WaitForSeconds(0);

        //Turn My game object that is set to false(off) to True(on).
        loadingPanel.SetActive(true);

        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(3);

        //Game object will turn off
        loadingPanel.SetActive(false);
    }   

    public void Start()
    {
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");

        StartCoroutine(ActivationRoutine());

        //If the Title ID is null or empty, goes in here
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "2B652";
        }

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
        else
        {
            //#if UNITY_ANDROID
            //var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
            //PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginAndroidSuccess, OnLoginAndroidFailure);
            //#endif
        }

    }

    #region LOGIN
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

        OnChangeScene();
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
        OnChangeScene();
    }
    #endregion

    #region LOGIN NORMAL
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);

        loginPanel.SetActive(false);
        recoverButton.SetActive(false);

        myID = result.PlayFabId;
        OnChangeScene();
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

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = username }, onDisplayName, OnLoginAndroidFailure); //set displayname as username
        SetBeginningStats();

        OnChangeScene();
    }
    void onDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " is your new display name.");
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        errorMessage.text = "ERROR: " + error.GenerateErrorReport();
    }
    #endregion
    #endregion

    public void OnChangeScene()
    {
        SceneManager.LoadScene("AR-Playscene");
    }
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

    public void SetBeginningStats()
    {
        //sending POST request with UpdatePlayerStatistic function
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "PlayerLevel", Value = 0 },
        new StatisticUpdate { StatisticName = "PlayerPoints", Value = 0 },
        new StatisticUpdate { StatisticName = "GeoPoints", Value = 0 },
        new StatisticUpdate { StatisticName = "TotalEngland", Value = 0 },
        new StatisticUpdate { StatisticName = "TotalFrance", Value = 0 },
        new StatisticUpdate { StatisticName = "TotalItaly", Value = 0 },
        new StatisticUpdate { StatisticName = "TotalChina", Value = 0 },
        new StatisticUpdate { StatisticName = "TotalBulgaria", Value = 0 },
    }
        },
        result => { Debug.Log("User statistics updated"); }, //callback for successful POST
        error => { Debug.LogError(error.GenerateErrorReport()); }); //failed POST
    }

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
}

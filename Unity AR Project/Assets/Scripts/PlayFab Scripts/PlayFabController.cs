using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using PlayFab.Json;

public class PlayFabController : MonoBehaviour
{
    public static PlayFabController PFC; //Singleton

    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject addLoginPanel;
    public GameObject recoverButton;
    
    public Text errorMessage;

    private string userEmail;
    private string userPassword;
    private string username;

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

    public void Start()
    {
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
            #if UNITY_ANDROID
            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginAndroidSuccess, OnLoginAndroidFailure);
            #endif
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

        GetStatistics();
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

        GetStatistics();
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

        GetStatistics();
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

        GetStatistics();
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
    #endregion

    #region PlayerStats
    public int playerLevel;
    public int playerPoints;

    public void SetStats()
    {
        //sending POST request with UpdatePlayerStatistic function
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "PlayerLevel", Value = playerLevel },
    }
        },
        result => { Debug.Log("User statistics updated"); }, //callback for successful POST
        error => { Debug.LogError(error.GenerateErrorReport()); }); //failed POST
    }

    void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "PlayerLevel":
                    playerLevel = eachStat.Value;
                    break;
                case "PlayerPoints":
                    playerPoints = eachStat.Value;
                    break;
            }
        }
            
    }
    // Build the request object and access the API
    public void StartCloudUpdatePlayerStats()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { argsPlayerLevel = playerLevel, argsPlayerPoints = playerPoints }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudUpdateStats, OnErrorShared);
    }
    // OnCloudHelloWorld defined in the next code block

    private static void OnCloudUpdateStats(ExecuteCloudScriptResult result)
    {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        //Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        Debug.Log("Prova JSON: " + PlayFab.PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    #endregion

    #region LeaderBoard
    public GameObject leaderboardPanel;
    public GameObject listingPrefab;
    public Transform listingContainer;

    public void getLeaderBoard()
    {
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerLevel", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, onGetLeaderboard, onErrorLeaderboard);
    }

    void onGetLeaderboard(GetLeaderboardResult result)
    {
        leaderboardPanel.SetActive(true);

        //loops through each player in the leaderbord
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject tempListin = Instantiate(listingPrefab, listingContainer);
            LeaderboardListing LL = tempListin.GetComponent<LeaderboardListing>();
            LL.playerNameText.text = player.DisplayName;
            LL.playerScoreText.text = player.StatValue.ToString();

            Debug.Log(player.DisplayName + ": " + player.StatValue);
        }
    }
    void onErrorLeaderboard(PlayFabError error)
    { Debug.Log(error.GenerateErrorReport());  }

    public void closeLeaderboardPanel()
    {
        leaderboardPanel.SetActive(false);
        for (int i = listingContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(listingContainer.GetChild(i).gameObject);
        }
    }
    #endregion

}

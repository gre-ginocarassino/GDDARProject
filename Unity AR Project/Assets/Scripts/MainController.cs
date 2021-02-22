using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using PlayFab.Json;

public class MainController : MonoBehaviour
{
    public static MainController MCC; //Singleton

    public string MyPlayfabID;
    public string MyPlayfabUsername;

    public GameObject leaderboardPanel;
    public GameObject listingPrefab;
    public Transform listingContainer;
    public Text UsernameText;
    public Text LVText;


    private void OnEnable()
    {
        if (MainController.MCC == null)
        {
            MainController.MCC = this;
        }
        else
        {
            if (MainController.MCC != this)
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

        GetAccountInfo();
    }

    #region AccountInfo
    

    void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Successs, fail);
    }


    void Successs(GetAccountInfoResult result)
    {

        MyPlayfabID = result.AccountInfo.PlayFabId;
        MyPlayfabUsername = result.AccountInfo.Username;
        UsernameText.text = MyPlayfabUsername;

    }


    void fail(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion

    #region LeaderBoard

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
    { Debug.Log(error.GenerateErrorReport()); }

    public void closeLeaderboardPanel()
    {
        leaderboardPanel.SetActive(false);
        for (int i = listingContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(listingContainer.GetChild(i).gameObject);
        }
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
    //
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
}

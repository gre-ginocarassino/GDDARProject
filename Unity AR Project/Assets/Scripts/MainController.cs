using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using PlayFab.Json;

public class MainController : MonoBehaviour
{
    #region [SINGLETON]
    public static MainController MCC; //Singleton
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
    #endregion

    #region[PLAYER STATS]
    public string MyPlayfabID;
    public string MyPlayfabUsername;
    public int playerLevel;
    public int playerPoints;
    public int totalEngland;
    public int totalFrance;
    public int totalItaly;
    public int totalChina;
    public int totalBulgaria;
    public int geoPoints;

    public bool GeoEngland;
    public bool GeoItaly;
    public bool GeoFrance;
    #endregion

    #region [BAR]
    public Text T_Username;
    public Text T_Level;
    public Text T_Points;
    public Text T_GeoPoints;
    #endregion

    #region [COUNTRIES_STATS]
    public Text ENG_Points;
    public Text ITA_Points;
    public Text FRA_Points;

    public GameObject IMG_GeoEng;
    public GameObject IMG_GeoIta;
    public GameObject IMG_GeoFra;
    #endregion

    public GameObject leaderboardPanel;
    public GameObject listingPrefab;
    public Transform listingContainer;
    public GameObject loadingPanel;

    public void Start()
    {
        GetStatistics();

        StartCoroutine(ActivationRoutine());

        //If the Title ID is null or empty, goes in here
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "2B652";
        }

        GetAccountInfo();

        
    }

    private void Update()
    {

    }

    public void UpdateGameStatistics()
    {
        T_Level.text = playerLevel.ToString();
        T_Points.text = playerPoints.ToString();
        T_GeoPoints.text = geoPoints.ToString();
        ENG_Points.text = totalEngland.ToString();
        ITA_Points.text = totalItaly.ToString();
        FRA_Points.text = totalFrance.ToString();

        UpdateTotalScore();
        UpdateLevel();
        UpdateGeoPoints(200);
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
        T_Username.text = MyPlayfabUsername;
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

    #region [GET/SET_STATS]
    public void SetStats()
    {
        //sending POST request with UpdatePlayerStatistic function
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "PlayerLevel", Value = playerLevel },
        new StatisticUpdate { StatisticName = "PlayerPoints", Value = playerPoints },
        new StatisticUpdate { StatisticName = "GeoPoints", Value = geoPoints },
        new StatisticUpdate { StatisticName = "TotalEngland", Value = totalEngland },
        new StatisticUpdate { StatisticName = "TotalFrance", Value = totalFrance },
        new StatisticUpdate { StatisticName = "TotalItaly", Value = totalItaly },
        new StatisticUpdate { StatisticName = "TotalChina", Value = totalChina },
        new StatisticUpdate { StatisticName = "TotalBulgaria", Value = totalBulgaria },
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
                case "GeoPoints":
                    geoPoints = eachStat.Value;
                    break;
                case "TotalEngland":
                    totalEngland = eachStat.Value;
                    break;
                case "TotalFrance":
                    totalFrance = eachStat.Value;
                    break;
                case "TotalItaly":
                    totalItaly = eachStat.Value;
                    break;
                case "TotalChina":
                    totalChina = eachStat.Value;
                    break;
                case "TotalBulgaria":
                    totalBulgaria = eachStat.Value;
                    break;
            }
        }

        UpdateGameStatistics();
    }
    public void StartCloudUpdatePlayerStats()
    {

        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { argsPlayerLevel = playerLevel,
                argsPlayerPoints = playerPoints,
                argsGeoPoints = geoPoints,
                argsTotalEngland = totalEngland,
                argsTotalFrance = totalFrance,
                argsTotalItaly = totalItaly,
                argsTotalChina = totalChina,
                argsTotalBulgaria = totalBulgaria
            }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudUpdateStats, OnErrorShared);
    }
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

    #region [INTERNAL_ECONOMICS]
    private void UpdateTotalScore()
    {
        playerPoints = totalEngland + totalFrance + totalItaly + totalChina + totalBulgaria;
        T_Points.text = playerPoints.ToString();
    }
    private void UpdateLevel()
    {
        int a = 30;

        playerLevel = playerPoints / a;
        T_Level.text = playerLevel.ToString();
    }
    private void UpdateGeoPoints(int threshold)
    {
        if (totalEngland >= threshold && GeoEngland == false)
        {
            geoPoints++;
            GeoEngland = true;
            IMG_GeoEng.SetActive(true);
        }
        if (totalItaly >= threshold && GeoItaly == false)
        {
            geoPoints++;
            GeoItaly = true;
            IMG_GeoIta.SetActive(true);
        }
        if (totalFrance >= threshold && GeoFrance == false)
        {
            geoPoints++;
            GeoFrance = true;
            IMG_GeoFra.SetActive(true);
        }
    }
    #endregion

    private IEnumerator ActivationRoutine()
    {
        loadingPanel.SetActive(true);

        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(3);

        //Game object will turn off
        loadingPanel.SetActive(false);
    }
}

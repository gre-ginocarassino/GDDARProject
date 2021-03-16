using RESTClient;
using Azure.StorageServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Xml;

public class AssetBundleAzure : MonoBehaviour
{
    [Header("Azure Storage Service")]
    [SerializeField]
    private string storageAccount;
    [SerializeField]
    private string accessKey;
    [SerializeField]
    private string container;

    private StorageServiceClient client;
    private BlobService blobService;
    private PlaceController pc;

    [Header("Asset Bundle Scene")]
    public Text label;
    public string assetBundleName;
    public string assetName;
    private AssetBundle assetBundle;
    private GameObject loadedObject;

    [Header("Audio")]
    public AudioSource audioSource;

    private string localPath;
    private string saveFileXML = "scene.xml";
    private string saveFileJSON = "scene.json";

    // Use this for initialization
    void Start()
    {
        if (string.IsNullOrEmpty(storageAccount) || string.IsNullOrEmpty(accessKey))
        {
            Log.Text(label, "Storage account and access key are required", "Enter storage account and access key in Unity Editor", Log.Level.Error);
        }

        client = StorageServiceClient.Create(storageAccount, accessKey);
        blobService = client.GetBlobService();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        UnloadAssetBundle();
    }

    private void UnloadAssetBundle()
    {
        if (assetBundle != null)
        {
            RemovePrefabs();
            loadedObject = null;
            assetBundle.Unload(true);
            Debug.Log("Unloaded Asset Bundle");
        }
    }

    public void TappedLoadAssetBundle(PlaceController currencity)
    {
        pc = currencity;

        UnloadAssetBundle();
        string filename = assetBundleName + "-" + GetAssetBundlePlatformName() + ".unity3d";
        string resourcePath = container + "/" + filename;
        Log.Text(label, "Load asset bundle: " + resourcePath);
        StartCoroutine(blobService.GetAssetBundle(GetAssetBundleComplete, resourcePath));

        currencity.baseSections = loadedObject;
    }

    private void GetAssetBundleComplete(IRestResponse<AssetBundle> response)
    {
        if (response.IsError)
        {
            Log.Text(label, "Failed to load asset bunlde: " + response.StatusCode, response.ErrorMessage, Log.Level.Error);
        }
        else
        {
            Log.Text(label, "Loaded Asset Bundle:" + response.Url);
            assetBundle = response.Data;
            StartCoroutine(LoadAssets(assetBundle, assetName)); ;
        }
    }

    private IEnumerator LoadAssets(AssetBundle bundle, string name)
    {
        // Load the object asynchronously
        AssetBundleRequest request = bundle.LoadAssetAsync(name, typeof(GameObject));

        // Wait for completion
        yield return request;

        // Get the reference to the loaded object
        loadedObject = request.asset as GameObject;
        loadedObject.tag = "Player";

        AddPrefab(new Vector3(0, 4, 0));
        Log.Text(label, "+ Prefab" + loadedObject.name, "Added prefab name: " + loadedObject.name);
    }

    private void AddPrefab(Vector3 position = default(Vector3))
    {
        AddPrefab(position, Quaternion.identity, Vector3.one, Color.clear);
    }

    private void AddPrefab(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
    {
        if (assetBundle == null || loadedObject == null)
        {
            Log.Text(label, "Load asset bundle first", "Error, Asset Bundle was null", Log.Level.Warning);
            return;
        }
        GameObject gameObject = Instantiate(loadedObject, pc.transform);
        gameObject.transform.localScale = scale;

        //gameObject.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }

    public void TappedRemovePrefabs()
    {
        if (assetBundle == null)
        {
            Log.Text(label, "Tap 'Load Asset Bundle' first", "No asset bundles loaded", Log.Level.Warning);
            return;
        }
        RemovePrefabs();
        Log.Text(label, "- Remove Prefabs", "Remove Prefabs");
    }

    private void RemovePrefabs()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }
    }

    private string GetAssetBundlePlatformName()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                return SystemInfo.operatingSystem.Contains("64 bit") ? "x64" : "x86";
            case RuntimePlatform.WSAPlayerX86:
            case RuntimePlatform.WSAPlayerX64:
            case RuntimePlatform.WSAPlayerARM:
                return "WSA";
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                return "OSX";
            default:
                throw new Exception("Platform not listed");
        }
    }
}

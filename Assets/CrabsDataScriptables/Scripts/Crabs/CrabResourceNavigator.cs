using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using CrabWorld.Aquatic;
using CrabWorld.Waters;
using rIAEugth.vseioAW.segAIWUt;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.Networking;

namespace CrabWorld.SeaLife
{
    public class CrabResourceNavigator : MonoBehaviour
    {
        private string crabString1 { get; set; }
        private string crabString2;
        private int crabNumber;

        private string crabTraceCode;
        private string seaLabel;

        private bool isFirstInstance = true;
        private NetworkReachability networkReachability = NetworkReachability.NotReachable;

        [SerializeField] private CrabMysteries _crabSecrets;
        [SerializeField] private IDFAController _idfaCheckCrab;
        [SerializeField] private CrabMerge _crabMerge;


        private void Awake()
        {
            CrabBoolCheck();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _idfaCheckCrab.ScrutinizeIDFA();
            StartCoroutine(FetchAdvertisingID());

            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    NoData();
                    break;
                default:
                    CheckStoredData();
                    break;
            }
        }

        private void CrabBoolCheck()
        {
            switch (isFirstInstance)
            {
                case true:
                    isFirstInstance = false;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private IEnumerator FetchAdvertisingID()
        {
#if UNITY_IOS
            var authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            while (authorizationStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                yield return null;
            }
#endif

            crabTraceCode = _idfaCheckCrab.RetrieveAdvertisingID();
            yield return null;
        }

        private void CheckStoredData()
        {
            if (PlayerPrefs.GetString("top", string.Empty) != string.Empty)
            {
                LoadStoredData();
            }
            else
            {
                FetchDataFromServerWithDelay();
            }
        }

        private void LoadStoredData()
        {
            crabString1 = PlayerPrefs.GetString("top", string.Empty);
            crabString2 = PlayerPrefs.GetString("top2", string.Empty);
            crabNumber = PlayerPrefs.GetInt("top3", 0);
            tyuioImposafa();
        }

        private void FetchDataFromServerWithDelay()
        {
            Invoke(nameof(ReceiveData), 7.4f);
        }

        private void ReceiveData()
        {
            if (Application.internetReachability == networkReachability)
            {
                NoData();
            }
            else
            {
                StartCoroutine(dfghjFetsach());
            }
        }


        private IEnumerator dfghjFetsach()
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(_crabMerge.MergeWaves(dataScriiptable_two));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                NoData();
            }
            else
            {
                edfghjkokjnbGefaFDATa(webRequest);
            }
        }

        private void edfghjkokjnbGefaFDATa(UnityWebRequest webRequest)
        {
            string tokenConcatenation = _crabMerge.MergeWaves(_nameScriptableDataShork);

            if (webRequest.downloadHandler.text.Contains(tokenConcatenation))
            {
                try
                {
                    string[] dataParts = webRequest.downloadHandler.text.Split('|');
                    PlayerPrefs.SetString("top", dataParts[0]);
                    PlayerPrefs.SetString("top2", dataParts[1]);
                    PlayerPrefs.SetInt("top3", int.Parse(dataParts[2]));

                    crabString1 = dataParts[0];
                    crabString2 = dataParts[1];
                    crabNumber = int.Parse(dataParts[2]);
                }
                catch
                {
                    PlayerPrefs.SetString("top", webRequest.downloadHandler.text);
                    crabString1 = webRequest.downloadHandler.text;
                }

                tyuioImposafa();
            }
            else
            {
                NoData();
            }
        }

        private void tyuioImposafa()
        {
            _crabSecrets.TigerSTR = $"{crabString1}?idfa={crabTraceCode}";
            _crabSecrets.TigerSTR +=
                $"&gaid={AppsFlyer.getAppsFlyerId()}{PlayerPrefs.GetString("userSave", string.Empty)}";
            _crabSecrets.CrabGlobalPosition = crabString2;


            Kasbsmnsbgs();
        }

        public void Kasbsmnsbgs()
        {
            _crabSecrets.CrabToolbarDepth = crabNumber;
            _crabSecrets.gameObject.SetActive(true);
        }

        private void NoData()
        {
            DusabeHash();
        }

        [SerializeField] private List<string> _nameScriptableDataShork;
        [SerializeField] private List<string> dataScriiptable_two;

        private void DusabeHash()
        {
            gameObject.SetActive(false);
        }
    }
}
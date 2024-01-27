using BananaWorld.Fruits;
using UnityEngine;

namespace CrabWorld.Aquatic
{
    public class CrabMysteries : MonoBehaviour
    {
        private UniWebView _oceanView;
        private GameObject crabInteraction;
        private string crabIdentifier;

        public string CrabGlobalPosition;
        public int CrabToolbarDepth = 70;
        public CrabManager CrabController;

        private void Start()
        {
            SetupCrabData();
            SetCrabIdentifierStart(crabIdentifier);
            ActivateCrabProcess();
        }

        public void OnEnable()
        {
            CrabController.SetupCrab();
        }

        private void InitPool()
        {
            _oceanView = GetComponent<UniWebView>();
            if (_oceanView == null)
            {
                _oceanView = gameObject.AddComponent<UniWebView>();
            }

            _oceanView.OnShouldClose += _ => false;

            // Other initialization logic...
        }

        private void SetupCrabData()
        {
            InitPool();

            switch (TigerSTR)
            {
                case "0":
                    _oceanView.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _oceanView.SetShowToolbar(false);
                    break;
            }

            _oceanView.Frame = new Rect(0, CrabToolbarDepth, Screen.width, Screen.height - CrabToolbarDepth);

            // Other setup logic...

            _oceanView.OnPageFinished += (_, _, url) =>
            {
                if (PlayerPrefs.GetString("LastLoadedPage", string.Empty) == string.Empty)
                {
                    PlayerPrefs.SetString("LastLoadedPage", url);
                }
            };
        }

        private void SetCrabIdentifierStart(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                _oceanView.Load(url);
            }
        }

        private void ActivateCrabProcess()
        {
            if (crabInteraction != null)
            {
                crabInteraction.SetActive(false);
            }
        }

        public string TigerSTR
        {
            get => crabIdentifier;
            set => crabIdentifier = value;
        }
    }
}
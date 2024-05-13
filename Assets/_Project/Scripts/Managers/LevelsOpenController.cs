using System;
using GoogleMobileAds.Api;
using Integration;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Managers
{
    public class LevelsOpenController : MonoBehaviour
    {
        [Inject] private IAPService _iapService;
        [Inject] private AdMobController _adMobController;
        [Inject] private BannerViewController _bannerView;
        private static int _opened;

        private void Start()
        {
            _opened++;
            if ((_opened + 1) % 2 == 0)
            {
                _adMobController.ShowInterstitialAd();
                _adMobController.ShowBanner(true);
                _bannerView.ChangeBannerPosition(AdPosition.TopRight);
            }
            else
            {
                _iapService.ShowSubscriptionPanel();
            }
        }
    }
}
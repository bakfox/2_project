using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ad_gold_more_sc : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public trun_manager trun_manager_temp;
    public main_menu_manager main_menu_temp;
    public void Start()
    {
        InitAds();
    }

    public void InitAds()
    {
        string adUnitId;

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif

        RewardedAd.Load(adUnitId, new AdRequest.Builder().Build(), LoadCallback);
    }
    public void LoadCallback(RewardedAd rewardedAd, LoadAdError loadAdError)
    {
        if (rewardedAd != null)
        {
            this.rewardedAd = rewardedAd;
            Debug.Log("로드성공");
        }
        else
        {
            Debug.Log(loadAdError.GetMessage());
        }

    }
    public void ShowAds()
    {
        if (rewardedAd.CanShowAd())
        {
            rewardedAd.Show(GetReward);
        }
        else
        {
            InitAds();
            Debug.Log("광고 재생 실패");
        }
    }
    public void GetReward(Reward reward)//사용 하는곳 
    {
        if (trun_manager_temp != null)
        {
            trun_manager_temp.clear_gold_temp.more_gold = true;
            trun_manager_temp.get_gold();
        }
        else
        {
            main_menu_temp.clear_gold_temp.more_gold = true;
            main_menu_temp.gold_get();
        }
        
        InitAds();
    }


}

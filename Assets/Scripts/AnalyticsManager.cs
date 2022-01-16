using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    public string playerName;

    private float secondsElapsed = 0.0f;

    Dictionary<string, object> customParams;

    private void Awake()
    {
        customParams = new Dictionary<string, object>();
        customParams.Add("player_name", playerName);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        secondsElapsed = 0.0f;
        AnalyticsEvent.GameStart();
        Analytics.CustomEvent("GetPlayerName", customParams);

    }

    private void Update()
    {
        secondsElapsed += Time.deltaTime;

    }

    private void OnApplicationQuit()
    {
        customParams.Add("seconds_played", secondsElapsed);
        AnalyticsEvent.GameOver("Test Game", customParams);
    }
}

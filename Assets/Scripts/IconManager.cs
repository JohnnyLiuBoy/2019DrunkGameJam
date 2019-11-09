using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class IconManager : Photon.MonoBehaviour
{
    [System.Serializable]
    public class Mission
    {
        public string Title;
        public string Content;
    }
    public static IconManager instance;

    //啤酒icon
    public float BeerCount;
    public float MaxBeer;
    public Image Beer_Bar;

    //時間Bar
    public bool isGameOver;
    public float timer;
    public float MaxTimer;
    public Text Time_Text;
    public Image Time_Bar;

    //分數
    public int Score;
    public Text Score_text;

    //任務清單
    public GameObject MissionObject;
    public Transform MissionParent;
    public Mission[] MissionContent;
    public List<Toggle> Checklist;

    void Awake()
    {
        #region singleton
        if (instance != null) { DestroyImmediate(this); }
        else { instance = this; }
        #endregion
    }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if(!isGameOver)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                Time_Bar.fillAmount = timer / MaxTimer;
                int Minute = Mathf.FloorToInt(timer/60);
                int Second = Mathf.FloorToInt(timer % 60);
                Time_Text.text = Minute.ToString() + ":" + Second.ToString();
            }
            else
            {
                GameOver();
                isGameOver = true;
            }
            if(PhotonNetwork.isMasterClient)
            {
                photonView.RPC("SyncValue", PhotonTargets.Others, timer, Score, BeerCount);
            }
        }
        Beer_Bar.fillAmount = Mathf.Lerp(Beer_Bar.fillAmount, BeerCount / MaxBeer,5*Time.deltaTime);
    }

    private void Reset()
    {
        timer = MaxTimer;
        int Minute = Mathf.FloorToInt(timer / 60);
        int Second = Mathf.FloorToInt(timer % 60);
        Time_Text.text = Minute.ToString() + ":" + Second.ToString();

        Score = 0;
        Score_text.text = "分數：" + Score.ToString();

        for(int i = 0;i< MissionContent.Length;i++)
        {
            GameObject mission = Instantiate(MissionObject, MissionParent);
            mission.name = "Mission" + i.ToString();
            mission.transform.GetChild(2).GetComponent<Text>().text = MissionContent[i].Content;
            mission.transform.GetChild(3).GetComponent<Text>().text = MissionContent[i].Title;

            Checklist.Add(mission.GetComponent<Toggle>());
        }
    }

    //完成任務
    public void MissionCheck(int check)
    {
        Checklist[check].isOn = true;
    }

    //喝酒～數值
    public void Drink(float l)
    {
        BeerCount += l*Time.deltaTime;
    }

    public void GameOver() 
    {
        //SceneManager.LoadScene(2);
        Debug.Log("GameOver了");
    }

    public void AddScore(int score)
    {
        Score += score;
        Score_text.text = "分數："+Score.ToString();
    }

    [PunRPC]
    void SyncValue(float time, int score,float beer) 
    {
        timer = time;
        Score = score;
        BeerCount = beer;
    }
}

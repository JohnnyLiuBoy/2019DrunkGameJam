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
    public bool isStart;
    public float timer;
    public float MaxTimer;
    public Text Time_Text;
    public Image Time_Bar;
    public RectTransform Clock_Niddel;

    //分數
    public int Score;
    public Text Score_text;

    //任務清單
    public GameObject MissionObject;
    public Transform MissionParent;
    public Mission[] MissionContent;
    public List<Toggle> Checklist;

    //提示
    public Text HintText;

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
        if (PhotonNetwork.player.NickName == "DrunkMan")
        {
            HintText.text = "你是可憐的上班族\n避開一路上的障礙物\n想辦法回到家吧！";
        }
        else if (PhotonNetwork.player.NickName == "Movey")
        {
            HintText.text = "你是酒精\n按 W A S D 可以影響宿主的步伐\n讓他到不了家吧！";
        }
        else if (PhotonNetwork.player.NickName == "Balancy")
        {
            HintText.text = "你是酒精\n按 上下左右 可以影響宿主的平衡\n讓他到不了家吧！";
        }
        else if (PhotonNetwork.player.NickName == "Frecky")
        {
            HintText.text = "你是酒精\n按 H J K L 可以影響宿主的精神狀態\n讓他到不了家吧！";
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (timer < MaxTimer)
            {
                timer += Time.deltaTime;
                Time_Bar.fillAmount = timer / MaxTimer;
                int Minute = Mathf.FloorToInt(timer/60);
                int Second = Mathf.FloorToInt(timer % 60);
                Time_Text.text = Minute.ToString() + ":" + Second.ToString();
                Clock_Niddel.localEulerAngles = new Vector3(0, 0, timer / MaxTimer * -90);
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
        int Minute = Mathf.FloorToInt(timer / 60);
        int Second = Mathf.FloorToInt(timer % 60);
        Time_Text.text = Minute.ToString() + ":" + Second.ToString();

        Score = 0;
        Score_text.text = Score.ToString();

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
        Score_text.text = Score.ToString();
    }

    [PunRPC]
    void SyncValue(float time, int score,float beer) 
    {
        timer = time;
        Score = score;
        BeerCount = beer;
    }
}

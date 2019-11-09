using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public static IconManager instance;

    //啤酒icon
    public int BeerCount;
    public GameObject[] Beers;
    public float BeerL;
    public Image Beer_Bar;

    //時間Bar
    public float timer;
    public Text Time_Text;
    public Image Time_Bar;
    public float MaxTime;

    //分數
    public float score;
    public Text Score_text;

    //任務清單
    public bool[] CheckList;
    public Toggle[] toggles;

    void Awake()
    {
        #region singleton
        if (instance != null) { DestroyImmediate(this); }
        else { instance = this; }
        #endregion
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    //完成任務
    public void MissionCheck(int check)
    {
        CheckList[check] = true;
        toggles[check].isOn = true;
    }

    //喝酒～
    public void Drink()
    {
        BeerCount += 1;
        foreach(var beer in Beers)
        {
            beer.SetActive(false);
        }
        for (int i = 0;i< BeerCount;i++)
        {
            Beers[i].SetActive(true); 
        }
    }

    public void Drink(float l)
    {
        BeerL += l;
        Beer_Bar.fillAmount = BeerL/ 100;
    }
}

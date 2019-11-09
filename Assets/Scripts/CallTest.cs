using UnityEngine;

public class CallTest : MonoBehaviour
{
    public float DrinkValue;
    public int Mission;
    public int score;

    void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            IconManager.instance.Drink(DrinkValue);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            IconManager.instance.MissionCheck(Mission);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            IconManager.instance.AddScore(score);
        }
    }
}

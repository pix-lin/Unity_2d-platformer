using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public TextMeshProUGUI UIPoint;
    public TextMeshProUGUI UIStage;
    public GameObject UIRestart;

    private void Update()
    {
        UIPoint.text = "Score: " + (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        //Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "Stage " + (stageIndex + 1).ToString();
        }
        //Game Clear
        else
        {
            //Player Control Lock
            Time.timeScale = 0;

            //Button UI Active
            UIRestart.SetActive(true);
            TextMeshProUGUI UIReStartText = UIRestart.GetComponentInChildren<TextMeshProUGUI>();
            UIReStartText.enabled = true;
        }
        
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.2f);
        }
            
        else
        {
            UIhealth[0].color = new Color(1, 0, 0, 0.2f);
            //Player Die Effect
            player.OnDie();

            //Result UI


            //Retry Button UI
            Invoke("Retry", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Health Down
            HealthDown();

            //Player Reposition
            if (health > 0)
                PlayerReposition();
        }   
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-8, 1, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Time.timeScale = 0;
        UIRestart.SetActive(true);
    }
}

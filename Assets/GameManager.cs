using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject levelFinishParent;
    public GameObject LosePanel;
    private bool levelFinish = false;

    private Destroy playerHealth;

    public bool GetLevelFinish
    {
        get
        {
            return levelFinish;
        }
    }


    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Destroy>();   
    }    
        
    

    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount <= 0)
        {
            levelFinishParent.gameObject.SetActive(true);
            LosePanel.gameObject.SetActive(false);
            levelFinish = true;
        }
        else if(playerHealth.GetHealth <= 0)
        {
            LosePanel.gameObject.SetActive(true);
            levelFinishParent.gameObject.SetActive(false);
            levelFinish = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

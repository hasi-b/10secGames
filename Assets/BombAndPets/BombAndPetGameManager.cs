using OkapiKit;
using UnityEngine;

public class BombAndPetGameManager : MonoBehaviour
{


    [SerializeField]
    Sprite winSprite;

    [SerializeField]
    Sprite loseSprite;

    [SerializeField]
    GameObject DecisionPanel;

    [SerializeField]
    Spawner spawner;

    [SerializeField]
    GameObject boom;

    [SerializeField]
    GameObject cursor;

    bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentTime <= 1 && !isPaused)
        {
            
            GameManager.instance.currentTime = 4;
            GameManager.instance.dontShowTimer = true;
            
            Win();

        }
    }

    public void Win(){
        spawner.enabled = false;
        foreach (Transform T in spawner.transform) { 
            Destroy(T.gameObject);
        }
        isPaused = true;
        GameManager.instance.isWinCondition = true;
        DecisionPanel.GetComponent<SpriteRenderer>().sprite = winSprite;
        cursor.SetActive(false);
    }

    public void Lose(){
        spawner.enabled = false;
        isPaused = true;
        GameManager.instance.isWinCondition = false;
        DecisionPanel.GetComponent<SpriteRenderer>().sprite = loseSprite;
        boom.SetActive(true);
        cursor.SetActive(false);
    }
}

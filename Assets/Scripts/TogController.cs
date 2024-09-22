using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TogController : MonoBehaviour
{
    public List <CharactersScriptables> Characters;
    public GameObject Player, Opponent, MidPoint;
    public SpriteRenderer PlayerFace, OpponentFace, BG;
    public Sprite WinBG, LoseBG;
    public Transform PlayerTransform, OpponentTransform;
    public Animator PAnimator, OAnimator;

    public AudioClip WinSound, LoseSound;
    public AudioSource audiosource, bgAudioSource;

    public Text PlayerName, OpponentName;

    bool _applyOForce = false;
    bool _applyPForce = false;
    bool tapUp = true;
    bool nullForces = false;
    bool isPaused = false;
    public float playerForce, opponentForce;
    public float increasePForce, increaseOForce;
    public int ClosePoint;
    public int PFaceInt = 0, OFaceInt = 0;

    private float tapUpTime = 0;
    private float tempPForce, tempOForce;
    private float pDistance, oDistance;

    Rigidbody2D rb;
    void Start()
    {
        
        //InstantiateCharacters(Player, Opponent);
    }

    private void OnEnable()
    {
        StartCoroutine(StartApplyingForces());
        rb = GetComponent<Rigidbody2D>();
        tempOForce = opponentForce;
        tempPForce = playerForce;
        //randomly selecting the players+opponents' avatar
        while (PFaceInt == OFaceInt)
        {
            PFaceInt = Random.Range(0, Characters.Count);
            OFaceInt = Random.Range(0, Characters.Count);
        }
        //changing faces accordingly + setting face position + names
        PlayerFace.sprite = Characters[PFaceInt].Face;
        OpponentFace.sprite = Characters[OFaceInt].Face;
        PlayerFace.transform.localPosition = Characters[PFaceInt].FacePosition;
        OpponentFace.transform.localPosition = Characters[OFaceInt].FacePosition;
        PlayerName.text = Characters[PFaceInt].Name;
        OpponentName.text = Characters[OFaceInt].Name;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
    IEnumerator StartApplyingForces()
    {
        yield return new WaitForSeconds(0.5f);
        _applyOForce = true;
        _applyPForce = true;
    }
    void Update()
    {


        if (GameManager.instance.currentTime <= 1 && !isPaused)
        {

            GameManager.instance.currentTime = 4;
            GameManager.instance.dontShowTimer = true;

            Win();

        }


        if (_applyOForce && _applyPForce) //at the end the booleans are turned off
        {
            CalculateDistance();
            PlayersPull();
            OpponentsPull();
            FindLooser();
            //IncreasePlayersForce();
        }
        else
        {
            RemoveForces();
        }
    }
    //void IncreasePlayersForce()
    //{
    //    if(oDistance < ClosePoint)
    //    {
    //        print("Close");
    //        playerForce = increasePForce;
    //    }
    //    else
    //    {
    //        playerForce = tempPForce;
    //    }
    //}
    void CalculateDistance()
    {
        pDistance = MidPoint.transform.position.x - Player.transform.position.x;
        oDistance = Opponent.transform.position.x - MidPoint.transform.position.x;
    }
    //void InstantiateCharacters(GameObject player, GameObject opponent)
    //{
    //    Player = Instantiate(player, PlayerTransform);
    //    Opponent = Instantiate(opponent, OpponentTransform);
    //}
    void FindLooser()
    {
        Debug.Log("LOOOOSERR");
        //    float pDistance = Vector2.Distance(Player.transform.localPosition, MidPoint.transform.localPosition);
        //    float oDistance = Vector2.Distance(Opponent.transform.localPosition, MidPoint.transform.localPosition);
        print(pDistance + " " + oDistance);
        if (pDistance < 0.01f)
        {
            Lose();
            RemoveForces();
        }
        else if (oDistance < 0.01f)
        {
            Win();
            RemoveForces();
        }
    }
    void PlayersPull()
    {
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            tapUp = false;
            nullForces = false;
            //opponentForce = tempOForce;
            playerForce += 0.6f;
            AddForce(-1, playerForce);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            tapUp = true;
        }
        NullifyPforce(); //check in-between-taps-delay (snappy effect)
    }
    void NullifyPforce() //as there is selay between taps the players' force nullifies and opponent's overpowers
    {                    
        if (tapUp)
        {
            if(tapUpTime < 0.5f) // if it's been less than 0.X f seconds the player hasn't tapped yet
            {                      //increase the time
                tapUpTime += Time.deltaTime;
            }
            else
            {                    // if the time reaches the threshold 0 than nullify forces
                nullForces = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = 0;
                tapUpTime = 0;
            }
            if (nullForces)
            {
                opponentForce = increaseOForce;
                
            }
        }
    }
    void OpponentsPull()
    {
        AddForce(1, opponentForce);
    }
    void AddForce(int direction, float force)
    {
        rb.AddForce(direction * transform.right * force);
    }
    void Win()
    {
        _applyOForce = false;
        _applyPForce = false;
        isPaused = true;
        GameManager.instance.isWinCondition = true;

        if (GameManager.instance.currentTime > 4)
        {
            GameManager.instance.currentTime = 4;
        }

        print("player wins");
        BG.sprite = WinBG;
        bgAudioSource.Stop();
        audiosource.PlayOneShot(WinSound);
        PAnimator.SetTrigger("Win");
        OAnimator.SetTrigger("Lose");
    }
    void Lose()
    {

        isPaused = true;
        GameManager.instance.isWinCondition = false;

        if (GameManager.instance.currentTime > 4)
        {
            GameManager.instance.currentTime = 4;
        }
        print("player loses");
        BG.sprite = LoseBG;
        bgAudioSource.Stop();
        audiosource.PlayOneShot(LoseSound);
        PAnimator.SetTrigger("Lose");
        OAnimator.SetTrigger("Win");
    }
    void RemoveForces()
    {
        _applyOForce = false;
        _applyPForce = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        PlayerName.gameObject.SetActive(false);
        OpponentName.gameObject.SetActive(false);
    }
    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

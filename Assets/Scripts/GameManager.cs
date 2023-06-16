using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;


[Serializable] 


public class BallAreaTechnicalOperations
{
    public Animator BallAreaLift;
    public TextMeshProUGUI CountText;
    public int AtilmasiGerekenTop;
    public GameObject[] Balls;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private GameObject[] PlayerPalets;
    [SerializeField] private GameObject[] BonusBalls;

    bool isTherePalets;
    [SerializeField] private GameObject BallControlObject;
    [SerializeField] public bool PlayerMotionStatu;
    [SerializeField] private List<BallAreaTechnicalOperations> _TechnicalOperations = new List<BallAreaTechnicalOperations>();

    int AtilanTopSayisi;
    int ToplamCheckPointSayisi;
    int MevcutCheckPointIndex;
    float FingerPosX;

    [Header("---UI SETTINGS")]
    public GameObject[] Panels;

    void Start()
    {
        PlayerMotionStatu = true;
        for (int i = 0; i < _TechnicalOperations.Count; i++)
        {
            _TechnicalOperations[i].CountText.text = AtilanTopSayisi + "/" + _TechnicalOperations[i].AtilmasiGerekenTop;
        }
            ToplamCheckPointSayisi = _TechnicalOperations.Count-1;    
        
    }
    
    void Update()
    {
        if (PlayerMotionStatu)
        {
            PlayerObject.transform.position += 5f * Time.deltaTime * PlayerObject.transform.forward;


            if(Time.timeScale!=0)
            {

                if(Input.touchCount>0)
                {
                    Touch touch = Input.GetTouch(0);

                    Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                    switch(touch.phase)
                    {
                        case TouchPhase.Began:
                            FingerPosX = TouchPosition.x - PlayerObject.transform.position.x;
                            break;

                        case TouchPhase.Moved:
                            if (TouchPosition.x - FingerPosX > -1.15 && TouchPosition.x - FingerPosX < 1.15)
                            {
                                PlayerObject.transform.position = Vector3.Lerp(PlayerObject.transform.position, new Vector3(TouchPosition.x - FingerPosX, PlayerObject.transform.position.y, PlayerObject.transform.position.z), 3f);
                            }
                            break;              
                    }
                }          
            }
        }      
    }

    public void ReachedBorder()
    {
        if (isTherePalets)
        {
            PlayerPalets[0].SetActive(false);
            PlayerPalets[1].SetActive(false);
        }
        PlayerMotionStatu = false;
        Invoke("PhaseControl", 2f);
        Collider[] HitColl = Physics.OverlapBox(BallControlObject.transform.position, BallControlObject.transform.localScale / 2,Quaternion.identity);

        int i = 0;
        while (i < HitColl.Length)
        {
                HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, .8f), ForceMode.Impulse);
                i++;
        }     
    }

    public void ToplariSay() 
    {
        AtilanTopSayisi++;
        _TechnicalOperations[MevcutCheckPointIndex].CountText.text = AtilanTopSayisi + "/" + _TechnicalOperations[MevcutCheckPointIndex].AtilmasiGerekenTop;
    }

    void PhaseControl()
    {
        if(AtilanTopSayisi >= _TechnicalOperations[MevcutCheckPointIndex].AtilmasiGerekenTop)
        {
            _TechnicalOperations[MevcutCheckPointIndex].BallAreaLift.Play("Asansor");
            foreach (var item in _TechnicalOperations[MevcutCheckPointIndex].Balls)
            {
                item.SetActive(false);
            }
            if (MevcutCheckPointIndex == ToplamCheckPointSayisi)
            {
                Panels[1].SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                MevcutCheckPointIndex++;
                AtilanTopSayisi = 0;
                if(isTherePalets)
                {
                    PlayerPalets[0].SetActive(true);
                    PlayerPalets[1].SetActive(true);
                }
            }
        }
        else
        {
            Panels[2].SetActive(true);
        }
    }

    public void EmergePalets()
    {
        isTherePalets = true;
    }

    public void AddBonusBall(int BonusBallIndex)
    {
        BonusBalls[BonusBallIndex].SetActive(true);
    }

    public void Pause()
    {
        Panels[0].SetActive(true);
        Time.timeScale = 0;
    }

    public void PausePanelsButton(string islem)
    {
        switch(islem) 
        {
            case "resume":
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;
            case "exit":
                Application.Quit();
                break;
            case "settings":
                break;
            case "again":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

}

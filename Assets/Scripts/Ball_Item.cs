using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Item : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private string itemType;
    [SerializeField] private int BonusBallIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBorderObject"))
        {

            if (itemType=="Palet")
            {
                _GameManager.EmergePalets();
                gameObject.SetActive(false);
            }
            else
            {
                _GameManager.AddBonusBall(BonusBallIndex);
                gameObject.SetActive(false);
            }
         
        }
    }
}

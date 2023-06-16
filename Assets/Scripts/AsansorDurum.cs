using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsansorDurum : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private Animator BarrierArea;
   public void BariyerKaldir()
    {
        BarrierArea.Play("BariyerKaldir");
    }

    public void End()
    {
        _GameManager.PlayerMotionStatu = true;
      
    }
}

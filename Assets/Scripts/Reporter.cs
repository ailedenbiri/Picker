using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reporter : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBorderObject"))
        {
            _GameManager.ReachedBorder();
        }
    }
}

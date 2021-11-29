using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private int timeScale;
    private void Update()
    {
        Time.timeScale = timeScale;
    }
}

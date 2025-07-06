using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameObject[] checkpoints;
    
    private float timePassed;

    private const int DEFAULTVALUE = 10;

    public int scoreDebug;

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    public void UpdateCheckpoint(Carro carro)
    {
        carro.checkpointControl++;

        if (carro.checkpointControl == checkpoints.Length)
        {
            carro.checkpointControl = 0;
            carro.lagent.IncreaseScore(1000);
        }
    }

    public GameObject GetActualCheckpoint(Carro carro)
    {
        return checkpoints[carro.checkpointControl];
    }

    public int GetBonusByTimePassed()
    {
        int bonusScore = DEFAULTVALUE / Mathf.Clamp((int)timePassed, 1 , DEFAULTVALUE);
        //Debug.Log(bonusScore);
        timePassed = 0;
        return bonusScore;
    }

    public void IncreaseScore(int score, Carro carro)
    {
        scoreDebug += score;
        carro.lagent.IncreaseScore(score);
    }

    public void Reset(Carro carro)
    {
        scoreDebug = 0;
        carro.checkpointControl = 0;
        timePassed = 0;
    }
}

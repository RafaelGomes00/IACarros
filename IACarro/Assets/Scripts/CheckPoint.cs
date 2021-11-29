using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private CheckPointController checkpointControl;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Carro"))
        {
            Carro carro = other.GetComponent<Carro>();
            if (carro.willIncreaseOnExit)
            {
                checkpointControl.IncreaseScore(20 + checkpointControl.GetBonusByTimePassed(), carro);
                carro.willIncreaseOnExit = false;
            }
            else
                checkpointControl.IncreaseScore(-50, carro);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Carro"))
        {
            Carro carro = other.GetComponent<Carro>();
            if (checkpointControl.GetActualCheckpoint(carro) == this.gameObject)
            {
                checkpointControl.IncreaseScore(5, carro);
                checkpointControl.UpdateCheckpoint(carro);
                carro.willIncreaseOnExit = true;
            }
            else
                checkpointControl.IncreaseScore(-50, carro);
        }
    }
}

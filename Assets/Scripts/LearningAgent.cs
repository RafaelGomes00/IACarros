using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class LearningAgent : Unity.MLAgents.Agent
{
    [SerializeField] private Carro carroInput;
    [SerializeField] private CheckPointController cpControl;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public event System.Action OnReset;
    public float score;

    public override void Initialize()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        //if (GameController.Instance.IsTraining)
            RequestDecision();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (actionBuffers.DiscreteActions.Array[0] == 0) // 1 - Frente
        {
            carroInput.Accelerate();
        }
        else if (actionBuffers.DiscreteActions.Array[0] == 1) // 2 - Tras
        {
            carroInput.Deaccelerate();
        }
        if (actionBuffers.DiscreteActions.Array[1] == 0) // 1 - Esquerda
        {
            carroInput.TurnLeft();
        }
        else if (actionBuffers.DiscreteActions.Array[1] == 1) // 2 - Direita
        {
            carroInput.TurnRight();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if (Input.GetKey(KeyCode.UpArrow)) // 1 - Frente
            actionsOut.DiscreteActions.Array[0] = 0;
        else if (Input.GetKey(KeyCode.DownArrow)) // 2 - Tras
            actionsOut.DiscreteActions.Array[0] = 1;

        if (Input.GetKey(KeyCode.LeftArrow)) // 1 - Esquerda
            actionsOut.DiscreteActions.Array[1] = 0;
        else if (Input.GetKey(KeyCode.RightArrow)) // 2 - Direita
            actionsOut.DiscreteActions.Array[1] = 1;
    }

    public void End()
    {
        EndEpisode();
    }

    public override void OnEpisodeBegin()
    {
        Reset();
    }

    private void Reset()
    {
        score = 0;
        transform.position = startingPosition;
        transform.rotation = startingRotation;

        carroInput.checkPointController.Reset(GetComponent<Carro>());

        if (OnReset != null)
            OnReset();
    }

    public void IncreaseScore(float score)
    {
        this.score += score;
        AddReward(score);
    }

}

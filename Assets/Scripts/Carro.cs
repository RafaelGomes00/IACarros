using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float turnSmoothVelocity = 5;

    private Rigidbody rb;
    Vector3 dir;

    public CheckPointController checkPointController;
    public LearningAgent lagent;
    public int checkpointControl = 0;
    public bool willIncreaseOnExit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("CheckPointDistancePointer", 0, 2f);
    }

    void FixedUpdate()
    {
        dir.Normalize();
        //transform.position += dir * speed * Time.fixedDeltaTime;
        rb.AddForce(dir * speed * Time.deltaTime, ForceMode.Force);
        //Debug.Log(dir);
        //transform.Translate(dir * speed * Time.fixedDeltaTime);
        dir = Vector3.zero;
    }

    private void CheckPointDistancePointer()
    {
        lagent.IncreaseScore(-Vector3.Distance(this.transform.position, checkPointController.GetActualCheckpoint(this).transform.position)/10);
    }

    public void Accelerate()
    {
        dir += transform.forward;
    }
    public void Deaccelerate()
    {
        dir -= transform.forward;
    }

    public void TurnLeft()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * turnSmoothVelocity);
        //dir -= transform.right;
    }
    public void TurnRight()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * -1 * turnSmoothVelocity);
        //dir += transform.right;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Parede")
        {
            lagent.IncreaseScore(-1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Parede")
        {
            lagent.IncreaseScore(-1);
        }
    }

}
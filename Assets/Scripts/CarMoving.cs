using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class CarMoving : MonoBehaviour
{
    private float timer = 0;
    private Rigidbody carRb;
    private List<float> replay = new List<float>();
    private Vector3 startPos;
    private quaternion startRot;
    private bool isRecording = false;
    private float enginePower = 150.0f;
    private float wheelPower = 0.35f;
    public float refreshmentRate;
    public GameObject enemyCar1;
    public GameObject enemyCar2;
    public GameObject enemyCar3;
    public GameObject enemyCar4;
    public GameObject enemyCar5;
    public GameObject enemyCar6;
    private EnemyCarScript enemyScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Record();
        MoveCar();
        CheckRecording();
    }

    void Record()
    {
        if (isRecording)
        {
            timer += Time.deltaTime;
            if (timer > refreshmentRate)
            {
                replay.Add(timer);
                timer = 0;
                replay.Add(transform.position.x);
                replay.Add(transform.position.z);
                replay.Add(transform.rotation.eulerAngles.y);
            }
        }
    }

    void CheckRecording()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRecording)
            {
                isRecording = true;
                timer = 0;
                replay = new List<float>();
                startPos = transform.position;
                startRot = transform.rotation;
            }
            else
            {
                isRecording = false;
                translateInfo();
            }
        }

    }
    void MoveCar()
    {
        transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up * wheelPower);
        carRb.AddForce(Input.GetAxis("Vertical") * transform.forward * enginePower);
    }

    void translateInfo()
    {
        enemyScript = enemyCar1.GetComponent<EnemyCarScript>();
        enemyScript.replay = replay;
        enemyScript.giveValues(enginePower, wheelPower, startPos, startRot);

        enemyScript = enemyCar2.GetComponent<EnemyCarScript>();
        enemyScript.replay = replay;
        enemyScript.giveValues(enginePower, wheelPower, startPos, startRot);

        enemyScript = enemyCar3.GetComponent<EnemyCarScript>();
        enemyScript.replay = replay;
        enemyScript.giveValues(enginePower, wheelPower, startPos, startRot);
    }
}

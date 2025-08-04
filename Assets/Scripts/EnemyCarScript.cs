using JetBrains.Annotations;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyCarScript : MonoBehaviour
{
    public quaternion startRot;
    public Vector3 startPos;
    public List<float> replay = new List<float>();
    private float enginePower;
    private float wheelPower;

    private Vector3 pushVector;
    private Rigidbody carRb;

    public enum MindModel {
        None,
        Force,
        Acceleration,
        Impulse,
        VelocityChange,
        TransformIf,
        TransformWhile,
        Translate,
        Lerp,
        MoveTowards
    }

    public MindModel carMM;
    //public string carMindModel = new List<string>() { "Force", "Acceleration", "Impulse", "VelocityChange", "TransformIf", "TransformWhile", "Translate", "Lerp", "MoveTowards" }[0];

    private bool isPlaying = false;
    private float timer = 0;
    private int currentIndex = 0;
    void Start()
    {
        carRb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        startReplay();
        Play();
    }

    void Play()
    {
        if (isPlaying && !(currentIndex >= replay.Count))
        {
            timer += Time.deltaTime;
            if (timer >= replay[currentIndex])
            {
                switch (carMM)
                {
                    case MindModel.None:
                        break;
                    case MindModel.Force:
                        pushVector = new Vector3(replay[currentIndex + 1] - transform.position.x, 0, replay[currentIndex + 2] - transform.position.z);
                        carRb.AddForce(pushVector * enginePower, ForceMode.Force);
                        break;
                    case MindModel.Acceleration:
                        pushVector = new Vector3(replay[currentIndex + 1] - transform.position.x, 0, replay[currentIndex + 2] - transform.position.z);
                        carRb.AddForce(pushVector, ForceMode.Acceleration);
                        break;
                    case MindModel.Impulse:
                        pushVector = new Vector3(replay[currentIndex + 1] - transform.position.x, 0, replay[currentIndex + 2] - transform.position.z);
                        carRb.AddForce(pushVector, ForceMode.Impulse);
                        break;
                    case MindModel.VelocityChange:
                        pushVector = new Vector3(replay[currentIndex + 1] - transform.position.x, 0, replay[currentIndex + 2] - transform.position.z);
                        carRb.AddForce(pushVector, ForceMode.VelocityChange);
                        break;
                    case MindModel.TransformIf:
                        transform.position = new Vector3(replay[currentIndex + 1], transform.position.y, replay[currentIndex + 2]);
                        break;
                    case MindModel.TransformWhile:
                        while (timer >= replay[currentIndex])
                        {
                            transform.position = new Vector3(replay[currentIndex + 1], transform.position.y, replay[currentIndex + 2]);
                        }
                        break;
                    case MindModel.Translate:
                        pushVector = new Vector3(transform.position.x - replay[currentIndex + 1], 0, transform.position.z - replay[currentIndex + 2]);
                        transform.Translate(pushVector * replay[currentIndex]);
                        break;
                    case MindModel.Lerp:
                        transform.position = Vector3.Lerp(transform.position, new Vector3(replay[currentIndex + 1], transform.position.y, replay[currentIndex + 2]), replay[currentIndex]);
                        break;
                    case MindModel.MoveTowards:
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(replay[currentIndex + 1], transform.position.y, replay[currentIndex + 2]), 1 / replay[currentIndex]);
                        break;
                    default:
                        return;
                }
                transform.rotation = Quaternion.Euler(0, replay[currentIndex + 3], 0);
                currentIndex += 4;
                timer = 0;
            }
        }
    }

    public void giveValues(float eP, float wP, Vector3 sP, quaternion sR)
    {
        enginePower = eP;
        wheelPower = wP;
        startPos = sP;
        startRot = sR;
    }

    void startReplay()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPlaying)
            {

                isPlaying = false;
            }
            else
            {
                currentIndex = 0;
                timer = 0;
                transform.position = startPos;
                transform.rotation = startRot;
                isPlaying = true;
            }
        }
    }
}

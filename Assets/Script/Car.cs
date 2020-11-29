using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle;
    public float maxMotorTorque;
    public float maxBrakeTorque;
    public float currentSpeed;
    public float maxSpeed = 1500f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public Vector3 centerOfMass;
    public int score = 0;
    public ParticleSystem Smoke;

    private bool isBraking = false;
    private List<Transform> nodes;
    private int currentNode = 0,previousNode;
    // Start is called before the first frame update
    void Start()
    {
        Smoke.Stop();
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = relativeVector.x / relativeVector.magnitude * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if(currentSpeed<maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void CheckWaypointDistance()
    {
        if (nodes.Count > 1&&currentNode==0)
        {
            previousNode = nodes.Count-1;
        }
        else
        {
            previousNode = currentNode - 1;
        }
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < Vector3.Distance(nodes[previousNode].position, nodes[currentNode].position)/2)
        {
            isBraking = true;
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            if(currentSpeed>=10)
            {
                wheelRL.brakeTorque = maxBrakeTorque;
                wheelRR.brakeTorque = maxBrakeTorque;
            }
            else
            {
                wheelFL.motorTorque = maxMotorTorque;
                wheelFR.motorTorque = maxMotorTorque;
                wheelRL.brakeTorque = 0;
                wheelRR.brakeTorque = 0;
            }
            if ((Vector3.Distance(transform.position, nodes[currentNode].position) < 5f))
            {
                if (currentNode == nodes.Count - 1) currentNode = 0;
                else currentNode++;
            }
        }
        else
        {
            isBraking = false;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}

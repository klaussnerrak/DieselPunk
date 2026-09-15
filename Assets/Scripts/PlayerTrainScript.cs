using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.UI;
using NavMeshPlus.Components;

public class PlayerTrainScript : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private GameObject mapEnd; 
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float spriteAngleOffset = -90f;

    private int pivotIndex = 0;
    bool playerStart = false;

    enum StateMachineType
    {
        Waiting,
        Moving,
        Finish
    }

    private StateMachineType state = StateMachineType.Waiting;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Impede que o sistema 3D gire o sprite de forma errada no plano 2D
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void DefinirDestino()
    {
        agent.SetDestination(mapEnd.transform.position);
    }


    private void RotateTrain()
    {
        Vector3 targetPoint = agent.steeringTarget;
        Vector3 direction = targetPoint - transform.position;


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + spriteAngleOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }


}

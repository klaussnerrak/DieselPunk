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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Impede que o sistema 3D gire o sprite de forma errada no plano 2D
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void MoveTrain()
    { agent.Warp(mapEnd.transform.position);
        Debug.Log("isActiveAndEnabled: "+isActiveAndEnabled);
        Debug.Log("agent.isOnNavMesh: "+agent.isOnNavMesh);
        if (agent.isActiveAndEnabled && agent.isOnNavMesh) { agent.Warp(mapEnd.transform.position); }
        //agent.SetDestination(mapEnd.transform.position);
    }



}

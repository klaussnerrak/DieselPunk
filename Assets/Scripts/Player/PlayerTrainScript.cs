using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerTrainScript : MonoBehaviour
{
    [SerializeField] private GameObject mapEnd;
    public TrainTrack firstTrack;
    private NavMeshAgent agent;
    

    public List<Transform> pivotPoints = new List<Transform>();
    [SerializeField] private Button startButton;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float spriteAngleOffset = -90f;
    [SerializeField] private float speed = 0.1f;

    private int pivotIndex = 0;
    private int trackIndex = 0;
    bool playerStart = false;

    public GridManager gridManager;

    

    enum StateMachineType
    {
        Waiting,
        Moving,
        Finish
    }

    private StateMachineType state = StateMachineType.Waiting;

    void Awake()
    {
        //startButton.onClick.AddListener(() => playerStart = true);

    }


    public void StartPath()
    {
        Debug.Log("CLICKED");
        //pivotPoints.Add(mapEnd.transform);

       // Debug.Log(pivotPoints);
        // agent.SetDestination(pivotPoints[pivotIndex].position);
        playerStart = true;
        //Moving();
    }

    void Update()
    {
        // ReadPivotPoints();
        // agent.SetDestination(pivotPoints[pivotIndex].position);
    }

    void Start()
    {
        GameObject gridManagerObject = GameObject.Find("Grid");

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        mapEnd = GameObject.Find("MapEnd(Clone)");
        if (firstTrack && mapEnd)
        {
            Debug.Log("Position first track: " + firstTrack.transform.position);
            Debug.Log("Position last track: " + mapEnd.transform.position);
            pivotPoints.Add(firstTrack.transform); 
        }

        if(gridManagerObject != null)
        {
            gridManager = gridManagerObject.GetComponent<GridManager>();
            gridManager.AddToTrack(firstTrack);
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            firstTrack.transform.position = transform.position;
            Instantiate(firstTrack, transform.position, rotation); 
            Debug.Log(firstTrack.transform.position);

        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == StateMachineType.Waiting) Waiting();
        else if (state == StateMachineType.Moving) Moving();
        else if (state == StateMachineType.Finish) Finish();

    }


    private void Waiting()
    {
        // if(playerStart == true || TimerScript.instance.timeCounter<=1)
        if (playerStart == true)
        {
            state = StateMachineType.Moving;
            playerStart = false;
            // TimerScript.instance.StartPlayCounter();   
        }

    }

    private void Moving()
    {
        Debug.Log("moving " + Vector3.Distance(transform.position, gridManager.Track[trackIndex].transform.position));
        // if(TimerScript.instance.timeCounter>1)
        // {
        /*if (Vector2.Distance(transform.position, pivotPoints[pivotIndex].position) < 0.1f)
        {
            pivotIndex += 1;
            if (pivotIndex < pivotPoints.Count)
            {
                agent.SetDestination(pivotPoints[pivotIndex].position);
            }
            else if (pivotIndex == pivotPoints.Count)
            {
                state = StateMachineType.Finish;
                TimerScript.instance.pauseTimer = true;
            }
        }*/
        if (trackIndex < gridManager.Track.Count)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                gridManager.Track[trackIndex].transform.position,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(
                transform.position,
                gridManager.Track[trackIndex].transform.position) < 0.1f)
            {
                trackIndex++;
            }
        }

        //RotateTrain();
        // }
        // else 
        // {
        //     state = StateMachineType.Finish;
        //     TimerScript.instance.pauseTimer = true;
        // }

    }

    private void Finish()
    {
        //    if(TimerScript.instance.timeCounter>1)
        //    {
        //         GameController.instance.WinCondition();
        state = StateMachineType.Waiting;
        //    }
        //    else 
        //    {
        //         GameController.instance.LoseCondition();
        //         state = StateMachineType.Waiting;

        //    } 
    }

    void ReadPivotPoints()
    {
        Debug.Log("pivotPoints size: " + pivotPoints.Count);
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

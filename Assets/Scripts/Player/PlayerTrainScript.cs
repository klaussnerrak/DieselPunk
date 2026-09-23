using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerTrainScript : MonoBehaviour
{
    public TrainTrack mapEnd;
    public TrainTrack firstTrack;
    private NavMeshAgent agent;
    

    public List<Transform> pivotPoints = new List<Transform>();
    [SerializeField] private Button startButton;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float spriteAngleOffset = 0f;
    [SerializeField] private float speed = 1f;

   // private int pivotIndex = 0;
    private int trackIndex = 0;
//private Vector3 lastDirection;
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

       /* agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        mapEnd = GameObject.Find("MapEnd(Clone)");
        if (firstTrack && mapEnd)
        {
            Debug.Log("Position first track: " + firstTrack.transform.position);
            Debug.Log("Position last track: " + mapEnd.transform.position);
            pivotPoints.Add(firstTrack.transform); 
        }*/

        if(gridManagerObject != null)
        {
            gridManager = gridManagerObject.GetComponent<GridManager>();
            gridManager.AddToTrack(firstTrack);
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            firstTrack.transform.position = transform.position;
            Instantiate(firstTrack, transform.position, rotation); 
            //Debug.Log(firstTrack.transform.position);

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
            gridManager.AddToTrack(mapEnd);
            // TimerScript.instance.StartPlayCounter();   
        }

    }

    private void Moving()
    {
        //Debug.Log("moving ");        
        
        if (trackIndex >= gridManager.Track.Count){
            state = StateMachineType.Finish;
            TimerScript.instance.pauseTimer = true;
            return;
        }

        

        Transform target = gridManager.Track[trackIndex].transform;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );
        float distance = Vector3.Distance(transform.position, target.position);
        //Debug.Log(distance);
        RotateTrain();

        if (distance < 0.1f)
        {
            //transform.position = target.position;
            trackIndex++;
        }
   
        
        //
        // }
        // else 
        // {
        //     state = StateMachineType.Finish;
        //     TimerScript.instance.pauseTimer = true;
        // }

    }

    private void Finish()
    {
            if(TimerScript.instance.timeCounter>1)
            {
                 GameController.instance.WinCondition();
                state = StateMachineType.Waiting;
            }
            else 
            {
                 GameController.instance.LoseCondition();
                 state = StateMachineType.Waiting;

            } 
    }

    /*void ReadPivotPoints()
    {
        Debug.Log("pivotPoints size: " + pivotPoints.Count);
    }*/

    private void RotateTrain()
    {
        if (trackIndex >= gridManager.Track.Count)
            return;
       
        Vector3 currentPoint = transform.position;
        Vector3 nextPoint = gridManager.Track[trackIndex].transform.position;        
        Vector3 direction = nextPoint - currentPoint;
        //direction = direction.normalized;

        if (direction.sqrMagnitude < 0.1f)
            return;
         
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + spriteAngleOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
            rotationSpeed * Time.deltaTime);

        //Debug.Log(angle);    
    }

    
}

using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.UI;

public class TrainScript : MonoBehaviour
{
    private Vector2 startPosition;    
    private NavMeshAgent agent;      
    

    [SerializeField] private List<Transform> pivotPoints = new List<Transform>();
    [SerializeField] private Button startButton;
    [SerializeField] private Transform mapEnd;
    [SerializeField] private PathValidator pathValidator;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float spriteAngleOffset = -90f;

    private int pivotIndex = 0;
    private readonly List<Vector3> pathPositions = new List<Vector3>();
    bool playerStart = false;

    enum StateMachineType
    {        
        Waiting,
        Moving,
        Finish
    }

    private StateMachineType state = StateMachineType.Waiting;  

    void Awake()
    {
        if (startButton != null)
            startButton.onClick.AddListener(() => playerStart = true);

    }  
    
    void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        if (pivotPoints.Count == 0 && mapEnd != null)
            pivotPoints.Add(mapEnd);

        if (agent != null && pivotPoints.Count > 0)
            agent.SetDestination(pivotPoints[pivotIndex].position);
    }

    public void Configure(PathValidator configuredPathValidator, Transform configuredMapEnd)
    {
        pathValidator = configuredPathValidator;
        mapEnd = configuredMapEnd;
        pivotPoints.Clear();
        pathPositions.Clear();
        pivotIndex = 0;
        state = StateMachineType.Waiting;
    }

    public void StartMovement()
    {
        playerStart = true;
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
        if (playerStart && CanStart())
        {
            if (!PreparePath())
            {
                playerStart = false;
                return;
            }

            state = StateMachineType.Moving;
            playerStart = false;
            if (TimerScript.instance != null)
                TimerScript.instance.StartPlayCounter();
            
            
        }

    }

    private bool PreparePath()
    {
        pathPositions.Clear();

        if (pathValidator != null)
        {
            var cells = new List<Vector3Int>();
            if (!pathValidator.TryGetPath(cells) || pathValidator.Board == null ||
                pathValidator.Board.Grid == null)
                return false;

            foreach (Vector3Int cell in cells)
                pathPositions.Add(pathValidator.Board.Grid.GetCellCenterWorld(cell));
        }
        else if (mapEnd != null)
        {
            pathPositions.Add(transform.position);
            pathPositions.Add(mapEnd.position);
        }

        pivotIndex = pathPositions.Count > 1 ? 1 : 0;
        return pathPositions.Count > 0;
    }

    private bool CanStart()
    {
        return pathValidator == null || pathValidator.HasValidPath();
    }
    
    private void Moving()
    {
        if (pathPositions.Count == 0 && pivotPoints.Count == 0)
        {
            state = StateMachineType.Finish;
            return;
        }

        Vector3 target = pathPositions.Count > 0
            ? pathPositions[pivotIndex]
            : pivotPoints[pivotIndex].position;

        if (TimerScript.instance == null || TimerScript.instance.timeCounter > 1)
        {
            if (Vector2.Distance(transform.position, 
            target) < 0.1f )
            {            
                pivotIndex += 1;
                int pathCount = pathPositions.Count > 0 ? pathPositions.Count : pivotPoints.Count;
                if (pivotIndex < pathCount)
                {      
                    if (agent != null && pathPositions.Count == 0)
                        agent.SetDestination(pivotPoints[pivotIndex].position);
                }
                else if (pivotIndex == pathCount)
                {                    
                    state = StateMachineType.Finish;
                    if (TimerScript.instance != null)
                        TimerScript.instance.pauseTimer = true;
                    return;
                }                      
            } 
            if (agent == null || pathPositions.Count > 0)
                MoveWithoutNavMesh();
            else
                RotateTrain();
        }
        else 
        {
            state = StateMachineType.Finish;
            if (TimerScript.instance != null)
                TimerScript.instance.pauseTimer = true;
        }
        
    }

    private void Finish()
    {
       if (GameController.instance == null)
           return;

       if (TimerScript.instance == null || TimerScript.instance.timeCounter > 1)
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

    private void RotateTrain()
    {
        Vector3 targetPoint = agent.steeringTarget;        
        Vector3 direction = targetPoint - transform.position;
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f,0f,angle + spriteAngleOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void MoveWithoutNavMesh()
    {
        Vector3 target = pathPositions.Count > 0
            ? pathPositions[pivotIndex]
            : pivotPoints[pivotIndex].position;
        Vector3 direction = target - transform.position;
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            rotationSpeed * Time.fixedDeltaTime);

        if (direction.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + spriteAngleOffset);
        }
    }
    
    
}

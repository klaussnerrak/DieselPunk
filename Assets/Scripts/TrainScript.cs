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

    void Awake()
    {
        startButton.onClick.AddListener(() => playerStart=true);

    }  
    
    void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.SetDestination(pivotPoints[pivotIndex].position);
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
        if(playerStart == true || TimerScript.instance.timeCounter<=1)
        {
            state = StateMachineType.Moving;
            playerStart = false;
            TimerScript.instance.StartPlayCounter();
            
            
        }
        
    }
    
    private void Moving()
    {
        if(TimerScript.instance.timeCounter>1)
        {
            if (Vector2.Distance(transform.position, 
            pivotPoints[pivotIndex].position) < 0.1f )
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
            } 
        RotateTrain();       
        }
        else 
        {
            state = StateMachineType.Finish;
            TimerScript.instance.pauseTimer = true;
        }
        
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

    private void RotateTrain()
    {
        Vector3 targetPoint = agent.steeringTarget;        
        Vector3 direction = targetPoint - transform.position;
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f,0f,angle + spriteAngleOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    
    
}

using UnityEngine;

public class TrainWagonScript : MonoBehaviour
{ 
    [SerializeField] private float followSpeed = 1f;
    [SerializeField] private float followDistance = 10f; 
    [SerializeField] private Transform train;
     [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float spriteAngleOffset = 0f;
    public int wagonIndex;

    
    private int trackIndex = 0;


    private bool isFollowing = true;

   
    void LateUpdate()
    {
        if (!isFollowing || train == null)
            return;

        trackIndex = PlayerTrainScript.instance.trackIndex - wagonIndex;
        followSpeed = PlayerTrainScript.instance.speed;

        if (trackIndex == GridManager.instance.Track.Count-1)
        {
            trackIndex--;
        }
        
       //Vector3 trainDirection = train.right;        
       //Vector3 targetPosition = train.position - 1;// - trainDirection * followDistance;

        Vector3 targetPosition = GridManager.instance.Track[trackIndex].transform.position;
 
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        RotateTrain();
       // transform.rotation = train.rotation
 
        if (PlayerTrainScript.instance != null && PlayerTrainScript.instance.IsAtMapEnd())
        {
            isFollowing = false;
            Destroy(gameObject);
        }
    } 

    public void SetTrain(Transform trainTransform)
    {
        train = trainTransform;
    }

    void OnDestroy()
    {
        isFollowing = false;
        train = null;
    }

    private void RotateTrain()
    {      

        Vector3 currentPoint = transform.position;
        Vector3 nextPoint = GridManager.instance.Track[trackIndex].transform.position;

        Vector3 direction = nextPoint - currentPoint;
        //direction = direction.normalized;

        if (direction.sqrMagnitude < 0.1f)
            return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + spriteAngleOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
            rotationSpeed * Time.deltaTime);

    }
}

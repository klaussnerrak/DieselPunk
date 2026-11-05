using UnityEngine;

public class TrainWagonScript : MonoBehaviour
{ 
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float followDistance = 1f; 
    [SerializeField] private Transform train;

    private bool isFollowing = true;

   
    void LateUpdate()
    {
        if (!isFollowing || train == null)
            return;
 
        Vector3 trainDirection = train.right; 
        Vector3 targetPosition = train.position - trainDirection * followDistance;
 
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
 
        transform.rotation = train.rotation;
 
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
}

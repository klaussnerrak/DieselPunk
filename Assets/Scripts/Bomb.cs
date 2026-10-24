using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public TrainTrack targetPosition;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float speed = 1f;

    void Update()
    {
        if(Vector3.Distance(targetPosition.transform.position, transform.position)<0.5)
        {
            ParticleSystem explosion = Instantiate(particles, transform.position, Quaternion.identity);
            UnityEngine.Debug.Log("Hit");

            Destroy(targetPosition.gameObject);
            Destroy(gameObject);
            Destroy(explosion, 2f);
            //ParticleSystem explosion = Instantiate(particles, transform.position, Quaternion.identity);
        }
    }   
}

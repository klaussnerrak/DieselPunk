using UnityEngine;
using System.Collections;

public class AirStrikeSpawner : MonoBehaviour
{        
    public GameObject bombPrefab; 
    public Transform target;

    public float spawnInterval = 1.5f; 
    public float minX = -5.5f; // Limite esquerdo no ar
    public float maxX = 2.5f;  // Limite direito no ar
    public float spawnY = 7f; // Altura de onde elas caem

    public static AirStrikeSpawner instance;
    public int trackIndex = 0;
    public float customGravityScale = 0.03f;
    public float flightTime = 3f;

    public bool isRunning = false;
    private Coroutine airstrikeCoroutine;
    

   void Awake()
    {
        instance  = this;
    }
    
    
    public void StartAirstrike()    
    {
        if (!isRunning)
        {
            isRunning = true;
            airstrikeCoroutine = StartCoroutine(SpawnTargetedBombs());
        }
    }

    public void StopAirstrike()    
    {
        
            isRunning = false;
            StopAllCoroutines();
        
    }

    IEnumerator SpawnTargetedBombs()
    {
        while (isRunning)
        {
            
            if(trackIndex < GridManager.instance.Track.Count)
            {
                TrainTrack track = GridManager.instance.Track[trackIndex];
                
                if (track != null && track.transform.position.x <= maxX && track.transform.position.x >= minX)            {
                    
                    float targetX = track.transform.position.x;
                    Vector3 spawnPosition = new Vector3(targetX+3, spawnY, 0f);

                    if (isRunning)
                    {
                        GameObject newBomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

                        Bomb bomb = newBomb.GetComponent<Bomb>();
                        bomb.targetPosition = track;                    
                        
                        Rigidbody2D rb = newBomb.GetComponent<Rigidbody2D>();           
                        rb.gravityScale = customGravityScale;

                        Vector2 initialVelocity = CalculateProjectileVelocity(spawnPosition, track.transform.position, flightTime, rb.gravityScale);
                        rb.linearVelocity = initialVelocity;
                    }

                }
                
                trackIndex++;
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return false;
            }
        }        
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time, float gravityScale)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float gravity = Physics2D.gravity.y * gravityScale;        
        float velocityY = (distance.y - (0.5f * gravity * time * time)) / time;
        return new Vector2(velocityX, velocityY);
    }
}
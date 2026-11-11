//using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public TrainTrack target;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Sprite crackSprite;
    [SerializeField] private Sprite curveCrackSprite;
    //public Animator animator;

    private Rigidbody2D rb;

    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(target.transform.position, transform.position)<0.1)
        {
            //ParticleSystem explosion = Instantiate(particles, transform.position, Quaternion.identity);
            
            
            /*rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            transform.localScale = Vector3.one;*/
            //animator.SetBool("Explode", true);
            AudioManager.instance.PlaySFX("Explosion");
            target.animator.SetBool("Explode", true);

            /*if(target.type != TrackType.Curve)
            {
                
                target.GetComponent<SpriteRenderer>().sprite = crackSprite; 
                float zAngle = target.transform.eulerAngles.z;
                if (zAngle != 0)
                {
                    target.transform.rotation = Quaternion.Euler(0f,0f,(90+zAngle));
                }
                else
                {
                    target.transform.rotation = Quaternion.Euler(0f,0f,90f);
                }
                //target.animator.SetBool("Explode", false);
                
            }
            else
            {
                target.GetComponent<SpriteRenderer>().sprite = curveCrackSprite; 
                float zAngle = target.transform.eulerAngles.z;
                if (zAngle != 0)
                {
                    target.transform.rotation = Quaternion.Euler(0f,0f,(180+zAngle));
                }
                else
                {
                    target.transform.rotation = Quaternion.Euler(0f,0f,180f);
                }
            }*/
            
            if(Vector3.Distance(target.transform.position, PlayerTrainScript.instance.transform.position)<0.1)
            {
                AirStrikeSpawner.instance.StopAirstrike();
                GameController.instance.LoseCondition();            
                Debug.Log("Loser");
            }
            
            
            Destroy(gameObject);
            
            //Destroy(explosion, 2f);
            //ParticleSystem explosion = Instantiate(particles, transform.position, Quaternion.identity);
        }

        

        
    }   
}

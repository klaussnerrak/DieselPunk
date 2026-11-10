using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public TrainTrack target;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Sprite crackSprite;
    [SerializeField] private Sprite curveCrackSprite;

    void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position)<0.1)
        {
            ParticleSystem explosion = Instantiate(particles, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX("Explosion");            

            if(target.type != TrackType.Curve)
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
            }
            if(Vector3.Distance(target.transform.position, PlayerTrainScript.instance.transform.position)<0.1)
            {
                AirStrikeSpawner.instance.StopAirstrike();
                GameController.instance.LoseCondition();            
                Debug.Log("Loser");
            }
            
            
            Destroy(gameObject);
            Destroy(explosion, 2f);
            //ParticleSystem explosion = Instantiate(particles, transform.position, Quaternion.identity);
        }

        

        
    }   
}

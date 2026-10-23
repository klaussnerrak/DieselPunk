using UnityEngine;
using System.Collections;

public class AirStrikeSpawner : MonoBehaviour
{        
    public GameObject bombPrefab; 
    public Transform target;

    public float spawnInterval = 1.5f; 
    public float minX = -8f; // Limite esquerdo no ar
    public float maxX = 8f;  // Limite direito no ar
    public float spawnY = 10f; // Altura de onde elas caem

   
    void Start()
    {
        // Se você esqueceu de arrastar o target no Inspector, tenta achar pela Tag "Player"
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("TrainTrackPiece");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
        }

        StartCoroutine(SpawnTargetedBombs());
    }

    IEnumerator SpawnTargetedBombs()
    {
        while (true)
        {
            // Só spawna se o alvo existir na cena
            if (target != null && target.position.x < maxX && target.position.x > minX)
            {
                // Pega a posição X atual do alvo e adiciona uma leve predição se ele estiver correndo
                float targetX = target.position.x;
                Vector3 spawnPosition = new Vector3(targetX, spawnY, 0f);

                // Cria a bomba exatamente acima do alvo
                Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
            }

            // Aguarda o tempo do intervalo para a próxima bomba
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) // Use OnCollisionEnter para 3D
    {
        // Exemplo: criar efeito de explosão (se tiver um prefab de partículas)
        // Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);

        // Destrói a bomba quando colidir com qualquer superfície
        Debug.Log(collision);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}

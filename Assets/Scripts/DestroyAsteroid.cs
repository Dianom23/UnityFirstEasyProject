using UnityEngine;

public class DestroyAsteroid : MonoBehaviour
{
    public ParticleSystem particle;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnDestroy()
    {
        gameManager.AddScore();
        GameObject gameObject = Instantiate(particle.gameObject, transform.position, Quaternion.identity);
        Destroy(gameObject, 2);
    }
}

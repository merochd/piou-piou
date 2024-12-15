using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletLifetime = 3f;
    [SerializeField] public int bulletType; // 0 = basic, 1 = piercing

    private void Start()
    {
        Destroy(gameObject, bulletLifetime); // Détruit la balle après un certain temps
    }

    private void Update()
    {
        transform.Translate(Vector3.up * (bulletSpeed * Time.deltaTime), Space.Self); // Déplacement
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
			 // Si c'est une balle basique, détruis la balle
            if (bulletType == 0)
            {
                Destroy(gameObject);
            }
            

            // Si c'est une balle piercing, ne fais rien (elle continue)
            if (bulletType == 1)
            {
                Debug.Log("Piercing bullet hit an enemy and continues.");
            }
        }
    }
}
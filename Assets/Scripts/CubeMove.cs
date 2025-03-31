using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float hitForce = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hittable")) // Verifica que el objeto golpeado tenga el tag correcto
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aplicar una fuerza en la dirección del golpe
                Vector3 forceDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(forceDirection * hitForce, ForceMode.Impulse);
            }
        }
    }
}

using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private float _tossForce;

    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            float xMultiplier = Random.Range(-0.5f, 0.5f);
            _rigidBody.AddForce(new Vector3(xMultiplier, 1, 0) * _tossForce, ForceMode.Impulse);
         
        }
    }
}

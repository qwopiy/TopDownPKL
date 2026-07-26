using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy;
    private float damage;
    private float speed;
    private float timeAlive = 0f;
    private Vector3 dir = Vector3.forward;


    private void Start()
    {
        if (dir.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(dir.normalized);
            return;
        }
    }


    private void FixedUpdate()
    {
        MoveForward();

        timeAlive += Time.fixedDeltaTime;
        if (timeAlive >= timeBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }

    public void AddBulletData(float _damage, float _speed, Vector3 _dir)
    {
        damage = _damage;
        speed = _speed;
        dir = _dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UnitStatsManager>() != null)
        {
            other.GetComponent<UnitStatsManager>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void MoveForward()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}

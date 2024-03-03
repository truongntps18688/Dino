using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] public Rigidbody2D _rb;
    [SerializeField] public float _speed;
    [SerializeField] private float _accuracy;
    [SerializeField] private float _drag;
    [SerializeField] private float _destroyTime;
    [SerializeField] private Vector3 _flyingDirection;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        _rb.AddForce(_flyingDirection.normalized * _speed, ForceMode2D.Impulse);
    }
    public void CreateProjectile(Transform firePoint, int spreadAngle)
    {        
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        //_flyingDirection = Vector3.right;
    }


}

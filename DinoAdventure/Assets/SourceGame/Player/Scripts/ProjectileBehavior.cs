using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _accuracy;
    [SerializeField] private float _drag;
    [SerializeField] private float _destroyTime;
    [SerializeField] private Vector3 _flyingDirection;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _flyingDirection);
        if(transform.position.x >= 10.0f)
        {
            gameObject.SetActive(false);
        }
    }
    public void CreateProjectile(Transform firePoint)
    {
        //var projecttile = Instantiate(this.gameObject, firePoint.position, firePoint.rotation);
        //Destroy(projecttile, _destroyTime);

        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);
        }
    }

    
}

using UnityEngine;

public class Pistol : RangeWeapon
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;
    //[SerializeField] private ProjectileBehavior _projectileBehavior;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private int _spreadAngle;

    
    private void Awake()
    {
        ReloadTime = 0.2f;
        SpreadAngle = _spreadAngle;
        firePoint = _firePoint;
        projectilePrefab = _projectilePrefab;
        projectileRb = _rigidbody2D;
        //projectileBehavior = _projectileBehavior;
        
    }
    private void Update()
    {
        coolDownCounter -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && coolDownCounter <= 0f && Time.timeScale != 0)
        {
            Fire();
        }
        Movement();
        
    }
    
}

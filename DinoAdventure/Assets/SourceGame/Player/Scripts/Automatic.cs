using UnityEngine;

public class Automatic : RangeWeapon
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private ProjectileBehavior _projectileBehavior;
    [SerializeField] private float _reloadTime;
    private void Awake()
    {
        ReloadTime = _reloadTime;
        firePoint = _firePoint;
        projectilePrefab = _projectilePrefab;
        projectileBehavior = _projectileBehavior;

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

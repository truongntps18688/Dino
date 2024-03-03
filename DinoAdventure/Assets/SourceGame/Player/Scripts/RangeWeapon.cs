using UnityEngine;
public abstract class RangeWeapon : MonoBehaviour ,IRangeWeapon
{
    protected Vector3 mousePos;
    protected Vector3 weaponPos;
    protected float angle;
    protected GameObject projectilePrefab;
    protected Rigidbody2D projectileRb;
    protected Transform firePoint;
    protected ProjectileBehavior projectileBehavior;

    protected float coolDownCounter = 0f;

    public float ReloadTime{ get; set; }

    public int SpreadAngle{ get; set; }

    //public bool FiringMethod { get; set; }

    public virtual void Fire()
    {
        //Shaking
        //Muzzle'
        //Create projectile
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);
        }

        //Recoil
        coolDownCounter = ReloadTime;
    }

    

    public void Movement()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 5.23f; //The distance between the camera and object
        weaponPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - weaponPos.x;
        mousePos.y = mousePos.y - weaponPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
    }
}

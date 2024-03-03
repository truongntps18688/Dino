public interface IRangeWeapon
{
    float ReloadTime { get; set; }
    int SpreadAngle { get; set; }

    void Movement();
    void Fire();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : Tower
{

    GameObject slingshotProjectile;

    override protected void Outside_Update()
    {

    }

    override protected void initiateProjectile()
    {
        Vector3 a = new Vector3(0f, 1.35f, 0);
        slingshotProjectile = Instantiate(projectile, a + transform.position, Quaternion.Euler(0, transform.localEulerAngles.y, 0));
        slingshotProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileVelocity * Time.deltaTime * 60f * 50f);
    }
}

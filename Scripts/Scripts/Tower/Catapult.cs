using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : Tower
{
    GameObject catapulttProjectile;

    override protected void Outside_Update()
    {

    }
    override protected void initiateProjectile()
    {
        Vector3 a = new Vector3(0, 4f, 0f);
        catapulttProjectile = Instantiate(projectile, a + transform.position, Quaternion.Euler(0, transform.localEulerAngles.y, 0));
        catapulttProjectile.GetComponent<Rigidbody>().AddForce(-transform.forward * projectileVelocity * Time.deltaTime * 60f * 50f);

        //add force up depedent on enemy range
        float distance = (target.transform.position - transform.position).magnitude;

        catapulttProjectile.GetComponent<Rigidbody>().AddForce(transform.up * 2500f * distance * Time.deltaTime);
        catapulttProjectile.GetComponent<ProjectileScript>().target = target;
    }

}

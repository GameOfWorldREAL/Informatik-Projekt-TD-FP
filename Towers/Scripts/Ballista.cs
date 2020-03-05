using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : Tower
{

    GameObject ballistaProjectile;

    override protected void Outside_Update()
    {
        GameObject fuss = GameObject.Find("Fuss");
        Quaternion r = transform.rotation;
        fuss.transform.rotation = Quaternion.Euler(r.x, r.y, r.z);
    }

    override protected void initiateProjectile()
    {
        Vector3 a = new Vector3(0, 1.06f, 0f);
        ballistaProjectile = Instantiate(projectile, a + transform.position, Quaternion.Euler(90, transform.localEulerAngles.y, 0));
        ballistaProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileVelocity * Time.deltaTime * 60f * 50f);
    }
}

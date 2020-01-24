using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour
{
    public GameObject body;
    public GameObject cmaPivot;

    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;
    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;
    public float frameCounter = 20;
    Quaternion originalRotation;



    public void Update()
    {
        //Resets the average rotation
        rotAverageY = 0f;
        rotAverageX = 0f;

        //Gets rotational input from the mouse
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;

        if (rotationY > maximumY)
        {
            rotationY = maximumY;
        }
        if (rotationY < minimumY)
        {
            rotationY = minimumY;
        }

        //Adds the rotation values to their relative array
        rotArrayY.Add(rotationY);
        rotArrayX.Add(rotationX);

        //If the arrays length is bigger or equal to the value of frameCounter remove the first value in the array
        if (rotArrayY.Count >= frameCounter)
        {
            rotArrayY.RemoveAt(0);
        }
        if (rotArrayX.Count >= frameCounter)
        {
            rotArrayX.RemoveAt(0);
        }

        //Adding up all the rotational input values from each array
        for (int j = 0; j < rotArrayY.Count; j++)
        {
            rotAverageY += rotArrayY[j];
        }
        for (int i = 0; i < rotArrayX.Count; i++)
        {
            rotAverageX += rotArrayX[i];
        }

        //Standard maths to find the average
        rotAverageY /= rotArrayY.Count;
        rotAverageX /= rotArrayX.Count;

        if (rotAverageY > maximumY)
        {
            rotAverageY = maximumY;
        }
        if (rotAverageY < minimumY)
        {
            rotAverageY = minimumY;
        }


        //Clamp the rotation average to be within a specific value range
        rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
        rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

        //Get the rotation you will be at next as a Quaternion
        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

        //Rotate
        cmaPivot.transform.rotation = originalRotation * xQuaternion * yQuaternion;
        body.transform.rotation = originalRotation * xQuaternion;
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == true)
        {
             rb.freezeRotation = true;
        }
           
        originalRotation = transform.localRotation;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
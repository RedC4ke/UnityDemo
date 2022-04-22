using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimToMouse : MonoBehaviour
{
    Transform crosshair;

    private void Start()
    {
        crosshair = transform.Find("Crosshair");
    }

    private void Update()
    {
        RotateToCursor();
    }

    void RotateToCursor()
    {
        Vector3 difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        // Prevent child from rotating, is this even necessary?
        crosshair.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}

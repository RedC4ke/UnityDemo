using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{
    public float damage;
    public float knockbackForce;
    [SerializeField] float attackSpeed;
    [SerializeField] float critChance;
    [SerializeField] float critMulti;

    //Rotation
    Quaternion lookRotation;
    Vector3 direction;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    protected abstract void Attack();

}

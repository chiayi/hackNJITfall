using UnityEngine;
using System.Collections;

public class CollisionDeath : MonoBehaviour
{
    Vector3 spawnPoint;
 
    void Start()
    {
        GameObject spawnObject = GameObject.FindGameObjectWithTag("Panel");
        spawnPoint = this.transform.position;
    }
    public GameObject Cube;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Death")
        { 
            transform.position = spawnPoint;
        }
    }
}

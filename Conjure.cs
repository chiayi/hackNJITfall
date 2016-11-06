using UnityEngine;
using System.Collections;
using Leap;
using System.Collections.Generic;
using Leap.Unity;

public class Conjure : MonoBehaviour
{
    Leap.Controller controller;

    // Use this for initialization
    void Start()
    {
        controller = new Controller();
    }


    // Update is called once per frame
    public bool isConjured = false;
    public GameObject temp;
    float nextFire = 0.0f;
    int counter = 0;
    void Update()
    {
        {
            Frame frame = controller.Frame();
            
            if (frame.Hands.Count == 2)
            {
                List<Hand> hands = frame.Hands;
                Hand one = hands[0];
                Hand two = hands[1];
                //Debug.Log(one.PinchDistance);
                if (one.PinchDistance < 20 && two.PinchDistance < 20 && !isConjured)
                {
                    GameObject eye = GameObject.Find("LeapHandController");
                    EyeOfA eyeA = eye.GetComponent<EyeOfA>();

                    GameObject a = eyeA.eyeOfA;
                    temp = GameObject.Instantiate(a) as GameObject;
                    temp.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    temp.transform.Rotate(90, 0, 0);
                    temp.transform.position = new Vector3(0, 0, 1);
                    isConjured = true;
                }
                else if (one.PinchDistance < 20 && two.PinchDistance < 20 && isConjured)
                {
                    temp.transform.Rotate(0, 0, 2);
                    if (temp.transform.localScale.x < 0.3f)
                    {
                        temp.transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);
                    }
                    else
                    {
                        if (Time.time > nextFire && counter < 10)
                        {
                            counter++;
                            GameObject clyinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                            clyinder.transform.position.Set(0, 0, 3);
                            clyinder.transform.Rotate(90, 0, 0);
                            Vector3 scale = transform.localScale;
                            scale.x = 0.2f;
                            scale.y = 0.2f;
                            scale.z = 0.2f;
                            clyinder.transform.localScale = scale;
                            Rigidbody clyinderRigid = clyinder.AddComponent<Rigidbody>().GetComponent<Rigidbody>();
                            clyinderRigid.useGravity = false;
                            clyinderRigid.AddForce(transform.forward * 500);
                            Destroy(clyinder, 2f);
                            nextFire = Time.time + 0.2f;
                        }
                      
                    }
                }
                else
                {
                    Destroy(temp);
                    isConjured = false;
                    counter = 0;
                }
              
            }
        }
    }
}

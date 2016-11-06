using UnityEngine;
using System.Collections;
using Leap;
using System.Collections.Generic;
using Leap.Unity;

public class Spells : MonoBehaviour
{
 
    Leap.Controller controller;
   


    // Use this for initialization
    void Start()
    {
        controller = new Controller();
    }

    // Update is called once per frame
    float nextFire = 0.0f;
    void Update()
    {
        Frame frame = controller.Frame();
        if (frame.Hands.Count > 0)
        {
            List<Hand> hands = frame.Hands;
            Hand firstHand = hands[0];
            Finger thumb = frame.Hands[0].Fingers
               [(int)Finger.FingerType.TYPE_THUMB];
            Finger index = frame.Hands[0].Fingers
               [(int)Finger.FingerType.TYPE_INDEX];
            Finger middle = frame.Hands[0].Fingers
                [(int)Finger.FingerType.TYPE_MIDDLE];
            Finger ring = frame.Hands[0].Fingers
                [(int)Finger.FingerType.TYPE_RING];
            Finger pinky = frame.Hands[0].Fingers
                [(int)Finger.FingerType.TYPE_PINKY];


            if (Time.time > nextFire && firstHand.IsRight)
            {
               
                Vector position = firstHand.PalmPosition/1000;
                bool extended = thumb.IsExtended && index.IsExtended && middle.IsExtended && ring.IsExtended && pinky.IsExtended;
                if (extended)
                {
                  
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.GetComponent<Renderer>().material.color = Color.red;
                    sphere.transform.position = position.ToVector3();
                    Vector3 scale = transform.localScale;
                    scale.x = 0.2f;
                    scale.y = 0.2f;
                    scale.z = 0.2f;
                    sphere.transform.localScale = scale;
                    Rigidbody sphereRigid = sphere.AddComponent<Rigidbody>().GetComponent<Rigidbody>();
                    sphereRigid.mass = 1f;
                    sphereRigid.useGravity = false;
                    Vector3 direct = new Vector3(firstHand.PalmNormal.ToVector3().x, firstHand.PalmNormal.ToVector3().y, -firstHand.PalmNormal.ToVector3().z);
                    sphereRigid.AddForce(transform.TransformVector(direct)*500);
                    Destroy(sphere, 3f);

                   nextFire = Time.time + 1.0f;

                    }


            }
        }
    }
}
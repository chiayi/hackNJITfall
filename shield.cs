using UnityEngine;
using System.Collections;
using Leap;
using System.Collections.Generic;
using Leap.Unity;

public class shield : MonoBehaviour
{
  //  private Texture2D txture;
    Leap.Controller controller;

    // Use this for initialization
    void Start()
    {
        //txture = Resources.Load("blah.jpg") as Texture2D;
        controller = new Controller();
    }

    // Update is called once per frame
    bool shielded = false;
    private GameObject cube;
    void Update()
    {
        Frame frame = controller.Frame();
        if (frame.Hands.Count > 1)
        {
            List<Hand> hands = frame.Hands;
            Hand secondHand = hands[1];
            Finger thumb = frame.Hands[1].Fingers
               [(int)Finger.FingerType.TYPE_THUMB];
            Finger index = frame.Hands[1].Fingers
               [(int)Finger.FingerType.TYPE_INDEX];
            Finger middle = frame.Hands[1].Fingers
                [(int)Finger.FingerType.TYPE_MIDDLE];
            Finger ring = frame.Hands[1].Fingers
                [(int)Finger.FingerType.TYPE_RING];
            Finger pinky = frame.Hands[1].Fingers
                [(int)Finger.FingerType.TYPE_PINKY];

            Vector position = secondHand.PalmPosition / 1000;
            bool extended = !thumb.IsExtended && !index.IsExtended && !middle.IsExtended && !ring.IsExtended && !pinky.IsExtended;
            if (extended && !shielded && secondHand.IsLeft)
            {

                shielded = true;
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Vector3 scale = transform.localScale;
                scale.x = 0.6f;
                scale.y = 0.6f;
                scale.z = 0.05f;
                cube.transform.localScale = scale;
            }
            else if (extended && shielded)
            {
                Vector3 direct = new Vector3(secondHand.PalmNormal.ToVector3().x, secondHand.PalmNormal.ToVector3().y, -secondHand.PalmNormal.ToVector3().z);
                //Texture2D blah = Resources.Load("blah.jpg") as Texture2D;
                // cube.transform.Rotate(direct);
                //  cube.GetComponent<Renderer>().material.mainTexture = txture;
                cube.transform.position = position.ToVector3() + new Vector3(-0.2f, 0, 0);
                //cube.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.09f);
                //Physics.IgnoreCollision(cube.GetComponent<Collider>(), transform.Find("Spells").GetComponent<Collider>());
            }
            else if (!extended)
            {
                shielded = false;
                Destroy(cube);
            }
            else
            {
                Destroy(cube);
            }
        }
     
        }
    }

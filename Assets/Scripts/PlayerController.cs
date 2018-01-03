using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float distanceToTravel;
    public float speed = 4.0f;
    private float timeToTravel;
    private float myTime = 0.0f;
    private bool showCutscene;
    public AnimationCurve lerpCurve;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.parent.GetChild(0).position;
        endPosition = transform.parent.GetChild(1).position;

        transform.position = startPosition;
        distanceToTravel = Vector3.Distance(startPosition, endPosition);
        timeToTravel = distanceToTravel / speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            showCutscene = true;
        }
        if (showCutscene && myTime < timeToTravel)
        {

            myTime += Time.deltaTime;
            if (myTime >= timeToTravel)
            {
                showCutscene = false;
            }
            transform.position = Vector3.Lerp(startPosition, endPosition, lerpCurve.Evaluate(myTime/timeToTravel));
        }

    }
}

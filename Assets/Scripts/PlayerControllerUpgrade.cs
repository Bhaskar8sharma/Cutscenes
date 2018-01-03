using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUpgrade : MonoBehaviour
{
    public float speed = 4.0f;
    private AnimationCurve animationCurve;

    private float distanceToTravel;
    private float timeToTravel;
    private float myTime;
    private bool showCutscene;
    private PointSetter gamePoints;
    private int currentPoint;
    private int maxPoints;
    private Vector3 start;
    private Vector3 end;

    // Use this for initialization
    void Start()
    {
        gamePoints = GameObject.FindGameObjectWithTag("GameController").GetComponent<PointSetter>();
        animationCurve = gamePoints.PlayerMovement;
        showCutscene = false;
        currentPoint = 0;
        maxPoints = 0;
        myTime = 0.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gamePoints.HidePlayer && !showCutscene)
        {
            showCutscene = true;
            maxPoints = gamePoints.Targets.Count;
        }

        if (currentPoint > gamePoints.Targets.Count - 2)
        {
            transform.position = gamePoints.Targets[currentPoint].transform.position;
            showCutscene = false;
            currentPoint = 0;
            myTime = 0.0f;
        }
        
        if (showCutscene)
        {
            start = gamePoints.Targets[currentPoint].transform.position;
            end = gamePoints.Targets[currentPoint + 1].transform.position;
            distanceToTravel = Vector3.Distance(start, end);
            timeToTravel = distanceToTravel / speed;

            myTime += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, animationCurve.Evaluate(myTime / timeToTravel));
            transform.right = (end - start).normalized;
            if(myTime >= timeToTravel)
            {
                currentPoint++;
                myTime = 0.0f;
            }
        }
    }
}

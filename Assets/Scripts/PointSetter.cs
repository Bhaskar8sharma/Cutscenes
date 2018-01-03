using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSetter : MonoBehaviour
{

    private List<GameObject> targets;
    public GameObject target;
    public GameObject Player;
    private bool hidePlayer;
    public AnimationCurve PlayerMovement;
    public Text message;

    public List<GameObject> Targets
    {
        get
        {
            return targets;
        }

        set
        {
            targets = value;
        }
    }

    public bool HidePlayer
    {
        get
        {
            return hidePlayer;
        }

        set
        {
            hidePlayer = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        targets = new List<GameObject>();
        HidePlayer = true;
        message.text = "left mouse click to add points";

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Setup();
        }
        if (targets.Count > 2 && HidePlayer)
        {
            ReadyForDeployment();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPoints();
        }
    }

    public void ReadyForDeployment()
    {
        message.text = "Use left mouse click to add points\nPress [Spacebar] to start sequence";
        //startButton.GetComponent<Renderer>().enabled = true;
        Instantiate(Player, targets[0].transform.position, Quaternion.identity);
        HidePlayer = false;
    }

    public void ResetPoints()
    {
        message.text = "Use left mouse click to add points\nPress R to reset points";
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        foreach (GameObject gameObject in targets)
        {
            Destroy(gameObject);
        }
        targets.Clear();
        HidePlayer = true;
    }

    public void Setup()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log(hit.transform.name);
        }
        else if (!Physics.Raycast(ray) && targets.Count < 10)
        {
            GameObject newTarget = Instantiate(target, ray.origin, Quaternion.identity);
            targets.Add(newTarget);
        }
    }
}

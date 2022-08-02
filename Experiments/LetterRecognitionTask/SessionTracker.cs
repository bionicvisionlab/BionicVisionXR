using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionTracker : MonoBehaviour
{
    private Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = " ";

    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "Current session: " + (HallwayTaskController.Instance.currentBlock)+"/27"
                   + "\nCurrent trial: "+(HallwayTaskController.Instance.currentTrial+1)+"/6";
    }
}

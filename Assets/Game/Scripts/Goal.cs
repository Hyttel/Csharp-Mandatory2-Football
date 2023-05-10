using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] private Player scriptPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Ball>() != null)
        {
            Debug.Log("Goal");
            if(name.Equals("GoalDetector1"))
            {
                Debug.Log("You scored!");
                scriptPlayer.IncreaseMyScore();
            }
            else
            {
                Debug.Log("They scored!");
                scriptPlayer.IncreaseOtherScore();
            }
        }
    }
}

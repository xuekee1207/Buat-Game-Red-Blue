using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RedBall")
        {
            ScoreManager.instance.AddScore();
        }

        if (other.gameObject.tag == "BlueBall")
        {
            ScoreManager.instance.DeductScore();
        }
    }
}

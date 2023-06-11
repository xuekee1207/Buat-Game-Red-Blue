using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlueBall")
        {
            ScoreManager.instance.AddScore();
        }

        if (other.gameObject.tag == "RedBall")
        {
            ScoreManager.instance.DeductScore();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_Cat : MonoBehaviour
{

    public Transform player;
    static Animator animate;
	// Use this for initialization
	void Start ()
    {
        animate = GetComponent<Animator>(); // attaches to the animator component
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 18 && angle < 30) // determines the distance the players camera is from the cat
        {
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                        Quaternion.LookRotation(direction), 0.1f);
            animate.SetBool("isIdle", false); // cat is no longer idle
            if(direction.magnitude > 3)
            {
                this.transform.Translate(0, 0, 0.18f); // Determines the speed at which the cat travels on the Z axis when following the player 
                animate.SetBool("isRunning", true); // cat is now wlaking
                animate.SetBool("isRunning", false);
            }
            else
            {
                animate.SetBool("isWalking", true);
                animate.SetBool("isRunning", false);
            }
        }
        else
        {
            animate.SetBool("isIdle", true);
            animate.SetBool("isWalking", false);
            animate.SetBool("isRunning", false);
        }
	}
}

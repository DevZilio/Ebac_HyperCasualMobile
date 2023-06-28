// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AnimationExample : MonoBehaviour
// {
//     public Animation animation;

//     public AnimationClip run;
//     public AnimationClip idle;


//     public void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.A))
//         {
//             PlayAnimation(run);
//         }

//         else if (Input.GetKeyDown(KeyCode.S))
//         {
//             PlayAnimation(idle);

//         }
//     }

// public void PlayAnimation(AnimationClip c)
// {
//     // Play on animation
//     /*
//     animation.clip = c;
//     animation.Play();*/


//     //or

//     //Use CrossFade to make a smooth transition
//     animation.CrossFade(c.name);

// }

// }


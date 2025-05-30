using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    /// <summary>
    /// Controls the opening and closing of closet doors in the game environment
    /// </summary>
    public class ClosetopencloseDoor : MonoBehaviour
    {
        // Required components and state variables
        public Animator Closetopenandclose;  // Animator controlling closet door animations
        public bool open;                    // Current closet door state
        public Transform Player;             // Reference to player transform for distance calculation

        /// <summary>
        /// Initialize closet door to closed state
        /// </summary>
        void Start()
        {
            open = false;
        }

        /// <summary>
        /// Handles mouse interactions when hovering over the closet door
        /// </summary>
        void OnMouseOver()
        {
            // Check if player reference exists
            if (Player)
            {
                // Calculate distance between player and closet
                float dist = Vector3.Distance(Player.position, transform.position);
                
                // Check if player is within interaction range
                if (dist < 15)
                {
                    // Handle door state toggle based on current state
                    if (open == false)
                    {
                        // If door is closed and left mouse clicked, open it
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartCoroutine(opening());
                        }
                    }
                    else
                    {
                        // If door is open and left mouse clicked, close it
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartCoroutine(closing());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Coroutine to handle closet door opening animation and state change
        /// </summary>
        IEnumerator opening()
        {
            print("you are opening the door");
            // Play closet opening animation
            Closetopenandclose.Play("ClosetOpening");
            // Update door state
            open = true;
            
            // Play sound effect if available
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.doorOpen);
                
            // Wait for animation to complete
            yield return new WaitForSeconds(.5f);
        }

        /// <summary>
        /// Coroutine to handle closet door closing animation and state change
        /// </summary>
        IEnumerator closing()
        {
            print("you are closing the door");
            // Play closet closing animation
            Closetopenandclose.Play("ClosetClosing");
            // Update door state
            open = false;
            // Wait for animation to complete
            yield return new WaitForSeconds(.5f);
        }
    }
}
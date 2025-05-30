using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    /// <summary>
    /// Controls the opening and closing of doors in the game environment
    /// Handles locked doors that require keys to open
    /// </summary>
    public class opencloseDoor : MonoBehaviour
    {
        // Required components and state variables
        public Animator openandclose;     // Animator controlling door open/close animations
        public bool open;                 // Current door state
        public Transform Player;          // Reference to player transform for distance calculation
        
        // Key state trackers
        public bool hasKey = false;       // Whether player has the main door key
        public bool hasGarageKey = false; // Whether player has the garage door key
        public bool hasGardenKey = false; // Whether player has the garden door key

        /// <summary>
        /// Initialize door to closed state
        /// </summary>
        void Start()
        {
            open = false;
        }

        /// <summary>
        /// Handles mouse interactions when hovering over the door
        /// </summary>
        void OnMouseOver()
        {
            if (Player)
            {
                // Check if player is within interaction range
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 15)
                {
                    // Left mouse click to interact with door
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Check if door is locked and player has the required key
                        if ((CompareTag("LockedDoor") && !hasKey) || 
                            (CompareTag("GarageDoor") && !hasGarageKey) ||
                            (CompareTag("GardenDoor") && !hasGardenKey))
                        {
                            Debug.Log("Kapı kilitli. Anahtar gerekli.");
                            return; // Exit if door is locked and player doesn't have key
                        }

                        // Toggle door state
                        if (!open)
                        {
                            StartCoroutine(opening());
                        }
                        else
                        {
                            StartCoroutine(closing());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Coroutine to handle door opening animation and state change
        /// </summary>
        IEnumerator opening()
        {
            print("you are opening the door");
            // Play door opening animation
            openandclose.Play("Opening");
            // Update door state
            open = true;
            
            // Play sound effect if available
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.doorOpen);
                
            // Wait for animation to complete
            yield return new WaitForSeconds(.5f);
        }

        /// <summary>
        /// Coroutine to handle door closing animation and state change
        /// </summary>
        IEnumerator closing()
        {
            print("you are closing the door");
            // Play door closing animation
            openandclose.Play("Closing");
            // Update door state
            open = false;
            // Wait for animation to complete
            yield return new WaitForSeconds(.5f);
        }
    }
}
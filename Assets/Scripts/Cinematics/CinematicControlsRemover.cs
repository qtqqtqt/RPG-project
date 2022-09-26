using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;


namespace RPG.Cinematics
{
    public class CinematicControlsRemover : MonoBehaviour
    {
        PlayableDirector playableDirector;
        GameObject player;

        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;

            player = GameObject.FindGameObjectWithTag("Player");

        }

        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
            Debug.Log("DisabledControls");
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
            Debug.Log("EnabledControls");
        }
    }
}

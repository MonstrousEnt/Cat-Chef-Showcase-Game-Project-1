using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] private Vector2 checkpointOnePos;

    private void Start()
    {
        if (GameManager.instance.GetLevelStarted())
        {
            CollectablesUI.instance.SetText(GameManager.instance.GetCoinNum());

            if (!GameManager.instance.GetRestart())
            {
                AudioManager.instance.stopAudio("Level Music");
                AudioManager.instance.SetAudioLoop("Level Music", false);

                AudioManager.instance.SetAudioLoop("Level Music", true);
                AudioManager.instance.playAudio("Level Music");
            }

            GameManager.instance.SetRestart(false);

            LiveSystemManager.instance.ResetLives();

            PlayerBase.instance.gameObject.transform.position = checkpointOnePos;

            GameManager.instance.SetLevelStarted(false);
        }
    }
}

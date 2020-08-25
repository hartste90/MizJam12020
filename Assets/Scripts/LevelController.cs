using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public List<LevelTilemapController> tilemapControllers;
    public PlayerController playerPrefab;

    public PlayerController playerController;
    private int currentLevelIdx = 0;
    private LevelTilemapController currentTileMap;
    private Vector3 lastGongPosition;

    public void BeginGameplay(Vector3 playerStartPosition)
    {
        Debug.Log("Beginning gameplay");
        currentTileMap = tilemapControllers[0];
        currentTileMap.PlayLevelIntro(OnNewLevelIntroAnimationComplete);  

        Instance.playerController = Instantiate<PlayerController>(Instance.playerPrefab, null);
        Instance.playerController.transform.position = playerStartPosition + .44f * Vector3.up;
        CharacterController2D.Instance = Instance.playerController.GetComponent<CharacterController2D>();
        CharacterController2D.Enable();
    }

    public static void HandleGoalReached()
    {
        Instance.playerController.playerMovement.Disable();
        Instance.playerController.PlayIdleAnimation();
        if(Instance.currentTileMap.isFinalLevel)
        {
            Debug.Log("final level reached");
            // Instance.currentTileMap.PlayFinalLevelOutro(Instance.FinalLevelTilemapsOutroComplete);
        }
        Instance.GoToNextLevel();
    }

    public void FinalLevelTilemapsOutroComplete()
    {
        Debug.Log("all final outros complete");
        //show game complete animation
        GameController.ShowCongratulations();

    }

    public static void HandlePlayerDeath()
    {
        //spawn new player at last gong position
        Debug.Log("Spawning player");
        Destroy(Instance.playerController.gameObject);
        Instance.playerController = Instantiate<PlayerController>(Instance.playerPrefab, null);
        Instance.playerController.transform.position = Instance.lastGongPosition + .44f * Vector3.up;
        CharacterController2D.Instance = Instance.playerController.GetComponent<CharacterController2D>();
        CharacterController2D.Enable();
    }
    public void GoToNextLevel()
    {
        //successful completion of level
        Debug.Log("Level completed successfully!");
        //freeze gameplay / disallow input
        // InputController.Enabled = false;
        if (currentTileMap.isFinalLevel)
        {
            currentTileMap.RemoveAllRemovables();
            OnLastLevelOutroAnimationComplete();

        }
        else
        {
            CharacterController2D.Disable();
            lastGongPosition = currentTileMap.goalLocation.position;
            currentLevelIdx += 1;
            currentTileMap = tilemapControllers[currentLevelIdx];
            currentTileMap.PlayLevelOutro(OnOldLevelOutroAnimationComplete);
        }
        
    }

    public void OnOldLevelOutroAnimationComplete()
    {
        currentTileMap.PlayLevelIntro(OnNewLevelIntroAnimationComplete);
    }

    public void OnLastLevelOutroAnimationComplete()
    {
        playerController.playerMovement.Enable();
        FinalLevelTilemapsOutroComplete();
    }

    public void OnNewLevelIntroAnimationComplete()
    {
        GameController.Instance.PlayDialogueByIdx(currentLevelIdx);
        CharacterController2D.Enable();
        playerController.playerMovement.Enable();
    }

    public static void KillPlayer()
    {
        GameController.Instance.PlayRandomDeathDialogue();
        Instance.playerController.HandlePlayerDeath();
    }
}

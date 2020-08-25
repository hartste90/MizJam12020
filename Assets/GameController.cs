using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public Transform playerStartPosition;
    public IntroController introController;
    public OutroController outroController;
    public DialogueController dialogeController;

    public ParticleSystem meatShower;

    void Start()
    {
        ShowIntroScreen();
    }

    public void BeginGameplay()
    {
        LevelController.Instance.BeginGameplay(playerStartPosition.position);
        dialogeController.PlayDialogueInAnimation();
    }

    public void PlayDialogueByIdx(int idx)
    {
        dialogeController.PlayDialogueByIdx(idx);
    }

    public static void ReleaseMeatShower()
    {
        Instance.meatShower.Play();
        
    }


    public void PlayRandomDeathDialogue()
    {
        float chanceToPlayEmote = .3f;
        float rand = Random.value;
        if (rand <= chanceToPlayEmote)
            dialogeController.PlayRandomDeathEmote();
    }

    void ShowIntroScreen()
    {
        introController.PlayIntro();
    }

    public static void ShowCongratulations()
    {
        //show congratulations
        Instance.dialogeController.PlayConclusionDialogue();
        //show replay button

        //thank you + credits
    }

    
    public void OnPlayButtonPressed()
    {
        introController.PlayOutro(OnIntroScreenOutroComplete);
    }

    public void OnIntroScreenOutroComplete()
    {
        BeginGameplay();
    }
}

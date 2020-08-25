using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum Emotion
{
    Happy = 0,
    Disappointed,
    Angry,
    Laughing
}

public class DialoguePair
{
    public string dialogue;
    public Emotion emotion;

    public DialoguePair(Emotion emotionSet, string dialogueSet)
    {
        dialogue = dialogueSet;
        emotion = emotionSet;
    }
}
public class DialogueController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Text textBox;
    public Image speakerIcon;
    public Image speakerEmoticon;
    public List<DialoguePair> dialogueLevelList =
        new List<DialoguePair> {
            new DialoguePair(Emotion.Disappointed, "Welcome to the SmArT Room!"),
            new DialoguePair(Emotion.Happy, "Here in the future, we swore never to eat SmArT creatures."),
            new DialoguePair(Emotion.Laughing, "The SmArT Room helps sort out the food from the rest of us!"),
            new DialoguePair(Emotion.Laughing, "Once we hear all  1 2  gongs you'll be free to join our feast!"),
            new DialoguePair(Emotion.Disappointed, "Most people turn out to be food, but that's kinda the point!"),
            new DialoguePair(Emotion.Angry, "Wow I didn't think you'd get this far..."),
            new DialoguePair(Emotion.Laughing, "Look at you go! I hope this part of the room isn't too tough."),
            new DialoguePair(Emotion.Happy, "This part gave me trouble too, but obviously I made it!"),
            
            new DialoguePair(Emotion.Disappointed, "Nobody knows who invented the SmArT Room, but they must be brilliant, right?!"),
            new DialoguePair(Emotion.Laughing, "You're talented for sure, but sometimes talent isn't enough!"),
            new DialoguePair(Emotion.Laughing, "Did you think it was going to get easier?"),
            new DialoguePair(Emotion.Happy, "One way or another, let's EAT!"),
            
        };

    public DialoguePair conclusionDialoge = new DialoguePair(Emotion.Happy, "We knew from the beginning you were SmArT! You can always tell when something is Food.  But not you, you're one of us!  Welcome to the feast!");
    public List<Sprite> animalIconsList;
    public List<Sprite> emotionIconsList;
    public List<DialoguePair> onDeathRandomDialogues =
        new List<DialoguePair> {
            new DialoguePair(Emotion.Disappointed, "Hopefully the next morsel has better luck!"),
            new DialoguePair(Emotion.Happy, "Great! I was getting hungry."),
            new DialoguePair(Emotion.Laughing, "Ouch."),
            new DialoguePair(Emotion.Laughing, "Dinner is served."),
            new DialoguePair(Emotion.Disappointed, "That part is tricky."),
            new DialoguePair(Emotion.Laughing, "Thank you for your donation!"),
            new DialoguePair(Emotion.Laughing, "Tastes like chicken."),
            new DialoguePair(Emotion.Happy, "Yummmmm."),
            new DialoguePair(Emotion.Laughing, "Dibs on the gizzard!"),
            new DialoguePair(Emotion.Laughing, "Better you than me I always say!"),
            new DialoguePair(Emotion.Disappointed, "This is how it has to be."),
            new DialoguePair(Emotion.Happy, "Praise the Room!"),
            new DialoguePair(Emotion.Laughing, "That part almost got me too, hah!"),
            
        };

        public Transform extraConclusionCharactersCharacters;
        public Transform extraCharacterTargetPosition;
        public Button menuButton;
        public Sprite blankSprite;
    // Start is called before the first frame update
    void Awake()
    {
        // PlayTextAnimation(new DialoguePair(Emotion.Happy, "This is a test sentence!"));
        // PlayRandomDeathEmote();
        canvasGroup.alpha = 0f;
        textBox.text = "";
        speakerIcon.sprite = blankSprite;
        speakerEmoticon.sprite = blankSprite;
    }

    void SetRandomAnimalIcon()
    {
        speakerIcon.sprite = animalIconsList[Random.Range(0, animalIconsList.Count)];
    }

    public void PlayConclusionDialogue()
    {
        SetRandomAnimalIcon();
        PlayTextAnimation(conclusionDialoge);
        PlayEmotionAnimation(conclusionDialoge.emotion);
        Sequence seq = DOTween.Sequence();
        seq.Append(extraConclusionCharactersCharacters.DOMove(extraCharacterTargetPosition.position, 1f));
        seq.AppendInterval(1f);
        seq.AppendCallback(ReleaseMeatShower);
    }

    public void ReleaseMeatShower()
    {
        GameController.ReleaseMeatShower();
        menuButton.transform.DOLocalMoveY(304f, 2f).SetEase(Ease.OutBack);
    }

    public void PlayDialogueInAnimation()
    {
        canvasGroup.DOFade(1f, .75f);
    }

    public void PlayDialogueByIdx(int idx)
    {
        SetRandomAnimalIcon();
        DialoguePair pair = dialogueLevelList[idx];
        PlayTextAnimation(pair);
        PlayEmotionAnimation(pair.emotion);
    }

    public void PlayRandomDeathEmote()
    {
        SetRandomAnimalIcon();
        DialoguePair pair = onDeathRandomDialogues[Random.Range(0, onDeathRandomDialogues.Count)];
        PlayTextAnimation(pair);
        PlayEmotionAnimation(pair.emotion);

    }

    void PlayEmotionAnimation(Emotion emotion)
    {
        Sprite sprite;
        switch(emotion)
        {
            case Emotion.Happy:
                sprite = emotionIconsList[0];
                break;
            case Emotion.Disappointed:
                sprite = emotionIconsList[0];
                break;
            case Emotion.Laughing:
                sprite = emotionIconsList[0];
                break;
            default:
                sprite = emotionIconsList[3];
                break;
        }
        speakerEmoticon.sprite = sprite;
        speakerEmoticon.transform.parent.DOShakePosition(.4f, 5f).SetDelay(.5f);
    }

    void PlayTextAnimation(DialoguePair pair)
    {
        textBox.text = "";
        textBox.DOText(pair.dialogue,pair.dialogue.Length * .02f, true, ScrambleMode.None).SetEase(Ease.Linear);
    }

    public void OnMenuButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTilemapController : MonoBehaviour
{
    public Transform goalLocation;
    public List<TileMapAnimatorController> tileMapsAdded;
    public List<TileMapAnimatorController> tileMapsRemoved;
    
    public GoalController goalPrefab;
    public GoalController finalGoalPrefab;
    public List<TileMapAnimatorController> finalLevelRemovables;

    public bool isFinalLevel = false;

    private UnityAction onAllIntrosCompleteCallback;
    private UnityAction onAllOutrosCompleteCallback;
    private UnityAction onAllFinalOutrosCompletedCallback;
    private int completedIntrosCount;
    private int completedOutrosCount;
    public void PlayLevelIntro(UnityAction onComplete)
    {
        onAllIntrosCompleteCallback = onComplete;
        completedIntrosCount = 0;
        foreach(TileMapAnimatorController tilemapAnimator in tileMapsAdded)
        {
            tilemapAnimator.PlayIntro(TrackIntroComplete);
        }

        //no intros to do
        if (tileMapsAdded.Count == 0)
        {
            
            OnAllIntrosComplete();
        }
    }

    public void PlayLevelOutro(UnityAction onComplete)
    {
        onAllOutrosCompleteCallback = onComplete;
        completedOutrosCount = 0;
        foreach(TileMapAnimatorController tilemapAnimator in tileMapsRemoved)
        {
            tilemapAnimator.PlayOutro(TrackOutroComplete);
        }

        //no outros to do
        if (tileMapsRemoved.Count == 0)
        {
            onAllOutrosCompleteCallback.Invoke();
        }
    }

    public void RemoveAllRemovables()
    {
        foreach(TileMapAnimatorController tilemapAnimator in finalLevelRemovables)
        {
            tilemapAnimator.PlayOutro(null);
        }
    }

    public void TrackIntroComplete()
    {
        completedIntrosCount ++;
        if (completedIntrosCount >= tileMapsAdded.Count)
        {
            OnAllIntrosComplete();
        }
    }
    
    public void TrackOutroComplete()
    {
        completedOutrosCount ++;
        
        if (completedOutrosCount >= tileMapsRemoved.Count)
        {
            onAllOutrosCompleteCallback?.Invoke();
        }
    }

    private void OnAllIntrosComplete()
    {
        CreateGoal();
        onAllIntrosCompleteCallback?.Invoke();
    }

    private void CreateGoal()
    {
        Debug.Log("creating goal");
        if(isFinalLevel)
        {
            GoalController finalGoal = Instantiate<GoalController>(finalGoalPrefab, goalLocation);
            finalGoal.transform.localPosition = Vector3.up * 1.05f;
        }
        else
        {
            GoalController goal = Instantiate<GoalController>(goalPrefab, goalLocation);
            goal.transform.localPosition = Vector3.up * .44f;
        }
    }
}

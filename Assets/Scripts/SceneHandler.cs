﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneHandler : SingletonMonoBehavior<SceneHandler>
{
    [Header("Scene Data")]
    [SerializeField] private List<string> levels;
    [SerializeField] private string menuScene;

    [Header("Transition Animation Data")]
    [SerializeField] private Ease animationType;
    [SerializeField] private float animationDuration;
    [SerializeField] private RectTransform transitionCanvas;

    [Header("Video Transition")]
    [SerializeField] private VideoPlayer transitionVideo;         // video player object
    [SerializeField] private GameObject transitionPanelObject;    // actual video - RawImage 
    private int nextLevelIndex;
    private float initXPosition;
    private string sceneToLoad;

    protected override void Awake()
    {
        base.Awake();
        initXPosition = transitionCanvas.anchoredPosition.x;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != menuScene)
        {
            SceneManager.LoadScene(menuScene);
        }
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode _)
    {
        // panel starts to left of screen 
        transitionCanvas.anchoredPosition = new Vector2(-Screen.width, 0);

    }

    public void LoadNextScene()
    {
        if (nextLevelIndex >= levels.Count)
        {
            LoadMenuScene();
        }
        else
        {
            sceneToLoad = levels[nextLevelIndex];
            nextLevelIndex++;
            StartCoroutine(PacmanAnimationScene());
        }
    }

    public void LoadMenuScene()
    {
        sceneToLoad = menuScene;
        nextLevelIndex = 0;
        StartCoroutine(PacmanAnimationScene());
    }

private IEnumerator PacmanAnimationScene()
{
    //video appears
    transitionPanelObject.SetActive(true);
    transitionVideo.gameObject.SetActive(true);

    // video loads prior to slide in
    transitionVideo.Stop();
    transitionVideo.frame = 0;
    transitionVideo.Prepare();

    while (!transitionVideo.isPrepared)
        yield return null;

    Debug.Log("Video ready");

    // video slides in
    yield return transitionCanvas
        .DOAnchorPos(Vector2.zero, animationDuration)
        .SetEase(animationType)
        .WaitForCompletion(); //waits till complete before exit right

    //starts loading next scene
    AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneToLoad);
    loadOp.allowSceneActivation = false;

    // Play the video once the transition is in place
    transitionVideo.Play();
    Debug.Log("Video playing");

    bool videoFinished = false;
    transitionVideo.loopPointReached += (_) =>
    {
        Debug.Log("Video done");
        videoFinished = true;
    };

    yield return new WaitUntil(() => videoFinished);


    //activates next scene during pacman video transition
     loadOp.allowSceneActivation = true;

    // video exits to the right
    yield return transitionCanvas
        .DOAnchorPos(new Vector2(Screen.width, 0), animationDuration)
        .SetEase(animationType)
        .WaitForCompletion();

 

}

}



//ORIGINAL SCRIPT 
// using System.Collections;
// using System.Collections.Generic;
// using DG.Tweening;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SceneHandler : SingletonMonoBehavior<SceneHandler>
// {
//     [Header("Scene Data")]
//     [SerializeField] private List<string> levels;
//     [SerializeField] private string menuScene;
//     [Header("Transition Animation Data")]
//     [SerializeField] private Ease animationType;
//     [SerializeField] private float animationDuration;
//     [SerializeField] private RectTransform transitionCanvas;

//     private int nextLevelIndex;
//     private float initXPosition;

//     protected override void Awake()
//     {
//         base.Awake();
//         initXPosition = transitionCanvas.transform.localPosition.x;
//         SceneManager.LoadScene(menuScene);
//         SceneManager.sceneLoaded += OnSceneLoad;
//     }

//     private void OnSceneLoad(Scene scene, LoadSceneMode _)
//     {
//         transitionCanvas.DOLocalMoveX(initXPosition, animationDuration).SetEase(animationType);
//     }

//     public void LoadNextScene()
//     {
//         if(nextLevelIndex >= levels.Count)
//         {
//             LoadMenuScene();
//         }
//         else
//         {
//             transitionCanvas.DOLocalMoveX(initXPosition + transitionCanvas.rect.width, animationDuration).SetEase(animationType);
//             StartCoroutine(LoadSceneAfterTransition(levels[nextLevelIndex]));
//             nextLevelIndex++;
//         }
//     }

//     public void LoadMenuScene()
//     {
//         transitionCanvas.DOLocalMoveX(initXPosition + transitionCanvas.rect.width, animationDuration).SetEase(animationType);
//         StartCoroutine(LoadSceneAfterTransition(menuScene));
//         nextLevelIndex = 0;
//     }

//     private IEnumerator LoadSceneAfterTransition(string scene)
//     {
//         yield return new WaitForSeconds(animationDuration);
//         SceneManager.LoadScene(scene);
//     }
// }
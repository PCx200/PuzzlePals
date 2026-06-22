using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider))]
public class CutscenePlayer : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private SkylandersCamera gameplayCamera;

    private bool hasPlayed;

    private void Awake()
    {
        Collider trigger = GetComponent<Collider>();
        trigger.isTrigger = true;

        if (director == null)
            director = GetComponent<PlayableDirector>();

        if (gameplayCamera == null)
            gameplayCamera = FindFirstObjectByType<SkylandersCamera>();

        if (director != null)
            director.stopped += OnDirectorStopped;
    }

    private void OnDestroy()
    {
        if (director != null)
            director.stopped -= OnDirectorStopped;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed || !other.CompareTag(playerTag) || director == null)
            return;

        hasPlayed = true;
        gameplayCamera?.SetFollowEnabled(false);
        director.Play();
    }

    private void OnDirectorStopped(PlayableDirector _)
    {
        gameplayCamera?.SetFollowEnabled(true);
    }
}

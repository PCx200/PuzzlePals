using UnityEngine;
using FMODUnity;

public class PlaySoundOnAnimation : MonoBehaviour
{
    [field: SerializeField] public EventReference sound { get; private set; }
    public void PlaySound()
    {
        AudioManager.Instance.PlayOneShot(sound, transform.position);
    }
}

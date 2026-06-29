using FMOD;
using Unity.VisualScripting;
using UnityEngine;

public class PlayUISound : MonoBehaviour
{
        public void PlayClick()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.clickUI, transform.position);
    }
}

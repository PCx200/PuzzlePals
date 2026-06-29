using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace EventBus
{
    public class DreamState : SuperPower
    {
        private List<GameObject> invisibleObjs = new();
        [SerializeField] bool visible;
        [SerializeField] private ParticleSystem dreamParticles;

        private void Awake()
        {
            invisibleObjs = Resources.FindObjectsOfTypeAll<GameObject>()
        .Where(obj => obj.CompareTag("Invisible") && obj.scene.IsValid())
        .ToList();
        }

        public override void SuperPowerPressed()
        {
            dreamParticles.Play();
            if (!visible)
            {
                foreach (var obj in invisibleObjs)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                foreach (var obj in invisibleObjs)
                {
                    obj.SetActive(false);
                }
            }
            visible = !visible;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.dreamState, transform.position);
        }

        private void OnDisable()
        {
            foreach (var obj in invisibleObjs)
            {
                if (obj == null)
                {
                    return;
                }
                obj.SetActive(false);
            }
            visible = false;
        }

        private void OnDestroy()
        {
            foreach (var obj in invisibleObjs)
            {
                if (obj == null) return;
                obj.SetActive(false);
            }
            visible = false;
        }
    }
}

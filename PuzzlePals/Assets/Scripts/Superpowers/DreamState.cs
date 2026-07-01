using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Vignette = UnityEngine.Rendering.Universal.Vignette;

namespace EventBus
{
    public class DreamState : SuperPower
    {
        private List<GameObject> invisibleObjs = new();
        [SerializeField] bool visible;
        [SerializeField] private ParticleSystem dreamParticles;
        [SerializeField] private Volume volume;
        [SerializeField] private float vignetteIntensity;
        [SerializeField] private float vignetteDuration;
        private Vignette vignette;
        private bool transitioning;

        private void Start()
        {
            invisibleObjs = Resources.FindObjectsOfTypeAll<GameObject>()
                .Where(obj => obj.CompareTag("Invisible") && obj.scene.IsValid())
                .ToList();
            
            vignette = volume.profile.TryGet(out Vignette v) ? v : null;
        }

        public override void SuperPowerPressed()
        {
            dreamParticles.Play();
            if (!visible)
            {
                StartCoroutine(LerpEffect(vignetteIntensity));
                foreach (var obj in invisibleObjs)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                StartCoroutine(LerpEffect(0.2f));
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

        private IEnumerator LerpEffect(float vignetteI)
        {
            while (transitioning)
            {
                yield return null;
            }
            transitioning = true;
            float startVignette = vignette.intensity.value;
            float timer = 0f;
            while (timer <= vignetteDuration)
            {
                timer += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(startVignette, vignetteI, timer / vignetteDuration);
                yield return null;
            }
            transitioning = false;
        }
    }
}

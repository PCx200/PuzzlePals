using UnityEngine;

namespace EventBus
{
    public class Invisible : MonoBehaviour
    {
        bool visible = false;
        private void OnEnable()
        {
            EventBus.Subscribe<RenderInvisibleThings>(OnRenderInvisible); 
        }
        private void OnDisable()
        {
            EventBus.Unsubscribe<RenderInvisibleThings>(OnRenderInvisible);
        }
        private void OnRenderInvisible(RenderInvisibleThings eventData)
        {
            gameObject.SetActive(visible);
            visible = !visible;
        }
    }
}

using UnityEngine;

namespace EventBus
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Invisible : MonoBehaviour
    {
        MeshRenderer m_Renderer;
        bool visible = false;
        private void Awake()
        {
            m_Renderer = GetComponent<MeshRenderer>();
        }
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
            m_Renderer.enabled = visible;
            visible = !visible;
        }
    }
}

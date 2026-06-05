using UnityEngine;
namespace EventBus
{
    public class DreamState : SuperPower
    {

        public override void UseSuperPower()
        {
            RenderInvisibleThings renderInvisible = new RenderInvisibleThings();
            EventBus.Publish(renderInvisible);
        }
    }
}

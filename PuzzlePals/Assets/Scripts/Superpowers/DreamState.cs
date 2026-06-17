using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace EventBus
{
    public class DreamState : SuperPower
    {
        [SerializeField] List<GameObject> invisibleObjs = new();
        [SerializeField] bool visible;

        private void Awake()
        {
            invisibleObjs = Resources.FindObjectsOfTypeAll<GameObject>()
        .Where(obj => obj.CompareTag("Invisible") && obj.scene.IsValid())
        .ToList();
        }

        public override void UseSuperPower()
        {
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

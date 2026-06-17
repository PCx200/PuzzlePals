using System.Collections;
using UnityEngine;
[RequireComponent  (typeof(Collider))]
public class IceCreamWall : MonoBehaviour
{
    private Collider col;
    [SerializeField] float enableBack;
    private void Awake()
    {
        col = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered the collision");
        if (collision.gameObject.GetComponent<PlayerController>().currentMonster.Name == MonsterCharacter.MonsterName.Mida)
        {
            col.enabled = false;
            Debug.Log("Collider disabled");
            StartCoroutine(EnableColliderBack());
        }
        else Debug.Log("It is not Mida");
    }
    IEnumerator EnableColliderBack()
    {
        yield return new WaitForSeconds(enableBack);
        col.enabled = true;
        Debug.Log("Enabled back");
    }
}

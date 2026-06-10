using UnityEngine;
[RequireComponent (typeof(Animator))]
public class Befriendable : MonoBehaviour
{
    Animator animator;
    [SerializeField] private string animationName;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered the trigger");
        if(other.CompareTag("Cupcake"))
        {
            animator.Play(animationName);
            Destroy(other.gameObject);
            Debug.Log("Cupcake eaten");
        }
    }
}

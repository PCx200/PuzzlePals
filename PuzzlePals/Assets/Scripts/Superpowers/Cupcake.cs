using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Cupcake : SuperPower
{
    private PlayerController player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject cupcake;
    private GameObject spawnedCupcake = null;
    
    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }
    public override void SuperPowerPressed()
    {
        if (spawnedCupcake == null)
        {
            StartCoroutine(SpawnCupcakeAfterAnimation());
        }
        else
        {
            Destroy(spawnedCupcake);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.createCupcake, transform.position);
        }
    }

    private IEnumerator SpawnCupcakeAfterAnimation()
    {       
        player.currentMonster.Animator.SetTrigger("CreateCupcake");

        yield return new WaitForSeconds(player.currentMonster.Animator.GetAnimator().GetCurrentAnimatorClipInfo(0).Length);

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.createCupcake, transform.position);
        spawnedCupcake = Instantiate(cupcake, spawnPoint.transform.position, Quaternion.identity, spawnPoint);
    }
}

using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WindStream : MonoBehaviour
{
    public enum Direction { Froward, Backward, Up, Down }
    [SerializeField] private Direction direction;

    [SerializeField] private BoxCollider area;
    [SerializeField] private float windSpeed;
    [SerializeField] private Vector3 windSource;
    private Vector3 windDirection;
    private float windStrength;

    [SerializeField] private ParticleSystem windParticles;

    private void OnValidate()
    {
        if (area == null)
            area = GetComponent<BoxCollider>();
        
        windDirection = transform.forward;
        windSource = transform.position - transform.forward * area.size.z / 2.0f;
        windParticles.transform.position = windSource;
        windParticles.transform.rotation = Quaternion.LookRotation(windDirection);
    }

    void Start()
    {
        OnValidate();
    }
    
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player)
        {
            var rigidbody = other.GetComponent<Rigidbody>();

            //updates the wind strength depending on how far the player is from the source
            //var distanceFromSource = windSource - other.transform.position;
            //windStrength = windSpeed / distanceFromSource.magnitude;



            if (player.currentMonster.Name == MonsterCharacter.MonsterName.Jullia)
            {
                rigidbody.AddForce(windDirection * windSpeed / 15f, ForceMode.Force);
            }
            else
            {
                rigidbody.AddForce(windDirection * windSpeed, ForceMode.Force);
            }
        }
    }

    private void OnDrawGizmos()
    {
        OnValidate();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(windSource, 1f);
        //Gizmos.DrawWireCube(transform.position, area.size);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(windSource, transform.position + transform.forward * area.size.z / 2.0f);
    }
}

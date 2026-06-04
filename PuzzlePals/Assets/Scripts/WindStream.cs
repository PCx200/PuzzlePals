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

    private void OnValidate()
    {
        if (area == null)
            area = GetComponent<BoxCollider>();

        DetermineWindSource();
        DetermineWindDirection();
    }

    void Start()
    {

    }

    private void DetermineWindDirection()
    {
        switch (direction)
        {
            case Direction.Froward:
                windDirection = Vector3.right;
                break;
            case Direction.Backward:
                windDirection = Vector3.left;
                break;
            case Direction.Up:
                windDirection = Vector3.up;
                break;
            case Direction.Down:
                windDirection = Vector3.down;
                break;
            default:
                break;
        }
    }
    private void DetermineWindSource()
    {
        switch (direction)
        {
            case Direction.Froward:
                windSource = new Vector3(transform.position.x - area.size.x / 2.0f, transform.position.y, transform.position.z);
                break;
            case Direction.Backward:
                windSource = new Vector3(transform.position.x + area.size.x / 2.0f, transform.position.y, transform.position.z); 
                break;
            case Direction.Up:
                windSource = new Vector3(transform.position.x, transform.position.y - area.size.y / 2.0f, transform.position.z);
                break;
            case Direction.Down:
                windSource = new Vector3(transform.position.x, transform.position.y + area.size.y / 2.0f, transform.position.z);
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player)
        {
            var rigidbody = other.GetComponent<Rigidbody>();

            //updates the wind strength depending on how far the player is from the source
            var distanceFromSource = windSource - other.transform.position;
            windStrength = windSpeed / distanceFromSource.magnitude;

            if (player.currentMonster.Name == MonsterCharacter.MonsterName.Jullia)
            {
                rigidbody.AddForce(windDirection * windStrength / 15f, ForceMode.Force);
            }
            else
            {
                rigidbody.AddForce(windDirection * windStrength, ForceMode.Force);
            }
        }
    }

    private void OnDrawGizmos()
    {
        OnValidate();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(windSource, 1f);
        Gizmos.DrawWireCube(transform.position, area.size);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(windSource, transform.position);
    }
}

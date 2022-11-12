using UnityEngine;

public class Player : MonoBehaviour
{
    public string PlayerId;

    public Character Character;
    public bool IsDead => Character.CurrentHitpoints <= 0;

    private bool _deathHandled;

    //private StateManager StateManager => Character.StateManager;

    void Start()
    {
        if (Character == null)
        {
            Character = GetComponent<Character>();
        }
    }
    
    void Update()
    {
        Character.TargetMoveDirection = Input.GetAxis("Horizontal" + PlayerId);

        if (Input.GetButton("Jump" + PlayerId))
        {
            Character.ShouldJump = true;
        }
        else if (Input.GetButtonUp("Jump" + PlayerId)) // release jump for short hop
        {
            Character.ShouldEndJump = true;
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput _input;
    private IMovement _movement;

    private void Awake()
    {
        _input = GetComponent<IPlayerInput>();
        _movement = GetComponent<IMovement>();
    }

    private void Update()
    {
        //ˆÚ“®“ü—Í‚ğæ“¾‚µ‚ÄˆÚ“®ˆ—‚ğŒÄ‚Ño‚·
        _movement.Move(_input.MoveInput);
    }
}

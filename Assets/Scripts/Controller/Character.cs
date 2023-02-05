using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float CurrentHitpoints = 100;
    public float MaxHitpoints = 100;

    public float MoveSpeed = 5;
    public float JumpForce = 5;
    public float TerminalVelocity = 15;
    public float CoyoteTime = 0.1f;
    
    public LayerMask WallCollisionLayers;
    public LayerMask GroundCollisionLayers;
    public Collider2D WallCheckColliderLeft; // a trigger on the right side of the player collider
    public Collider2D WallCheckColliderRight; // a trigger on the right side of the player collider
    public Collider2D GroundCheckCollider; // a trigger on the bottom of the player collider
    public Collider2D SquishCheckCollider; // a trigger in the center of the player collider

    public MusicManager Music;
    public Animator Animator;

    [Tooltip("The height added from the origin to determine which character is higher to determine who wins collisions")]
    public float DamageCalculationHeight;
    public float DamageDealt = 1;

    public Bubble Bubble;
    public Player LovingPartner;
    public float BubblePushForce;

    public AudioSource AudioSource;
    public AudioClip JumpSound;
    public AudioClip DeathSound;

    public float TargetMoveDirection { get; set; }
    public bool ShouldJump { get; set; }
    public bool ShouldEndJump { get; set; }

    //public StateManager StateManager { get; set; }

    public Vector2 AdditionalMovementVector { get; set; }

    private bool _forceJump;
    private bool _jumping;

    private bool _previousIsGrounded;
    private bool _inCoyoteTime;

    private bool _bubbleEnabled;

    private Rigidbody2D _rigidbody;
    private ContactFilter2D _wallContactFilter;
    private ContactFilter2D _groundContactFilter;
    private bool _facingRight = true;

    private bool _needsAnimationSync = false;

    //public int CharacterLayer { get; set; }
    //public int AttackLayer { get; set; }
    //public int FallingLayer { get; set; }

    private void Start()
    {
        Music = FindObjectOfType<MusicManager>();

        if (Animator == null)
        {
            Animator = GetComponent<Animator>();
        }

        _wallContactFilter.useTriggers = false;
        _wallContactFilter.SetLayerMask(WallCollisionLayers);
        _wallContactFilter.useLayerMask = true;

        _groundContactFilter.useTriggers = false;
        _groundContactFilter.SetLayerMask(GroundCollisionLayers);
        _groundContactFilter.useLayerMask = true;
        
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = CalculateMovement();
        if (IsSquished())
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(name + " takes " + damage + " damage");

        CurrentHitpoints -= damage;
        if (CurrentHitpoints <= 0)
        {
            CurrentHitpoints = 0; // for display
            Die();
        }
    }

    public void Fall(bool fall)
    {
        if (fall)
        {
            //gameObject.layer = FallingLayer;
            _groundContactFilter.SetLayerMask(WallCollisionLayers); // TODO: This is kinda hacky, probably make a falling layer mask ? Feels extra
        }
        else
        {
            //gameObject.layer = CharacterLayer;
            _groundContactFilter.SetLayerMask(GroundCollisionLayers);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var otherCharacter = collision.collider.GetComponent<Character>();
        if (otherCharacter != null)
        {
            var myHeight = transform.position.y + DamageCalculationHeight;
            var otherHeight = otherCharacter.transform.position.y + otherCharacter.DamageCalculationHeight;
            if (myHeight > otherHeight)
            {
                otherCharacter.TakeDamage(DamageDealt);
                _forceJump = true;
            }
        }
    }

    private void Die()
    {
        Debug.Log("Died");
        // Make not collide with barriers but still collide with borders
        // Make float around randomly
        // Allow tapping jump to push you slightly towards the other player


        //foreach (var collider in GetComponents<Collider2D>())
        //{
        //    collider.isTrigger = true; // body should pass through colliders on death
        //}

        SetBubbleEnabled(true);

        if (DeathSound != null && AudioSource != null)
        {
            AudioSource.volume = 1;
            AudioSource.clip = DeathSound;
            AudioSource.Play();
        }
    }

    public void SetBubbleEnabled(bool enabled)
    {
        _bubbleEnabled = enabled;

        Bubble.gameObject.SetActive(enabled);
        GetComponent<Collider2D>().isTrigger = enabled;
        _rigidbody.gravityScale = enabled ? 0 : 1;
    }

    private void UpdateAnimation()
    {
        if (_needsAnimationSync)
        {
            _needsAnimationSync = false;

            var animationInfo = Animator.GetCurrentAnimatorClipInfo(0);
            var numFrames = animationInfo[0].clip.frameRate * animationInfo[0].clip.length; // use num frames to set the start position
            var currentState = Animator.GetCurrentAnimatorStateInfo(0).fullPathHash;

            // begin current animation at a position where the animation should be in sync with the beat
            Animator.Play(currentState, -1, 1 - (Music.BeatProgress / (numFrames / 4f))); // 4 frames per beat, always
        }

        var startFallAnimation = !_previousIsGrounded;
        var startDirtyAnimation = _bubbleEnabled;

        if (Animator.GetBool("Dirty") != startDirtyAnimation || Animator.GetBool("Falling") != startFallAnimation)
        {
            _needsAnimationSync = true; // sync on next update when an animation state changes
        }

        Animator.SetBool("Dirty", _bubbleEnabled);
        //Animator.SetBool("Running", startRunAnimation);
        Animator.SetBool("Falling", startFallAnimation);
    }

    private Vector2 CalculateMovement()
    {
        if (_bubbleEnabled)
        {
            return CalculateBubbleMovement();
        }
            
        return new Vector2(CalculateHorizontalMovement(), CalculateVerticalMovement()) + AdditionalMovementVector;
    }

    private Vector2 CalculateBubbleMovement()
    {
        if (ShouldJump || _forceJump)
        {
            var distance = LovingPartner.transform.position - transform.position;
            var direction = distance.normalized;

            ShouldJump = false;
            return direction * BubblePushForce;
        }

        return _rigidbody.velocity.normalized * _rigidbody.velocity.magnitude * 0.95f;
    }
    
    private float CalculateVerticalMovement()
    {
        SetGroundedState();

        var verticalMovement = _rigidbody.velocity.y;

        if (ShouldJump || _forceJump)
        {
            // the !_jumping check should only matter for a single fixed update, not important
            if ((!_jumping && (_previousIsGrounded || _inCoyoteTime)) || _forceJump)
            {
                _jumping = true;
                verticalMovement = JumpForce;

                if (JumpSound != null && AudioSource != null)
                {
                    AudioSource.clip = JumpSound;
                    AudioSource.Play();
                }
            }
            ShouldJump = false;
            _forceJump = false;
        }

        if (ShouldEndJump)
        {
            if (verticalMovement > 0)
            {
                verticalMovement *= 0.5f;
            }
            ShouldEndJump = false;
        }

        if (Mathf.Abs(verticalMovement) > TerminalVelocity)
        {
            verticalMovement = verticalMovement > 0 ? TerminalVelocity : -TerminalVelocity;
        }

        return verticalMovement;
    }

    private float CalculateHorizontalMovement()
    {
        float horizontalMovement = TargetMoveDirection * MoveSpeed;
        
        if (horizontalMovement > 0)
        {
            //Flip(true);
        }
        else if (horizontalMovement < 0)
        {
            //Flip(false);
        }
        
        if (IsOnWall() && (ShouldJump || !IsGrounded()))
        {
            // stop moving on contact with a wall so we don't stick
            // in the case where we are on a wall and grounded, allow movement UNLESS we are in a state where we should jump
            // if we don't include the jump clause, jumping while running into a wall is slower because we stick to the wall until we leave the ground
            horizontalMovement = 0;
        }

        return horizontalMovement;
    }

    private void Flip(bool right)
    {
        if (_facingRight != right)
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        _facingRight = right;
    }

    private bool IsOnWall()
    {
        int numCollisions;

        if (TargetMoveDirection > 0) // moving right
        {
            numCollisions = Physics2D.OverlapCollider(WallCheckColliderRight, _wallContactFilter, new Collider2D[1]);
        }
        else if (TargetMoveDirection < 0) // moving left
        {
            numCollisions = Physics2D.OverlapCollider(WallCheckColliderLeft, _wallContactFilter, new Collider2D[1]);
        }
        else // not moving
        {
            numCollisions = Physics2D.OverlapCollider(WallCheckColliderRight, _wallContactFilter, new Collider2D[1])
                + Physics2D.OverlapCollider(WallCheckColliderLeft, _wallContactFilter, new Collider2D[1]);
        }

        return numCollisions != 0;
    }

    private bool IsSquished()
    {
        var numCollisions = Physics2D.OverlapCollider(SquishCheckCollider, _wallContactFilter, new Collider2D[1]);

        return numCollisions != 0;
    }

    private void SetGroundedState()
    {
        var isGrounded = IsGrounded();

        if (_jumping && !_previousIsGrounded && isGrounded)
        {
            // if we touch the ground after jumping
            _jumping = false;
        }

        if (_previousIsGrounded && !isGrounded && !_jumping)
        {
            // if we just fell off a cliff
            StartCoroutine(RunCoyoteTime());
        }

        _previousIsGrounded = isGrounded;
    }

    private bool IsGrounded()
    {
        var numCollisions = Physics2D.OverlapCollider(GroundCheckCollider, _groundContactFilter, new Collider2D[1]);

        return numCollisions != 0 && Mathf.Abs(_rigidbody.velocity.y) < 1;// && !falling; // TODO: This is all a little screwy
        // this will not work for rising platforms like elevators unless they move slow
        // (maybe it will work if we do the whole "child" model and use relative velocity instead?)
    }

    private IEnumerator RunCoyoteTime()
    {
        _inCoyoteTime = true;
        yield return new WaitForSeconds(CoyoteTime);

        _inCoyoteTime = false;
    }
}

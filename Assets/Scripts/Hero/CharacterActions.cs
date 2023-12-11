using System;
using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class CharacterActions : Resettable
{
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [SerializeField] private float dashForce = 300f; // Amount of force added when the player dashes.

    [SerializeField] private float movementForce = 5f; // The force applied to the character to move them forward.
    private float movementSmoothing = .05f; // How much to smooth out the movement

    [SerializeField] private float
        dashRecoveryTime = 3f; // How long it takes for the character to get back to the start and be able to move again

    public float MovementDirection { get; set; }

    private Vector3 velocity = Vector3.zero;

    private bool IsFacingRight
    {
        get => transform.rotation.eulerAngles.y == 0;
        set
        {
            if (!value)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private Rigidbody2D rb;

    public static event Action OnLandEvent, OnDashEvent, OnJumpEvent, OnJumpFallEvent, OnDeathEvent;
    public static event Action<float> DashRecoveryUpdate;

    private Feet feet;
    private Sword sword;

    private Vector3 defaultPosition;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        feet = GetComponentInChildren<Feet>();
        sword = GetComponentInChildren<Sword>();
        ToggleSword(false);
        SubscribeToInputEvents();
        defaultPosition = transform.position;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        SubscribeToInputEvents(false);
    }

    private void SubscribeToInputEvents(bool subscribeOrUnsubscribe = true)
    {
        if (subscribeOrUnsubscribe)
        {
            InputManager.OnJumpPressed += Jump;
            InputManager.OnDashPressed += Dash;
        }
        else
        {
            InputManager.OnJumpPressed -= Jump;
            InputManager.OnDashPressed -= Dash;
        }
    }

private float verticalSpeed = 0;
    private bool wasGrounded = true;

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(movementForce, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
            //MovementEvent?.Invoke(rb.velocity.x);


            //if we started falling in the jump, start the falling animation
            if (!feet.Grounded)
            {
                if (verticalSpeed > 0 && rb.velocity.y < 0)
                {
                    OnJumpFallEvent?.Invoke();
                }

                verticalSpeed = rb.velocity.y;
            }

            if (feet.Grounded && !wasGrounded)
            {
                OnLandEvent?.Invoke();
                wasGrounded = true;
            }
            else
                wasGrounded = feet.Grounded;
        }
    }

    public void Jump()
    {
        // If the player should jump...
        if (feet.Grounded)
        {
            // Add a vertical force to the player.
            //m_Grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
            OnJumpEvent?.Invoke();
        }
    }

    private bool isDashAvailable = true;

    private bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            if (!value)
            {
                SubscribeToInputEvents(false);
                GetComponent<Collider2D>().excludeLayers=LayerMask.GetMask("Enemy");
                OnDeathEvent?.Invoke();
            }
            else
            {
                SubscribeToInputEvents(true);
                GetComponent<Collider2D>().excludeLayers = 0;
                transform.position = defaultPosition;
                ToggleSword(false);
                
            }

            _isAlive = value;
        }
    }

    public void Dash()
    {
        // If the player should dash...
        if (feet.Grounded && isDashAvailable)
        {
            // Add a horizontal force to the player.
            rb.AddForce(new Vector2(dashForce*(IsFacingRight?1:-1), 0f));
            OnDashEvent?.Invoke();
            StartCoroutine(DashRecoveryCoroutine());
        }
    }

    private IEnumerator DashRecoveryCoroutine()
    {
        isDashAvailable = false;
        float dashRecoveryProgress = 0f;
        while (dashRecoveryProgress<10*dashRecoveryTime)
        {
            dashRecoveryProgress += 1;
            yield return new WaitForSeconds(.1f);
        }

        isDashAvailable = true;
    }

    public void ToggleSword(bool? b = null)
    {
        sword.gameObject.SetActive(b ?? (!sword.gameObject.activeSelf));
    }

    protected override void Reset()
    {
        if(!IsAlive)
            IsAlive = true;
        else
        {
            transform.position = defaultPosition;
            ToggleSword(false);
        }

        rb.velocity = Vector2.zero;
        wasGrounded = true;
        isDashAvailable = true;
    }
}
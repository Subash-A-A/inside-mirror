using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private int _extraJumps = 2;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _maxSpeed = 100f;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private LayerMask _whatIsGround;

    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _isJumping;
    private bool _isGrounded;
    private int _jumpCount = 0;
    private bool _isSwitching;
    private bool _isPressingRMB;
    private bool _isPressingLMB;

    private MenuControls _menuControls;

    public bool _isControlling = false;
    public bool _canSwitch = true;
    public bool _isShielded = false;
    public bool _canShield = true;
    public bool _ultimateReady = true;
    public bool _canShoot = true;

    public bool _ultimateInUse = false;
    public bool _selfAbilityInUse = false;

    [Range(0f, 1f)]
    public float _goodEvil = 0f;
    

    private void Start()
    {
        _menuControls = FindObjectOfType<MenuControls>();

        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _anim.SetFloat("goodEvil", _goodEvil);
    }

    private void Update()
    {
        PlayerInput();
        
        Jump();
        Run();
        
        GroundCheck();
        PlayerAnimations();

        if (_isSwitching && _canSwitch)
        {
            _canSwitch = false;
            Switch();
        }

        if(_isPressingRMB && _isControlling && _ultimateReady)
        {   
            if(_goodEvil == 0f)
            {
                _isShielded = true;
                _canShield = false;
            }
            else
            {
                _canShoot = false;
            }

            _ultimateReady = false;
            _ultimateInUse = true;
            UseUltimate();
        }
        else if (_isPressingLMB && _isControlling)
        {
            _selfAbilityInUse = true;
            if (_goodEvil == 0f && !_isShielded && _canShield)
            {
                _isShielded = true;
                UseSelfAbility();
            }
            else if(_goodEvil == 1 && _canShoot)
            {
                _canShoot = false;
                UseSelfAbility();
            }
        }
    }

    private void PlayerInput()
    {
        if (_menuControls.canListenInput)
        {
            _isJumping = Input.GetKeyDown(KeyCode.Space);
            _isSwitching = Input.GetKeyDown(KeyCode.LeftShift);
            _isPressingRMB = Input.GetMouseButtonDown(1);
            _isPressingLMB = Input.GetMouseButtonDown(0);
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _jumpCount = 0;
        }
        if (_isJumping && _jumpCount < _extraJumps)
        {
            _rb.AddForce(_jumpForce * _rb.gravityScale * Vector2.up, ForceMode2D.Impulse);
            _jumpCount++;
        }
    }

    private void Run()
    {
        _rb.AddForce(Vector2.right * _moveSpeed, ForceMode2D.Force);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);
    }

    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckTransform.position, 0.1f, _whatIsGround);
    }

    private void PlayerAnimations()
    {   
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetFloat("jumpState", _jumpCount);
    }

    private void Switch()
    {
        if (_isControlling)
        {
            _anim.SetTrigger("switch");
        }
        _isControlling = !_isControlling;
    }

    private void UseSelfAbility()
    {
        _anim.SetTrigger("SelfAbility");
    }

    private void UseUltimate()
    {
        _anim.SetTrigger("Ultimate");
    }
}

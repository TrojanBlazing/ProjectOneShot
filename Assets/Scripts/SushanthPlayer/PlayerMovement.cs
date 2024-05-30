using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool CanMove { get; private set; }
    bool isSprinting => canSprint && Input.GetKey(sprintKey);
    bool shouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;
    bool shouldCrouch => Input.GetKeyDown(crouchKey) && characterController.isGrounded && !inCrouchingAnimation;

    [Header("Functional options")]
    [SerializeField] bool canSprint = true;
    [SerializeField] bool canJump = true;
    [SerializeField] bool canCrouch = true;
    [SerializeField] bool canHeadBob = true;
    [SerializeField] bool canZoom = true;

    [Header("Controls")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] KeyCode zoomKey = KeyCode.Mouse1;

    [Header("Jump Parameters")]
    [SerializeField] float jumpForce = 8f;

    [Header("Zoom Parameter")]
    [SerializeField] float timeToZoom = 0.3f;
    float zoomFOV = 30f;
    float defaultFOV;
    Coroutine ZoomCoroutine;

    [Header("Movement")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float crouchSpeed = 1.5f;
    [SerializeField] float gravity = 30f;

    [Header("Look Parameter")]
    [SerializeField, Range(1, 10)] float lookSpeedX;
    [SerializeField, Range(1, 10)] float lookSpeedY;
    [SerializeField, Range(1, 180)] float upperLookLimit = 80f;
    [SerializeField, Range(1, 180)] float lowerLookLimit = 80f;

    [Header("Crouch Parameters")]
    [SerializeField] float crouchHeight = 0.5f;
    [SerializeField] float standHeight = 2f;
    [SerializeField] float timeToCrouch = 0.25f;
    [SerializeField] Vector3 crouchCentre = new Vector3(0, 0.5f, 0);
    [SerializeField] Vector3 standingCentre = new Vector3(0, 0, 0);
    bool isCrouching;
    bool inCrouchingAnimation;

    [Header("HeadBob Parameter")]
    [SerializeField] float walkBobSpeed = 14f;
    [SerializeField] float walkBobAmt = 0.05f;
    [SerializeField] float sprintBobSpeed = 18f;
    [SerializeField] float sprintBobAmt = 0.1f;
    [SerializeField] float crouchBobSpeed = 9f;
    [SerializeField] float crouchBobAmt = 0.025f;
    float defaultYPos = 0f;
    float timer;

    Camera playerCam;
    CharacterController characterController;

    Vector3 moveDirection;
    Vector2 currentInput;

    float rotationX;

    private void Awake()
    {
        playerCam = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CanMove = true;
        defaultFOV = playerCam.fieldOfView;
        defaultYPos = playerCam.transform.localPosition.y;
    }

    void Update()
    {
        if (CanMove)
        {
            HandleMovement();
            HandleMouse();


            if (canJump)
            {
                HandleJump();
            }

            if (canCrouch)
            {
                HandleCrouch();
                if (canHeadBob)
                {
                    HeadBob();
                }
            }

            if (canZoom)
            {
                HandleZoom();
            }
            ApplyMovement();

        }

    }

    void HandleZoom()
    {
        if (Input.GetKeyDown(zoomKey))
        {
            if(ZoomCoroutine!= null)
            {
                StopCoroutine(ZoomCoroutine);
                ZoomCoroutine = null;
            }

            ZoomCoroutine = StartCoroutine(ToggleZoom(true));
        }

        if (Input.GetKeyUp(zoomKey))
        {
            if (ZoomCoroutine != null)
            {
                StopCoroutine(ZoomCoroutine);
                ZoomCoroutine = null;
            }

            ZoomCoroutine = StartCoroutine(ToggleZoom(false));
        }
    }
        void HeadBob()
        {
            if(!characterController.isGrounded)
            {
                return;
            }

            if (Mathf.Abs(moveDirection.x) > 0.1f||Mathf.Abs(moveDirection.z) > 0.1f)
            {
                timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : isSprinting ? sprintBobSpeed : walkBobSpeed);
                playerCam.transform.localPosition = new Vector3(playerCam.transform.localPosition.x , 
                    defaultYPos + Mathf.Sin(timer) * (isCrouching? crouchBobAmt : isSprinting? sprintBobAmt : walkBobAmt)
                    , playerCam.transform.localPosition.z
                    );
            }

        }
        void HandleMovement()
        {
            currentInput = new Vector2((isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

            float moveDirectionY = moveDirection.y;
            moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
            moveDirection.y = moveDirectionY;
        }

        void HandleJump()
        {
            if (shouldJump)
            {
                moveDirection.y = jumpForce;
            }
        }

        void HandleCrouch()
        {
            if (shouldCrouch)
            {
                StartCoroutine(StartCrouch());
            }
        }
         #region Coroutines
    // Coroutines
    IEnumerator StartCrouch()
        {
            if (isCrouching && Physics.Raycast(playerCam.transform.position, Vector3.up, 1f))
            {
                yield break;
            }


            inCrouchingAnimation = true;

            float timeElapsed = 0f;
            float targetHeight = isCrouching ? standHeight : crouchHeight;
            float currentHeight = characterController.height;
            Vector3 targetCentre = isCrouching ? standingCentre : crouchCentre;
            Vector3 currentCentre = characterController.center;
            Debug.Log(targetCentre);
            while (timeElapsed < timeToCrouch)
            {
                characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
                characterController.center = Vector3.Lerp(currentCentre, targetCentre, timeElapsed / timeToCrouch);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            characterController.height = targetHeight;
            characterController.center = targetCentre;

            isCrouching = !isCrouching;

            inCrouchingAnimation = false;
        }

    IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : defaultFOV;
        float startingFOV = playerCam.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed <timeToZoom) 
        {
        playerCam.fieldOfView = Mathf.Lerp(startingFOV , targetFOV , timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCam.fieldOfView = targetFOV;

        ZoomCoroutine = null;
    }

    #endregion
    void HandleMouse()
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
        }

        void ApplyMovement()
        {

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);

        }

    }

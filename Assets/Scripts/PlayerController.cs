using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variable Declaration
    [SerializeField] private bool isPlayer1;

    [Header("Jump")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    private bool isJumpCooldown;
    #endregion

    #region Component Declaration
    private Rigidbody2D rb;
    Keyboard kb;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Initialize Variables
        isJumpCooldown = false;
        #endregion

        #region Assign Components
        rb = gameObject.GetComponent<Rigidbody2D>();
        kb = Keyboard.current;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();

        #region Player Actions
        if ((isPlayer1? kb.qKey.wasPressedThisFrame : kb.oKey.wasPressedThisFrame)&&!isJumpCooldown)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(transform.up*jumpForce, ForceMode2D.Impulse);

            StartCoroutine(JumpCooldown());
        }
        #endregion
    }

    private void ApplyGravity()
    {
        rb.velocity += new Vector2(0, -gravity)*Time.deltaTime;
    }

    private IEnumerator JumpCooldown()
    {
        isJumpCooldown = true;
        yield return new WaitForSeconds(jumpCooldown);
        isJumpCooldown = false;
    }
}

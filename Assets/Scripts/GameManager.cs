using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Speed Settings")]
    public float baseSpeed = 5f;
    public float speedPerCoin = 0.2f;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int coinsCollected;

    [Header("Dash Settings")]
    public float forwardMultiplier = 2f;
    public float backwardMultiplier = 0.5f;
    public float dashDuration = 1f;
    public float dashCooldown = 2f;
    private bool canDash = true;

    [Header("UI Settings")]
    public Text coinsUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        currentSpeed = baseSpeed;
    }

    public void OnCoinCollected()
    {
        coinsCollected++;
        coinsUI.text =  "Coins: " + coinsCollected;
        currentSpeed = baseSpeed + coinsCollected * speedPerCoin;
    }

    public void StartDashForward()
    {
        if (!canDash) return;
        StartCoroutine(DashRoutine(forwardMultiplier));
    }

    public void StartDashBackward()
    {
        if (!canDash) return;
        StartCoroutine(DashRoutine(backwardMultiplier));
    }

    private IEnumerator DashRoutine(float multiplier)
    {
        canDash = false;
        float originalSpeed = currentSpeed;
        currentSpeed = originalSpeed * multiplier;

        yield return new WaitForSeconds(dashDuration);

        // restore based on coins
        currentSpeed = baseSpeed + coinsCollected * speedPerCoin;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
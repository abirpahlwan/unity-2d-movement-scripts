using UnityEngine;

/// <summary>
/// ItemCollector detects 2D trigger collisions with collectible objects (gems, speed boosts, coins).
/// </summary>
public class ItemCollector : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffectPrefab;

    private AudioSource audioSource;
    private PlayerController playerController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedBoost"))
        {
            if (playerController != null)
            {
                playerController.SetMoveSpeed(9f);
                Debug.Log("[ItemCollector] Speed Boost Activated!");
            }
            CollectItem(collision.gameObject);
        }
        else if (collision.CompareTag("Collectible"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(10);
            }
            CollectItem(collision.gameObject);
        }
    }

    private void CollectItem(GameObject item)
    {
        if (pickupSound != null && audioSource != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        if (pickupEffectPrefab != null)
        {
            Instantiate(pickupEffectPrefab, item.transform.position, Quaternion.identity);
        }
        Destroy(item);
    }
}
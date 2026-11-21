using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject deathPanel; // child object, hidden until the player dies

    void Awake()
    {
        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    void OnEnable()
    {
        player.Died += Show;
    }

    void OnDisable()
    {
        player.Died -= Show;
    }

    private void Show()
    {
        deathPanel.SetActive(true);
    }
}

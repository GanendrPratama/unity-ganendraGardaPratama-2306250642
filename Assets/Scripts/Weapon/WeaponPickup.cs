using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
   [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    void Awake()
    {
        weapon = Instantiate(weaponHolder);
    }

    void Start() 
    {
        if (weapon != null) {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("PLAYER IS ENTERING ME");
            weapon.transform.SetParent(other.transform);
            weapon.transform.position = other.transform.position;
            TurnVisual(true);
        } else {
            Debug.Log("NOT A PLAYER");
        }
    }

    void TurnVisual(bool on)
    {
        if (on) {
            weapon.gameObject.SetActive(on);
            gameObject.SetActive(!on);
        } else {
            weapon.gameObject.SetActive(false);
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        if (on) {
            weapon.gameObject.SetActive(on);
        } else {
            weapon.gameObject.SetActive(false);
        }
    }
}

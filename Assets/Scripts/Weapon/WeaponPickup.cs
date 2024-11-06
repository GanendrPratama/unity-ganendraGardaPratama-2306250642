using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
   [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    void Awake()
    {
        weapon = weaponHolder;
    }

    void Start() 
    {
        if (weapon != null) {
            Destroy(this);
        } else {
            DontDestroyOnLoad(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("PLAYER IS ENTERING ME");
            weapon.transform.SetParent(other.transform);
        }
    }

    void TurnVisual(bool on)
    {

    }

    void TurnVisual(bool on, Weapon weapon)
    {

    }
}

using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Weapons weapon;
    public PoolScript pool;
    public int currentWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        UseWeapon();
        weapon = GetComponentInChildren<Weapons>();
        Debug.Log(weapon.range);
    }

    // Update is called once per frame
    void Update()
    {
        int prevWeapon = currentWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(currentWeapon >= transform.childCount - 1)
                currentWeapon = 0;
            else
                currentWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(currentWeapon <= 0)
                currentWeapon = transform.childCount - 1;
            else
                currentWeapon--;
        }

        if (prevWeapon != currentWeapon)
        {
            UseWeapon();
        }
    }

    void UseWeapon()
    {
        int index = 0;
        foreach(Transform weapon in transform)
        {
            if(index == currentWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            index++;
        }
        weapon = GetComponentInChildren<Weapons>();
    }

    public int getDamage()
    {
        return weapon.damage;
    }

    public float getRange()
    {
        return weapon.range;
    }
}

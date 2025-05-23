using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    public bool[] weaponUnlockStatus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Assuming 10 weapons max
            weaponUnlockStatus = new bool[10];
        }
        else
        {
            Destroy(gameObject);
        }

        LoadData();
    }

    public void UnlockWeapon(int index)
    {
        if (index >= 0 && index < weaponUnlockStatus.Length)
        {
            weaponUnlockStatus[index] = true;
        }
    }

    public bool IsWeaponUnlocked(int index)
    {
        return index >= 0 && index < weaponUnlockStatus.Length && weaponUnlockStatus[index];
    }

    public void SaveData()
    {
        for (int i = 0; i < weaponUnlockStatus.Length; i++)
        {
            PlayerPrefs.SetInt("WeaponUnlocked" + i, weaponUnlockStatus[i] ? 1 : 0);
        }
    }

    public void LoadData()
    {
        for (int i = 0; i < weaponUnlockStatus.Length; i++)
        {
            weaponUnlockStatus[i] = PlayerPrefs.GetInt("WeaponUnlocked" + i, 0) == 1;
        }
    }

    public void UnlockWeaponFromShop(int weaponIndex)
    {
        Game_Manager.Instance.UnlockWeapon(weaponIndex);
    }
}

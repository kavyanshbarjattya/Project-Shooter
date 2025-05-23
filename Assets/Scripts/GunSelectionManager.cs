using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunSelectionManager : MonoBehaviour
{
    public List<int> selectedGunIndices = new();

    public void SelectGun(int index)
    {
        if (selectedGunIndices.Contains(index))
        {
            selectedGunIndices.Remove(index);
        }
        else if (selectedGunIndices.Count < 2)
        {
            selectedGunIndices.Add(index);
        }
        else
        {
            Debug.Log("Only 2 guns can be selected.");
        }
    }

    public void StartGame()
    {
        if (selectedGunIndices.Count < 1)
        {
            Debug.Log("Select at least one gun.");
            return;
        }

        PlayerPrefs.SetInt("SelectedGunCount", selectedGunIndices.Count);
        for (int i = 0; i < selectedGunIndices.Count; i++)
        {
            PlayerPrefs.SetInt("SelectedGun_" + i, selectedGunIndices[i]);
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}

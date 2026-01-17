using UnityEngine;
using UnityEngine.UI;

public class TrashPile : MonoBehaviour
{
    [SerializeField] Image healthSlider;
    
    public int trashAmount { get; private set; } = 100;


    public void StealTrash(int amount)
    {
        trashAmount -= amount;
        if (trashAmount <= 0)
        {
            trashAmount = 0;
        }

        healthSlider.fillAmount = trashAmount / 100f;
    }

    public void ReturnTrash(int amount)
    {
        trashAmount += amount;
        healthSlider.fillAmount = trashAmount / 100f;
    }
}

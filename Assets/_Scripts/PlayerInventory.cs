using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int numberOfAnchors;

    public void AddAnchor()
    {
        numberOfAnchors++;
    }

    public bool RemoveAnchor()
    {
        if (numberOfAnchors > 0)
        {
            numberOfAnchors--;
            return true;
        }
        return false;
    }
}
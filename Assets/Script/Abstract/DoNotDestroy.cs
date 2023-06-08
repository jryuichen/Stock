using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    // Game objects with this script will not be destroyed when changing scenes!
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
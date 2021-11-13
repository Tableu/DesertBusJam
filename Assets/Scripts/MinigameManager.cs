using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        NetworkManager.Instance.InstantiatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

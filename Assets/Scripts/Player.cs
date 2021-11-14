using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class Player : PlayerBehavior
{
    public float speed = 5.0f;

    private MinigameManager _minigameManager;
    // Start is called before the first frame update
    void Start()
    {
        _minigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject == null)
            return;
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }
        
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;
        
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }
}

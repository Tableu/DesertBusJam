using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class Cube : CubeBehavior
{
    /// <summary>
    /// The speed that the cube will move by when the user presses a
    /// Horizontal or Vertical mapped key
    /// </summary>
    public float speed = 5.0f;

    private void Update()
    {
        // If unity's Update() runs, before the object is
        // instantiated in the network, then simply don't
        // continue, otherwise a bug/error will happen.
        // 
        // Unity's Update() running, before this object is instantiated
        // on the network is **very** rare, but better be safe 100%
        if (networkObject == null)
            return;
		
        // If we are not the owner of this network object then we should
        // move this cube to the position/rotation dictated by the owner
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.Position;
            transform.rotation = networkObject.Rotation;
            return;
        }

        // Let the owner move the cube around with the arrow keys
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;

        // If we are the owner of the object we should send the new position
        // and rotation across the network for receivers to move to in the above code
        networkObject.Position = transform.position;
        networkObject.Rotation = transform.rotation;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually
    }
}

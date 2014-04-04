using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlaceOnObjectBelow : MonoBehaviour
{
	public LayerMask surfaceLayers = 1;  // LayerMask == 1 is the "Default" layer
	public bool resetOnEnable = false;
	private bool initialized = false;


	#region Cached Component Properties

	private Transform mCachedTransform;
	private Transform cachedTransform
	{
		get
		{
			if(mCachedTransform == null) {
				mCachedTransform = GetComponent<Transform>();
			}
			return mCachedTransform;
		}
	}

	private Collider mCachedCollider;
	public Collider cachedCollider
	{
		get
		{
			if(mCachedCollider == null) {
				mCachedCollider = GetComponent<Collider>();
			}
			return mCachedCollider;
		}
	}

	#endregion

	#region MonoBehaviour Overrides

	void OnEnable()
	{
		initialized = !resetOnEnable;
	}

	void FixedUpdate()
	{
		if(!initialized) {
			Initialize();
			initialized = true;
		}
	}

	#endregion

	[ContextMenu("Place Now")]
	void Initialize()
	{
		RaycastHit hit;
		if(Physics.Raycast(cachedTransform.position, Vector3.down, out hit, float.MaxValue, surfaceLayers.value)) {

			// Align the up vector to the surface normal
			cachedTransform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

			// Place the rotated object on the surface
			Vector3 pos = cachedTransform.position;
			pos.y = hit.point.y + cachedCollider.bounds.extents.y;
			cachedTransform.position = pos;
		}
	}
}

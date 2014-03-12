using UnityEngine;
using System.Collections;

namespace MartianCraft.Common
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	public class PlaceOnObjectBelow : MCBehaviour
	{
		public LayerMask surfaceLayers = 1;  // LayerMask == 1 is the "Default" layer
		public bool resetOnEnable = false;
		private bool initialized = false;
		private bool wasKinematic;

		void OnEnable()
		{
			if(!resetOnEnable) {
				wasKinematic = cachedRigidbody.isKinematic;
				cachedRigidbody.isKinematic = true;
				initialized = false;
			}
		}

		void FixedUpdate()
		{
			if(!initialized) {
				Initialize();
				initialized = true;
				cachedRigidbody.isKinematic = wasKinematic;
			}
		}

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
}
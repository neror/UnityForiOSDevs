using UnityEngine;
using System.Collections;

namespace MartianCraft.Common
{
	public class MCBehaviour : MonoBehaviour
	{
		#region Cached Component Properties

		private Transform mCachedTransform;
		public Transform cachedTransform
		{
			get
			{
				if(mCachedTransform == null) {
					mCachedTransform = GetComponent<Transform>();
				}
				return mCachedTransform;
			}
		}
		
		private Camera mCachedMainCamera;
		public Camera cachedMainCamera
		{
			get
			{
				if(mCachedMainCamera == null) {
					mCachedMainCamera = Camera.main;
				}
				return mCachedMainCamera;
			}
		}
		
		private Rigidbody mCachedRigidbody;
		public Rigidbody cachedRigidbody
		{
			get
			{
				if(mCachedRigidbody == null) {
					mCachedRigidbody = GetComponent<Rigidbody>();
				}
				return mCachedRigidbody;
			}
		}
		
		private Renderer mCachedRenderer;
		public Renderer cachedRenderer
		{
			get
			{
				if(mCachedRenderer == null) {
					mCachedRenderer = GetComponent<Renderer>();
				}
				return mCachedRenderer;
			}
		}

		private Rigidbody2D mCachedRigidbody2D;
		public Rigidbody2D cachedRigidbody2D
		{
			get
			{
				if(mCachedRigidbody2D == null) {
					mCachedRigidbody2D = GetComponent<Rigidbody2D>();
				}
				return mCachedRigidbody2D;
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
	}

}
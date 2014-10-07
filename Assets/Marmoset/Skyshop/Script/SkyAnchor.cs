﻿// Marmoset Skyshop
// Copyright 2014 Marmoset LLC
// http://marmoset.co

//WARNING: causes material property issues pre 4.5
#define USE_PROPERTY_BLOCKS

using UnityEngine;
//using UnityEditor;
using System.Collections;

using System;

namespace mset {
	public class SkyAnchor : MonoBehaviour {
		public enum AnchorBindType {Center, Offset, TargetTransform, TargetSky};
		public AnchorBindType BindType = AnchorBindType.Center;

		public Transform	AnchorTransform = null;
		public Vector3		AnchorOffset = Vector3.zero;
		public mset.Sky		AnchorSky = null;

		public mset.SkyApplicator	CurrentApplicator = null;
		public mset.Sky 			CurrentSky { 
			get { return Blender.CurrentSky; } 
		}

		public float BlendTime {
			get { return Blender.BlendTime; }
			set { Blender.BlendTime = value; }
		}


		//true if this anchor is assigned a local sky and should be calling Blender.Apply()
		//false if using the global sky and blender in SkyManager
		[SerializeField]
		private bool HasLocalSky = false;

		//true if gameObject has moved or needs to research applicators
		public bool HasChanged = true;

		[SerializeField]
		private mset.SkyBlender Blender = new mset.SkyBlender();
		private Vector3 LastPosition = Vector3.zero;

		public Material[] materials = null;

		// Use this for initialization
		void Start() {
			if(BindType != AnchorBindType.TargetSky) {
				#if USE_PROPERTY_BLOCKS
				//HACK: clear the property block for this renderer, good catch-all for old data
				renderer.SetPropertyBlock(new MaterialPropertyBlock());
				#endif

				//instantly register and hook up skies to this anchor on creation
				mset.SkyManager skymgr = mset.SkyManager.Get();
				skymgr.RegisterNewRenderer(renderer);
				skymgr.ApplyCorrectSky(renderer);
				BlendTime = skymgr.LocalBlendTime;
				if(Blender.CurrentSky == null) Blender.SnapToSky(skymgr.GlobalSky);
			}
			materials = renderer.materials;
			LastPosition = transform.position;
			HasChanged = true;
		}

		private void LateUpdate() {
			//direct link to a sky
			if(BindType == AnchorBindType.TargetSky) {
				HasChanged = AnchorSky != Blender.CurrentSky;
			}
			// use a third-party transform for anchor checks
			else if(BindType == AnchorBindType.TargetTransform) {
				if(AnchorTransform) {
					if(AnchorTransform.position.x != LastPosition.x ||
					   AnchorTransform.position.y != LastPosition.y ||
					   AnchorTransform.position.z != LastPosition.z) {
						HasChanged = true;
						LastPosition = AnchorTransform.position;
					}
				}
			}
			else {
				if(LastPosition.x != transform.position.x ||
				   LastPosition.y != transform.position.y ||
				   LastPosition.z != transform.position.z) {
					HasChanged = true;
					LastPosition = transform.position;
				}
			}
			Apply();
		}

		//Call this whenever the material list on a renderer has changed (added materials, changed material references, etc).
		public void UpdateMaterials() {
			materials = renderer.materials;
		}

		public void SnapToSky(mset.Sky nusky) {
			if(nusky == null) return;
			if(BindType == AnchorBindType.TargetSky) return;			
			Blender.SnapToSky(nusky);
			HasLocalSky = true;
		}
		public void BlendToSky(mset.Sky nusky) {
			if(nusky == null) return;
			//ignore if swaps if we are glued to a specific sky
			if(BindType == AnchorBindType.TargetSky) return;
			Blender.BlendToSky(nusky);
			HasLocalSky = true;
		}

		public void SnapToGlobalSky(mset.Sky nusky) {
			SnapToSky(nusky);
			HasLocalSky = false;
		}
		public void BlendToGlobalSky(mset.Sky nusky) {
			if(HasLocalSky) BlendToSky(nusky);
			HasLocalSky = false;
		}

		public void Apply() {
			if(BindType == AnchorBindType.TargetSky) {
				//we don't want to check for null skies every frame for every object but for
				//targeted skies, we do a global sky backup here
				if(AnchorSky)	Blender.SnapToSky(AnchorSky);
				else			Blender.SnapToSky(SkyManager.Get().GlobalSky);
				Blender.Apply(renderer, materials);
			}
			else if(HasLocalSky || Blender.IsBlending) {
				Blender.Apply(renderer, materials);
			}
		}

		//cached in anchors for memory allocation reasons
		//private Vector3 _center;
		public void GetCenter(ref Vector3 _center) {
			_center.x = transform.position.x;
			_center.y = transform.position.y;
			_center.z = transform.position.z;

			switch(BindType) {
			case AnchorBindType.TargetTransform:
				if(AnchorTransform) {
					_center.x = AnchorTransform.position.x;
					_center.y = AnchorTransform.position.y;
					_center.z = AnchorTransform.position.z;
				}
				break;
			case AnchorBindType.Center:
				_center.x = renderer.bounds.center.x;
				_center.y = renderer.bounds.center.y;
				_center.z = renderer.bounds.center.z;
				break;
			case AnchorBindType.Offset:
				Vector3 p = transform.localToWorldMatrix.MultiplyPoint3x4(this.AnchorOffset);
				_center.x = p.x;
				_center.y = p.y;
				_center.z = p.z;
				break;
			case AnchorBindType.TargetSky:
				if(AnchorSky) {
					_center.x = AnchorSky.transform.position.x;
					_center.y = AnchorSky.transform.position.y;
					_center.z = AnchorSky.transform.position.z;
				}
				break;
			};
			//return _center;
		}

	#if UNITY_EDITOR
		public void OnDrawGizmosSelected() {
			Gizmos.color = Color.cyan;
			Vector3 center = Vector3.zero;
			GetCenter(ref center);
			Gizmos.DrawLine(transform.position, center);
			if(BindType == AnchorBindType.Offset) {
				Gizmos.color = new Color(0f,4f,4f);
				Gizmos.DrawSphere(center, 0.15f);
			}
		}
	#endif
	}
}

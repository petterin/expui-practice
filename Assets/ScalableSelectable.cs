using UnityEngine;
using System.Collections.Generic;

public class ScalableSelectable : RUISSelectable {
	private RUISWandSelector selector_;
	private RUISWandSelector scalerSelector;
	private float originalDistanceBetweenSelectors;
	private Vector3 originalScale;
	// Used to determine whether to pring debug stuff
	public bool debug = false;
	// Use only the mouse to (poorly) simulate scaling with two wands
	public bool useOnlyMouse = false;
	
	public bool isScaling { 
		get { 
			return base.isSelected && scalerSelector != null; 
		}
	}
	
	public void Awake() {
		base.Awake();
		ReInitialize();
	}
	
	public override void OnSelection(RUISWandSelector selector) {
		Log("ScalableSelectable#OnSelection called for " + selector);
		if(this.selector_ == null) {
			this.selector_ = selector;
		} else {
			Log("Setting scaler selection and the original distance between the selectors");
			this.scalerSelector = selector;
			this.originalDistanceBetweenSelectors = GetDistanceBetweenSelectors();
		}
		base.OnSelection(selector);
	}
	
	// This clears everything since there is no easy way to tell which selection ended
	public override void OnSelectionEnd() {
		Log("Selection ending");
		ReInitialize();
		base.OnSelectionEnd();
	}
	
	protected override void UpdateTransform(bool safePhysics) {
		if(isSelected) {
			// TODO: Need to use false here, otherwise base.UpdateTransform uses RigidBody
			// 		 for the translation & rotation, which somehow gets messed up when
			//		 scaling
			base.UpdateTransform(false);
			Log("position before: " + transform.position);
		}
		if(isScaling || (isSelected && useOnlyMouse)) {
			float scale = this.GetCurrentScale();
			if(scale > 0) {
				Log("Scaling with " + scale);
				transform.localScale = originalScale * scale;
			}
		}
	}
	
	private float GetCurrentScale() {
		// In the debug case just use some scale that is visible and changing
		if(useOnlyMouse)
			return (Mathf.Sin(Time.time) + 2) / 2.0f;
		else
			return GetDistanceBetweenSelectors() / originalDistanceBetweenSelectors;
	}
	
	private float GetDistanceBetweenSelectors() {
		float distance = 0;
		if(useOnlyMouse)
			distance = selector_.transform.position.magnitude;
		else
			distance = (selector_.transform.position - scalerSelector.transform.position).magnitude;
		Log("distance: " + distance);
		return distance;
	}
	
	private void Log(string message) {
		if(debug)
			Debug.Log(message);
	}
	
	private void ReInitialize() {
		Log("(Re)Initializing");
		selector_ = null;
		scalerSelector = null;
		originalDistanceBetweenSelectors = -1.0f;
		originalScale = transform.localScale;
	}
}

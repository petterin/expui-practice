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
	
	public override bool IsSelectable()
	{ 
		return this.selector_ == null || this.scalerSelector == null;	 
	}
	
	public void Awake() {
		base.Awake();
		ReInitialize();
	}
	
	public override void OnSelection(RUISWandSelector selector) {
		Log("ScalableSelectable#OnSelection called for " + selector);
		if(!this.isSelected) {
			this.selector_ = selector;
			// Only call this on first selection so the base class' selector does not change
			// when multi-selecting
			base.OnSelection(selector);
		} else {
			Log("Setting scaler selection and the original distance between the selectors");
			this.scalerSelector = selector;
			this.originalDistanceBetweenSelectors = GetDistanceBetweenSelectors();
			// Must update transform here?
			this.UpdateTransform(false);
		}
	}
	
	// This clears everything since there is no easy way to tell which selection ended
	public override void OnSelectionEnd() {
		Log("Selection ending");
		ReInitialize();
		base.OnSelectionEnd();
	}
	
	protected override void UpdateTransform(bool safePhysics) {
		if(!this.IsSelectable() || (this.isSelected && useOnlyMouse)) {
			float scale = this.GetCurrentScale();
			if(scale > 0) {
				Log("Scaling with " + scale);
				transform.localScale = originalScale * scale;
			}
		}
		if(this.isSelected) {
			base.UpdateTransform(safePhysics);
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
		//Log("distance: " + distance);
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

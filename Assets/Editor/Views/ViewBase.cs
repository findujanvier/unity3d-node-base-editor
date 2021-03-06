﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[Serializable]
public class ViewBase {

	public string viewTitle;
	public Rect viewRect;

	protected GUISkin viewSkin;
	protected NodeGraph currentGraph;

	public ViewBase (string viewTitle) {
		this.viewTitle = viewTitle;
		GetEditorSkin ();
	}

	public virtual void UpdateView (Rect editorRect, Rect precentageRect, Event e, NodeGraph currrentGraph) {
		if (viewSkin == null) {
			GetEditorSkin ();
			return;
		}

		// set the current view graph
		this.currentGraph = currrentGraph;
		if (currentGraph != null) {
			viewTitle = currentGraph.graphName;
		} else {
			viewTitle = "No graph";
		}
		// update view rect
		viewRect = new Rect (editorRect.x * precentageRect.x,
			editorRect.y * precentageRect.y,
			editorRect.width * precentageRect.width,
			editorRect.height * precentageRect.height
		);
	}
	public virtual void ProcessEvent (Event e) {}

	protected void GetEditorSkin () {
		viewSkin = (GUISkin) Resources.Load ("GUISkins/EditorSkins/NodeEditorSkin");
	}
}

﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class NodeGraph : ScriptableObject {

	public string graphName = "New Graph";
	public List<NodeBase> nodes;
	public NodeBase selectedNode;
	public bool wantsConnection = false;
	public NodeBase connectionNode;
	public bool showProperties = false;
	void OnEnable () {
		if (nodes == null)
			nodes = new List<NodeBase> ();
	}

	public void InitGraph() {
		// TODO: Scriptable object
		if (nodes.Count > 0) {
			for (int i = 0; i < nodes.Count; i++) {
				nodes [i].InitNode ();
			}
		}
	}

	public void UpdateGraph () {
		if (nodes.Count > 0) {

		}
	}

	public void UpdateGraphGUI (Event e, Rect viewRect, GUISkin viewSkin) {
		if (nodes.Count > 0) {
			ProcessEvents (e, viewRect);
			for (int i = 0; i < nodes.Count; i++) {
				nodes [i].UpdateNodeGUI (e, viewRect, viewSkin);
			}
		}

		if (wantsConnection) {
			if (connectionNode != null) {
				DrawConnectionToMouse (e.mousePosition);
			}
		}

		if (e.type == EventType.Layout) {
			if(selectedNode != null) {
				showProperties = true;
			}
		}
		EditorUtility.SetDirty (this);
	}

	private void ProcessEvents (Event e, Rect viewRect) {
		if (viewRect.Contains (e.mousePosition)) {
			if (e.button == 0) {
				if (e.type == EventType.MouseDown) {
					
					DeselectAllNodes ();
					showProperties = false;
					bool setNode = false;
					selectedNode = null;

					for (int i = 0; i < nodes.Count; i++) {
						if (nodes [i].nodeRect.Contains (e.mousePosition)) {
							nodes [i].isSelected = true;
							selectedNode = nodes [i];
							setNode = true;
						}
					}

					if (!setNode) {
						DeselectAllNodes ();
					}

					if (wantsConnection) {
						wantsConnection = false;
					}
				}
			}
		}
	}

	void DrawConnectionToMouse (Vector2 mousePosition) {
		Handles.BeginGUI ();
		Handles.color = Color.white;
		Handles.DrawLine (new Vector3 (connectionNode.nodeRect.x + connectionNode.nodeRect.width + 24f,
			connectionNode.nodeRect.y + connectionNode.nodeRect.height * 0.5f, 0f), 
			new Vector3(mousePosition.x, mousePosition.y, 0f)
		);
		Handles.EndGUI ();
	}

	void DeselectAllNodes () {
		for (int i = 0; i < nodes.Count; i++) {
			nodes [i].isSelected = false;
		}
	}
}

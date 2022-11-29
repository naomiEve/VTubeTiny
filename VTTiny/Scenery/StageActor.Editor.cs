﻿using System;
using ImGuiNET;
using VTTiny.Editor;

namespace VTTiny.Scenery
{
    public partial class StageActor
    {
        /// <summary>
        /// Renders this stage actor's editor GUI and all of the editor GUIs for the components within.
        /// </summary>
        /// <returns>
        /// Whether we've modified the actor collection before returning from the function.
        /// For example, by removing this actor from the actor list.
        /// </returns>
        internal bool RenderEditorGUI()
        {
            if (ImGui.TreeNode($"{Name}"))
            {
                if (ImGui.Button("Remove Actor"))
                {
                    OwnerStage.RemoveActor(this);
                    return true;
                }

                ImGui.Text($"Parented to: {ParentActor?.Name}");
                ImGui.Text("Components");

                foreach (var component in _components)
                {
                    if (ImGui.TreeNode($"{component.GetType().Name}"))
                    {
                        component.RenderEditorGUI();

                        ImGui.TreePop();
                    }
                }

                if (EditorGUI.ComponentDropdown(out Type componentType))
                    ConstructComponentFromType(componentType);

                ImGui.TreePop();
            }

            return false;
        }
    }
}
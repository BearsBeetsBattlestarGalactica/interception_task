﻿using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.XR.Interaction.Toolkit.UI
{
    /// <summary>
    /// A custom UI event for devices that exist within 3D Unity space, separate from the camera's position.
    /// </summary>
    public class TrackedDeviceEventData : PointerEventData
    {
        /// <summary>
        /// Created with event system.
        /// </summary>
        /// <param name="eventSystem"> The event system associated with the UI.</param>
        public TrackedDeviceEventData(EventSystem eventSystem)
            : base(eventSystem)
        {
        }

        /// <summary>
        /// A series of interconnected points used to track hovered and selected UI.
        /// </summary>
        public List<Vector3> rayPoints { get; set; }

        /// <summary>
        /// Set by the raycaster, this is the index within the raypoints list that received the hit.
        /// </summary>
        public int rayHitIndex { get; set; }

        /// <summary>
        /// The physics layer mask to use when checking for hits, both in occlusion and UI objects.
        /// </summary>
        public LayerMask layerMask { get; set; }

        /// <summary>
        /// (Read Only) The Interactor that triggered this event, or <see langword="null"/> if no interactor was responsible.
        /// </summary>
        public IUIInteractor interactor
        {
            get
            {
                var xrInputModule = currentInputModule as XRUIInputModule;
                if (xrInputModule != null)
                {
                    return xrInputModule.GetInteractor(pointerId);
                }

                return null;
            }
        }
    }
}

using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Canvas))]
    [ExecuteAlways]
    [AddComponentMenu("Layout/Canvas Scaler", 101)]
    [DisallowMultipleComponent]
    /// <summary>
    ///   The Canvas Scaler component is used for controlling the overall scale and pixel density of UI elements in the Canvas. This scaling affects everything under the Canvas, including font sizes and image borders.
    /// </summary>
    /// <remarks>
    /// For a Canvas set to 'Screen Space - Overlay' or 'Screen Space - Camera', the Canvas Scaler UI Scale Mode can be set to Constant Pixel Size, Scale With Screen Size, or Constant Physical Size.
    ///
    /// Using the Constant Pixel Size mode, positions and sizes of UI elements are specified in pixels on the screen. This is also the default functionality of the Canvas when no Canvas Scaler is attached. However, With the Scale Factor setting in the Canvas Scaler, a constant scaling can be applied to all UI elements in the Canvas.
    ///
    /// Using the Scale With Screen Size mode, positions and sizes can be specified according to the pixels of a specified reference resolution. If the current screen resolution is larger than the reference resolution, the Canvas will keep having only the resolution of the reference resolution, but will scale up in order to fit the screen. If the current screen resolution is smaller than the reference resolution, the Canvas will similarly be scaled down to fit. If the current screen resolution has a different aspect ratio than the reference resolution, scaling each axis individually to fit the screen would result in non-uniform scaling, which is generally undesirable. Instead of this, the ReferenceResolution component will make the Canvas resolution deviate from the reference resolution in order to respect the aspect ratio of the screen. It is possible to control how this deviation should behave using the ::ref::screenMatchMode setting.
    ///
    /// Using the Constant Physical Size mode, positions and sizes of UI elements are specified in physical units, such as millimeters, points, or picas. This mode relies on the device reporting its screen DPI correctly. You can specify a fallback DPI to use for devices that do not report a DPI.
    ///
    /// For a Canvas set to 'World Space' the Canvas Scaler can be used to control the pixel density of UI elements in the Canvas.
    /// </remarks>
    public class CanvasScaler : UIBehaviour
    {
        /// <summary>
        /// Determines how UI elements in the Canvas are scaled.
        /// </summary>
        public enum ScaleMode
        {
            /// <summary>
            /// Using the Constant Pixel Size mode, positions and sizes of UI elements are specified in pixels on the screen.
            /// </summary>
            ConstantPixelSize,
            /// <summary>
            /// Using the Scale With Screen Size mode, positions and sizes can be specified according to the pixels of a specified reference resolution.
            /// If the current screen resolution is larger than the reference resolution, the Canvas will keep having only the resolution of the reference resolution, but will scale up in order to fit the screen. If the current screen resolution is smaller than the reference resolution, the Canvas will similarly be scaled down to fit.
            /// </summary>
            ScaleWithScreenSize,
            /// <summary>
            /// Using the Constant Physical Size mode, positions and sizes of UI elements are specified in physical units, such as millimeters, points, or picas.
            /// </summary>
            ConstantPhysicalSize
        }

        [Tooltip("Determines how UI elements in the Canvas are scaled.")]
        [SerializeField] private ScaleMode m_UiScaleMode = ScaleMode.ConstantPixelSize;

        ///<summary>
        ///Determines how UI elements in the Canvas are scaled.
        ///</summary>
        public ScaleMode uiScaleMode { get { return m_UiScaleMode; } set { m_UiScaleMode = value; } }        Gizmos.color = Color.green;
            Gizmos.DrawWireCube(objectCollider.transform.position, colliderBounds.size);
            Vector3 oppoisteSideRayOrigin = mainRayHit.point + mainRay.direction * boundDistance;
            Ray oppoisteSideRay = new Ray(oppoisteSideRayOrigin, -mainRay.direction);
            RaycastHit oppositeRayHit;
            Vector3 verticalCenterPoint;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(mainRay.origin, mainRayHit.point);
            if (objectCollider.Raycast(oppoisteSideRay, out oppositeRayHit, boundDistance))
            {
                Gizmos.DrawLine(oppoisteSideRay.origin, oppositeRayHit.point);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(mainRayHit.point, oppositeRayHit.point);

                Vector3 mainToOppHitDirection = (oppositeRayHit.point - mainRayHit.point).normalized;
                float halfMainToOppDistance = (oppositeRayHit.point - mainRayHit.point).magnitude * 0.5f;
                verticalCenterPoint = mainRayHit.point + mainToOppHitDirection * halfMainToOppDistance;
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(verticalCenterPoint, 0.5f);

                Vector3 rightRayPos = verticalCenterPoint + transform.right * boundDistance;
                Vector3 rightRayPosToCenter = (verticalCenterPoint - rightRayPos).normalized;
                Ray rightSideRay = new Ray(rightRayPos, rightRayPosToCenter);
                RaycastHit rightInfo;

                Vector3 leftRayPos = verticalCenterPoint - transform.right * boundDistance;
                Vector3 leftRayPosToCenter = (verticalCenterPoint - leftRayPos).normalized;
                Ray LeftSideRay = new Ray(leftRayPos, leftRayPosToCenter);
                RaycastHit leftInfo;

                objectCollider.Raycast(rightSideRay, out rightInfo, boundDistance);
                objectCollider.Raycast(LeftSideRay, out leftInfo, boundDistance);
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(rightRayPos, rightInfo.point);
                Gizmos.DrawLine(leftRayPos, leftInfo.point);

                float halfDistance = (Vector3.Distance(rightInfo.point, leftInfo.point) * 0.5f);
                Vector3 horizontalCenterPoint = leftInfo.point + (rightInfo.point - leftInfo.point).normalized * halfDistance;
                Debug.Log($"{leftInfo.point}, {rightInfo.point}"); 
                Gizmos.DrawSphere(horizontalCenterPoint, 0.5f);
            }
        }
    }
}

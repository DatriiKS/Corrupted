using System;

using UnityEditor;

namespace Unity.PlasticSCM.Editor.UI
{
    internal static class ShowWindow
    {
        internal static PlasticWindow Plastic()
        {
            return ShowPlasticWindow(false);
        }

        internal static PlasticWindow PlasticAfterDownloadingProject()
        {
            return ShowPlasticWindow(true);
        }

        static PlasticWindow ShowPlasticWindow(bool disableCollabWhenLoaded)
        {
            PlasticWindow window = EditorWindow.GetWindow<PlasticWindow>(
                UnityConstants.PLASTIC_WINDOW_TITLE,
                true,
                mConsoleWindowType,
                mProjectBrowserType);

            if (disableCollabWhenLoaded)
                window.DisableCollabIfEnabledWhenLoaded();

            window.SetupWindowTitle(PlasticNotification.Status.None);

            return window;
        }

        s
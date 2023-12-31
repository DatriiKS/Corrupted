                                                   �y�                                                                                    TestListTreeViewDataSource  �  using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEditor.IMGUI.Controls;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools.TestRunner;

namespace UnityEditor.TestTools.TestRunner.GUI
{
    internal class TestListTreeViewDataSource : TreeViewDataSource
    {
        private bool m_ExpandTreeOnCreation;
        private readonly TestListGUI m_TestListGUI;
        private ITestAdaptor m_RootTest;

        public TestListTreeViewDataSource(TreeViewController testListTree, TestListGUI testListGUI, ITestAdaptor rootTest) : base(testListTree)
        {
            showRootItem = false;
            rootIsCollapsable = false;
            m_TestListGUI = testListGUI;
            m_RootTest = rootTest;
        }

        public void UpdateRootTest(ITestAdaptor rootTest)
        {
            m_Root
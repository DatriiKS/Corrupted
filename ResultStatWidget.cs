using System;

namespace Unity.VisualScripting
{
    /// <summary>
    /// Handles an exception if it occurs.
    /// </summary>
    [UnitCategory("Control")]
    [UnitOrder(17)]
    [UnitFooterPorts(ControlOutputs = true)]
    public sealed class TryCatch : Unit
    {
        /// <summary>
        /// The entry point for the try-catch block.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter { get; private set; }

        /// <summary>
        /// The action to attempt.
        /// </summary>
        [DoNotSerialize]
        public ControlOutput @try { get; private set; }

        /// <summary>
        /// The action to execute if an exception is thrown.
        /// </summary>
        [DoNotSerialize]
        public ControlOutput @catch { get; private set; }

        /// <summary>
        /// The action to execute afterwards, regardless of whether there was an exception.
        /// </summary>
        [DoNotSerialize]
        public ControlOutput @fina
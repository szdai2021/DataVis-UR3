using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using UnityEngine.XR.InteractionSubsystems;

namespace UnityEngine.XR.WindowsMR
{
    /// <summary>
    /// The event data related to a WindowsMR Manipulation gesture
    /// </summary>
    /// <seealso cref="XRGestureSubsystem"/>
    [StructLayout(LayoutKind.Sequential)]
    [Preserve]
    public struct WindowsMRManipulationGestureEvent : IEquatable<WindowsMRManipulationGestureEvent>
    {
        /// <summary>
        /// The <see cref="GestureId"/> associated with this gesture.
        /// </summary>
        public GestureId id { get { return m_Id; } }

        /// <summary>
        /// The <see cref="GestureState"/> of the gesture.
        /// </summary>
        public GestureState state { get { return m_State; } }

        /// <summary>
        /// Total distance moved since the beginning of the manipulation gesture.
        /// </summary>
        public Vector3 cumulativeDelta { get { return m_CumulativeDelta; } }

        /// <summary>
        /// Gets a default-initialized <see cref="WindowsMRManipulationGestureEvent"/>.
        /// </summary>
        /// <returns>A default <see cref="WindowsMRManipulationGestureEvent"/>.</returns>
        public static WindowsMRManipulationGestureEvent GetDefault()
        {
            return new WindowsMRManipulationGestureEvent(GestureId.invalidId, GestureState.Invalid, Vector3.zero);
        }

        /// <summary>
        /// Constructs a new <see cref="WindowsMRManipulationGestureEvent"/>.
        /// </summary>
        /// <param name="id">The <see cref="GestureId"/> associated with the gesture.</param>
        /// <param name="state">The <see cref="GestureState"/> associated with the gesture.</param>
        /// <param name="cumulativeDelta">The cumulative delta associated with the gesture.</param>
        public WindowsMRManipulationGestureEvent(GestureId id, GestureState state, Vector3 cumulativeDelta)
        {
            m_Id = id;
            m_State = state;
            m_CumulativeDelta = cumulativeDelta;
        }

        /// <summary>
        /// Generates a new string describing the gestures's properties suitable for debugging purposes.
        /// </summary>
        /// <returns>A string describing the gestures's properties.</returns>
        public override string ToString()
        {
            return string.Format(
                "Manipulation Gesture:\n\tgestureId: {0}\n\tgestureState: {1}\n\tcumulativeDelta: {2}",
                id, state, cumulativeDelta);
        }

        /// <summary>
        /// Determine if the <see cref="WindowsMRManipulationGestureEvent"/> object param is the same as this object
        /// </summary>
        /// <param name="obj">The <see cref="WindowsMRManipulationGestureEvent"/> object to check against</param>
        /// <returns>True if the objects are the same</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is WindowsMRManipulationGestureEvent && Equals((WindowsMRManipulationGestureEvent)obj);
        }

        /// <summary>
        /// Get the hash code for this <see cref="WindowsMRManipulationGestureEvent"/>
        /// </summary>
        /// <returns>The integer representation of the hash code</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                const int k_HashCodeMultiplier = 486187739;
                var hashCode = m_Id.GetHashCode();
                hashCode = (hashCode * k_HashCodeMultiplier) + ((int)m_State).GetHashCode();
                hashCode = (hashCode * k_HashCodeMultiplier) + (int)cumulativeDelta.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Operator Equals for <see cref="WindowsMRManipulationGestureEvent"/>
        /// </summary>
        /// <param name="lhs">Left hand <see cref="WindowsMRManipulationGestureEvent"/></param>
        /// <param name="rhs">Right hand <see cref="WindowsMRManipulationGestureEvent"/></param>
        /// <returns>True if the <see cref="WindowsMRManipulationGestureEvent"/> objects are the same</returns>
        public static bool operator ==(WindowsMRManipulationGestureEvent lhs, WindowsMRManipulationGestureEvent rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Operator Inequal for <see cref="WindowsMRManipulationGestureEvent"/>
        /// </summary>
        /// <param name="lhs">Left hand <see cref="WindowsMRManipulationGestureEvent"/></param>
        /// <param name="rhs">Right hand <see cref="WindowsMRManipulationGestureEvent"/></param>
        /// <returns>True if the <see cref="WindowsMRManipulationGestureEvent"/> objects are not the same</returns>
        public static bool operator !=(WindowsMRManipulationGestureEvent lhs, WindowsMRManipulationGestureEvent rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Check if a <see cref="WindowsMRManipulationGestureEvent"/> object is the same as this
        /// </summary>
        /// <param name="other">The <see cref="WindowsMRManipulationGestureEvent"/> object to test against</param>
        /// <returns>True if the <see cref="WindowsMRManipulationGestureEvent"/> objects are the same</returns>
        public bool Equals(WindowsMRManipulationGestureEvent other)
        {
            return
                m_Id.Equals(other.id) &&
                m_State == other.state &&
                m_CumulativeDelta == other.cumulativeDelta;
        }

        GestureId m_Id;
        GestureState m_State;
        Vector3 m_CumulativeDelta;
    }
}

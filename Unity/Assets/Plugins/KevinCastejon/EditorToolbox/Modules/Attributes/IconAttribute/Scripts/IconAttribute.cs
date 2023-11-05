using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    /// <summary>
    /// Custom inspector property icon.
    /// </summary>
    public class IconAttribute : PropertyAttribute
    {
        public string path;

        /// <summary>
        /// Custom inspector property icon.
        /// </summary>
        /// <param name="path">The relative path (starting from 'Assets/') to the icon you want to display in front of the property.</param>
        public IconAttribute(string path)
        {
            this.path = path;
        }
    }
}
using System.Drawing;
using VuToanThang_23110329.Controls;

namespace VuToanThang_23110329.Controls
{
    public static class ButtonFactory
    {
        // Standard button sizes
        public static readonly Size SmallSize = new Size(80, 32);
        public static readonly Size MediumSize = new Size(100, 36);
        public static readonly Size LargeSize = new Size(120, 40);

        /// <summary>
        /// Creates a modern button with specified text and type
        /// </summary>
        public static ModernButton CreateButton(string text, ModernButton.ButtonType type = ModernButton.ButtonType.Primary, Size? size = null)
        {
            var button = new ModernButton
            {
                Text = text,
                Type = type,
                Size = size ?? MediumSize,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };
            return button;
        }

        /// <summary>
        /// Creates a primary button (blue) - for main actions
        /// </summary>
        public static ModernButton CreatePrimaryButton(string text, Size? size = null)
        {
            return CreateButton(text, ModernButton.ButtonType.Primary, size);
        }

        /// <summary>
        /// Creates a success button (green) - for save, add actions
        /// </summary>
        public static ModernButton CreateSuccessButton(string text, Size? size = null)
        {
            return CreateButton(text, ModernButton.ButtonType.Success, size);
        }

        /// <summary>
        /// Creates a warning button (orange) - for edit, update actions
        /// </summary>
        public static ModernButton CreateWarningButton(string text, Size? size = null)
        {
            return CreateButton(text, ModernButton.ButtonType.Warning, size);
        }

        /// <summary>
        /// Creates a danger button (red) - for delete actions
        /// </summary>
        public static ModernButton CreateDangerButton(string text, Size? size = null)
        {
            return CreateButton(text, ModernButton.ButtonType.Danger, size);
        }

        /// <summary>
        /// Creates an info button (light blue) - for info actions
        /// </summary>
        public static ModernButton CreateInfoButton(string text, Size? size = null)
        {
            return CreateButton(text, ModernButton.ButtonType.Info, size);
        }

        /// <summary>
        /// Creates a secondary button (gray) - for cancel, secondary actions
        /// </summary>
        public static ModernButton CreateSecondaryButton(string text, Size? size = null)
        {
            return CreateButton(text, ModernButton.ButtonType.Secondary, size);
        }

        /// <summary>
        /// Creates a dark button (dark gray) - for neutral actions
        /// </summary>
        public static ModernButton CreateDarkButton(string text, Size? size = null)
        {
            return CreateButton(text, ModernButton.ButtonType.Dark, size);
        }
    }
}

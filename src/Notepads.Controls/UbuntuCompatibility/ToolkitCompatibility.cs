namespace Microsoft.Toolkit.Uwp.UI.Converters
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public sealed class EmptyObjectToObjectConverter : IValueConverter
    {
        public object EmptyValue { get; set; }
        public object NotEmptyValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language) => value == null ? EmptyValue : NotEmptyValue;
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();
    }

    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => value is true ? Visibility.Visible : Visibility.Collapsed;
        public object ConvertBack(object value, Type targetType, object parameter, string language) => value is Visibility visibility && visibility == Visibility.Visible;
    }

    public sealed class BoolToObjectConverter : IValueConverter
    {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language) => value is true ? TrueValue : FalseValue;
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();
    }

    public sealed class DoubleToVisibilityConverter : IValueConverter
    {
        public double GreaterThan { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language) => value is double number && number > GreaterThan ? Visibility.Visible : Visibility.Collapsed;
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();
    }
}

namespace Microsoft.Toolkit.Uwp.UI
{
    using Windows.UI.Xaml;

    public static class FrameworkElementExtensions
    {
        public static readonly DependencyProperty AncestorTypeProperty = DependencyProperty.RegisterAttached("AncestorType", typeof(string), typeof(FrameworkElementExtensions), new PropertyMetadata(null));
        public static readonly DependencyProperty AncestorProperty = DependencyProperty.RegisterAttached("Ancestor", typeof(DependencyObject), typeof(FrameworkElementExtensions), new PropertyMetadata(null));

        public static void SetAncestorType(DependencyObject element, string value) => element.SetValue(AncestorTypeProperty, value);
        public static string GetAncestorType(DependencyObject element) => (string)element.GetValue(AncestorTypeProperty);
        public static DependencyObject GetAncestor(DependencyObject element) => (DependencyObject)element.GetValue(AncestorProperty);
    }

    public static class VisualTreeExtensions
    {
        public static DependencyObject FindDescendant(this DependencyObject root, string name)
        {
            if (root == null) return null;
            for (var index = 0; index < Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(root); index++)
            {
                var child = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(root, index);
                if (child is FrameworkElement element && element.Name == name) return child;
                var descendant = child.FindDescendant(name);
                if (descendant != null) return descendant;
            }
            return default;
        }

        public static T FindDescendant<T>(this DependencyObject root, string name = null) where T : FrameworkElement
        {
            if (root == null) return default;
            for (var index = 0; index < Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(root); index++)
            {
                var child = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(root, index);
                if (child is T typed && (name == null || typed.Name == name)) return typed;
                var descendant = child.FindDescendant<T>(name);
                if (descendant != null) return descendant;
            }
            return default;
        }

        public static T FindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            for (var parent = Windows.UI.Xaml.Media.VisualTreeHelper.GetParent(child); parent != null; parent = Windows.UI.Xaml.Media.VisualTreeHelper.GetParent(parent))
                if (parent is T typed) return typed;
            return default;
        }
    }

}

namespace Microsoft.Toolkit
{
    public static class StringExtensions
    {
        public static bool IsEmail(this string value) => !string.IsNullOrWhiteSpace(value) && value.IndexOf('@') > 0 && value.IndexOf('@') < value.Length - 1;
    }
}

namespace ColorCode
{
    public interface ILanguage { }
    internal sealed class Language : ILanguage { }
    public static class Languages
    {
        public static ILanguage FindById(string id) => string.IsNullOrWhiteSpace(id) ? null : new Language();
    }

    public sealed class RichTextBlockFormatter
    {
        private readonly Styling.RichTextBlockFormatter _formatter;
        public RichTextBlockFormatter(Styling.StyleDictionary styles) => _formatter = new Styling.RichTextBlockFormatter(styles);
        public RichTextBlockFormatter(Windows.UI.Xaml.ElementTheme theme) => _formatter = new Styling.RichTextBlockFormatter(theme);
        public void FormatInlines(string text, ILanguage language, Windows.UI.Xaml.Documents.InlineCollection inlines) => _formatter.FormatInlines(text, language, inlines);
    }
}

namespace ColorCode.Styling
{
    using ColorCode;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Documents;

    public sealed class StyleDictionary { }

    public sealed class RichTextBlockFormatter
    {
        public RichTextBlockFormatter(StyleDictionary styles) { }
        public RichTextBlockFormatter(ElementTheme theme) { }

        public void FormatInlines(string text, ILanguage language, InlineCollection inlines)
        {
            inlines.Add(new Run { Text = text });
        }
    }
}

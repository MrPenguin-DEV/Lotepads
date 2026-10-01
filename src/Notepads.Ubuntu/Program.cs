namespace Notepads
{
    using Windows.UI.Xaml;

    /// <summary>Linux desktop entry point for the shared Notepads application.</summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            Application.Start(_ => new App());
        }
    }
}

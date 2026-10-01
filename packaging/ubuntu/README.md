# Ubuntu desktop integration

The Ubuntu build is the same Notepads application and visual tree, compiled
with Uno's GTK desktop backend. It does not maintain a separate Linux UI or
ship a bundled runtime; it uses the installed .NET runtime to stay lightweight.

Publish a framework-dependent Linux build with
`dotnet publish src/Notepads.Ubuntu/Notepads.Ubuntu.csproj -c Release -r linux-x64 --self-contained false`.
Install the resulting executable as `Notepads` on the system `PATH`, then
install `notepads.desktop` to
`/usr/share/applications/notepads.desktop` (or
`~/.local/share/applications/notepads.desktop`). Install the application icon
as `notepads` in the appropriate `hicolor` icon directory for desktop-menu
integration.

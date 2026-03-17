using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Admin.Extensibility
{
    public class Hook
    {
        // https://stackoverflow.com/questions/18362368/loading-dlls-at-runtime-in-c-sharp

        public void CreateHook([CallerMemberName] string methodName = null, string className = null, object[] parameters = null)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                var stackTrace = new StackTrace();
                className = stackTrace.GetFrame(1).GetMethod().DeclaringType?.Name;
            }

            if (string.IsNullOrWhiteSpace(methodName) || string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException("className and methodName cannot be null or empty");
            }

            string executingPath = Assembly.GetExecutingAssembly().Location;
            string libraryFullPath = Path.Combine(
                Path.GetDirectoryName(executingPath),
                $"{className}Extended.dll");

            if (!File.Exists(libraryFullPath))
            {
                return;
            }

            try
            {
                var library = Assembly.LoadFile(libraryFullPath);

                foreach (Type type in library.GetExportedTypes())
                {
                    var instance = Activator.CreateInstance(type);
                    type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, instance, parameters);
                }
            }
                catch (Exception ex)
            {
                Console.WriteLine($"Hook error: {ex.Message}");
            }
        }
    }
}

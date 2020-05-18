namespace Gofbd.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;

    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> Assemblies { get; }
    }
    public class AssemblyProvider : IAssemblyProvider
    {
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (_assemblies != null)
                    return _assemblies;

                var assemblies = GetAssemblies();
                lock (lockObject)
                {
                    if (_assemblies == null)
                        _assemblies = assemblies;
                }
                return assemblies;
            }
        }
        private static readonly IAssemblyProvider Singleton = new AssemblyProvider();
        private static readonly object lockObject = new Object();

        private static IEnumerable<Assembly> _assemblies;

        public static IAssemblyProvider Instance
        {
            get { return Singleton; }
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path == null)
            {
                throw new InvalidOperationException("Unable to get direcotry location.");
            }
            var files = Directory.GetFiles(path, "Gofbd*.dll");
            var assemblies = files.Select(LoadAssembly).ToList();
            assemblies.Add(Assembly.GetEntryAssembly());
            return assemblies.Where(a => a != null).Distinct().OrderBy(a => a.FullName).ToList();
        }

        private static Assembly LoadAssembly(string path)
        {
            try
            {
                return AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

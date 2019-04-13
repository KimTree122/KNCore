using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace KNCore.Comm.ServiceRegistry
{
    public static class AssemblyHelper
    {
        public static List<Assembly> LoadCompileAssemblies()
        {
            
            List<CompilationLibrary> libs = DependencyContext.Default.CompileLibraries
                .Where(lib => !lib.Serviceable && lib.Type != "package").ToList();
            List<Assembly> ret = new List<Assembly>();

            foreach (var lib in libs)
            {

                //try
                //{
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                ret.Add(assembly);
                //}
                //catch
                //{
                //assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(dll.Name)));

                //if (assembly == null)
                //{
                //    throw;
                //}
                //}
            }

            return ret;
        }

    }
}

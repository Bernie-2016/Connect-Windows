using BernieApp.Common.DependencyInjection;

namespace BernieApp.Common.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        //    "CA1822:MarkMembersAsStatic",
        //    Justification = "This non-static member is needed for data binding purposes.")]
        public static MainViewModel Main
        {
            get
            {
                return IOC.Default.Resolve<MainViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}
